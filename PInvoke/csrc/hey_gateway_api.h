#ifndef HEY_GATEWAY_API_H
#define HEY_GATEWAY_API_H

#include <stdint.h>

typedef enum {
    P_IDLE,
    P_CONNECTED,
    P_SECURE_CHANNEL_ESTABLISHED,
    P_GET_DEVICE_INFO,
    P_WAIT_USER_CONFIRM = 32,
    P_FAIL,
    P_SUCCESS = 1024,
}SUBDEV_ADD_PROGRESS;

typedef enum  {
    NETWORK_CORE      = 0,
    NETWORK_BLE       ,
    NETWORK_BLE_MESH  ,
    // NETWORK_ZIGBEE ,
    // NETWORK_LORA   ,
    NETWORK_COMMUNITY ,
    NETWORK_MAX,
}SUBDEV_NETWORK_TYPE;

#ifdef BASE_ON_RTOS
typedef struct {
    uint16_t len;
    uint8_t *s;
}string_t;

int string_assign(string_t *s, char *ts, int len);
void string_free(string_t *s);
int string_cmp(string_t *s1, string_t *s2);
int string_cpy(string_t *dst, string_t *src);
#else
#include "HeyThings_devinfo.h"  //libheythings.
int string_cmp(string_t *s1, string_t *s2);
#endif


// info of gateway itself. provided for RTOS.
//get pubkey of gateway
int plugin_get_domain_pubkey(uint8_t **key, int *key_len);

//get private key and cert of gateway.
int plugin_get_gateway_secure_info(uint8_t **private_key, uint8_t **cert, int *cert_len);


#define MAX_VENDOR_LEN  16

//hide the internal struct define.
typedef struct cmd_transaction cmd_transaction_t;
typedef struct ht_packet ht_packet_t;
typedef void   action_handle_t;

/**
 * @brief : candidate device for setup
 */
struct candidate_dev{
    SUBDEV_NETWORK_TYPE   nwk_type;   //BLE, Beacon, Mesh
    uint32_t        pid;
    uint32_t        cid;
    int8_t          rssi;

    string_t    vendor_dev_id;
    string_t    dev_data;       //private device data used by plugin if it has.
};

/**
 * @brief : API for community app to add new sub device.
 * @param bind_key       : bindkey get from cloud
 * @param bind_key_len   : bind key len
 * @param dev_pubkey     : pubkey of device.
 * @param dev_pubkey_len : pubkey length
 * @param p_dev          : device info (to be added)
 * @return int
 */
int community_bind_new_dev(char *bind_key, int bind_key_len, uint8_t *dev_pubkey,
                           int dev_pubkey_len, struct candidate_dev *p_dev);


/**
 * @brief : plugin report candidate subdev to core after network scanning.
 * @param p_dev : candidate device info
 * @return int
 */
void plugin_report_candidate_subdev(struct candidate_dev *p_dev);

/**
 * @brief : plugin notify subdev adding progress to core.
 * @param trans
 * @param progress_code
 */
void plugin_notify_adding_progress(struct cmd_transaction *trans,
                                  uint32_t progress_code, string_t *event_data);


//debug only.
void debug_print_candidate_dev(struct candidate_dev *p_dev);


//子设备属性变化，通知网关，网关上报属性到云端和订阅者
int subdev_update_property(uint32_t dev_addr, int siid, int iid, uint8_t *value, int value_len);

//子设备返回action call的处理结果 action_return.
int subdev_action_return(action_handle_t *action, int act_out_len, uint8_t *act_out);

/******************* 事件上报 *************************/
struct subdev_event {
    uint32_t  siid;
    uint32_t  iid;
    uint8_t   *data;
    int       data_len;
    uint32_t  importance;
    uint64_t  event_id;
    uint64_t  ref_event_id;
    uint64_t  timestamp;
};

//子设备上报事件到网关，网关上报事件到云端和订阅者。
int subdev_raise_event(uint32_t dev_addr, struct subdev_event *evt);


/**
 *  1.  APP   -> GW_CORE           : start scanning sub-dev
 *  2.           GW_CORE -> PLUGIN : interface->set_nwk_joinable(true)
 *  3.           GW_CORE <- PLUGIN : plugin_report_candidate_subdev(p_cand_dev)
 *  4.  APP   <- GW_CORE           : pb subdev info
 *  5.  APP   -> GW_CORE           : CMD_SUBDEV_SETUP_REQ
 *  6.           GW_CORE -> PLUGIN : interface->add_subdev(trans, p_cand_dev)
 *  7.           GW_CORE <- PLUGIN : plugin_notify_adding_progress(trans, code)
 *  8.  APP   <- GW_CORE           : progress
 *  9.  APP   <- GW_CORE           : progress = complete
 *  10  CLOUD <- GW_CORE           : CMD_BIND_SUBDEV_REQ
 *  11. CLOUD -> GW_CORE           : CMD_BIND_SUBDEV_RESP
 *  12.          GW_CORE -> PLUGIN : interface->bind_subdev_complete(result)
 *
 */
typedef struct subdev_plugin_interface_s {

    char *name;             //plugin name for debugging

    SUBDEV_NETWORK_TYPE nwk_type;

    //core notify plugin to enter/exit joinable mode.
    int (* set_nwk_joinable)(int enable, int timeout_ms); //deprecated. will remove.

    //App/Cloud want plugin module to start scanning subdevice. use this api to notify plugin.
    //plugin module call plugin_report_candidate_subdev() to report subdev found.
    //when plugin module stopped scanning (timeouted or error),plugin should call subdev_scan_stopped()
    int (*start_subdev_scan)(char *pid, char *verdor_dev_id, int timeout_ms);

    //return 0 : add device to our network success.
    //       1 : progress on-going
    //      -1 : failed to add device to our network.
    int (*add_subdev)(cmd_transaction_t *p_trans_handle, struct candidate_dev *p_dev, uint8_t *oob, int oob_len);

    //core notity plugin cancel the adding progress, could be caused by app cancel it or timeout.
    int (*cancel_subdev_adding)();

    //bind subdev to cloud complete
    int (*bind_subdev_complete)(int result, struct candidate_dev *p_dev, uint32_t domain_addr, uint8_t *cert, int cert_len);

    //delete a subdev from the nwk.
    int (*remove_subdev)(uint32_t dev_addr);

    //网关透传app和云端命令，到子设备处理模块
    int (*subdev_cmd_relay)(ht_packet_t *pkt);  //for BLE module

    // core notify plugin to handle set cmd.
	// /* 返回值： -1 处理中，网关回应命令处理中。  >= 0 success or error code.*/
    int (*handle_cmd_set)(cmd_transaction_t *p_trans_handle, uint32_t dev_addr,
                           int siid, int n_iid, unsigned int *iids, int len,
                           unsigned char *changes);

    //TODO:
    int (*handle_cmd_array_add)(cmd_transaction_t *p_trans_handle, uint32_t dev_addr,
                                int siid, int iid, int len, unsigned char* value);

    int (*handle_cmd_array_update)(cmd_transaction_t *p_trans_handle, uint32_t dev_addr,
                                 int siid, int iid, int len, unsigned char* value);

    int (*handle_cmd_array_del)(cmd_transaction_t *p_trans_handle, uint32_t dev_addr,
                                int siid, int iid, int n_ids, uint32_t* ids);

    //action call, call subdev_action_return() to send result back to caller.
    int (*handle_cmd_action_call)(action_handle_t *action, uint32_t dev_addr,
								  int siid, int iid, int act_in_len, unsigned char *act_in);

    // action stream.
	int (*handle_cmd_action_start)(action_handle_t *action, uint32_t dev_addr,
								  int siid, int iid, int msg_len, unsigned char *msg);

	int (*handle_cmd_action_msg)(action_handle_t *action, uint32_t dev_addr,
								  int siid, int iid, int msg_len, unsigned char *msg );

    int (*handle_cmd_action_end)(action_handle_t *action, uint32_t dev_addr, int siid, int iid );

    /** cmds below were handled by the core. plugin can ignore this cmd */
    // //core notify plugin that a get cmd from app was received.
    // int (*cmd_get)(uint64_t did, int siid, int n_iid, unsigned int* iids);

    // //core notify plugin that an observe cmd from app was received.
    // int (*cmd_observe)(int siid, int n_iid, unsigned int* iids);

}subdev_plugin_interface;

/**
 * @brief : 子设备处理模块注册
 * @param subdev_mgmt
 * @return int
 */
int plugin_subdev_plugin_register(subdev_plugin_interface *subdev_mgmt);

//子设备消息，网关透传到app或云端, BLE透传模块调用
int subdev_cmd_relay_response(ht_packet_t *pkt);

// 子设备停止扫描通知. plugin->start_subdev_scan() 开始扫描。
void subdev_scan_stopped();

/**
 * @brief : start gateway init and run main loop. this function will not return.
 * @return int
 */
int hey_start_gateway_sdk();

/**
 * @brief : check whetehr gateway is ready
*/
int hey_gateway_ready();


/*
*   key-value storage: get value size.
*/
typedef unsigned int (*func_kv_get_size)(char* key);

/*
*   key-value storage: get value (fixed size).
*/
typedef unsigned int (*func_kv_get_value)(char* key, unsigned char* buf, unsigned int data_size);

/*
*   key-value storage: add key value.
*/
typedef unsigned int (*func_kv_set)(char* key, unsigned char* value, unsigned value_size);

/*
*   key-value storage: delete key-value pair.
*/
typedef int (*func_kv_del)(char* key);


struct hey_kv_op {
    func_kv_get_size    get_size;
    func_kv_get_value   get;
    func_kv_set         set;
    func_kv_del         del;
};

/**
 * @brief  :  set key-value operation,
 *            if kv_op is null, use the key-value function defined by sdk.
 *            if kv_op is not null, use user defined function.
 * @param path  : path of key-value files if required.
 * @param kv_op : key-value operation defined by user.
 * @return int
 */
int hey_gateway_set_kv_op(char *path, struct hey_kv_op *kv_op);


// key-value storage. provide for BLE or other submodule.
unsigned int hey_kv_get_size(char* key);
unsigned int hey_kv_get(char* key, unsigned char* buf, unsigned int data_size);
unsigned int hey_kv_set(char* key, unsigned char* value, unsigned value_size);
int hey_kv_del(char* key);
unsigned int hey_kv_get_newbuf(char* key, unsigned char** pbuf, unsigned int* olen);

// should include the 'platform-interfaces.h' provided by the wifi-sdk before these calling these MARCO.
// #define HELPER_KV_GET_SIZE(key)                     gStorageInterface.f_kv_get_size(key)
// #define HELPER_KV_GET_VALUE(key, buf, size)         gStorageInterface.f_kv_get_value(key, buf, size)
// #define HELPER_KV_GET_VAL_NEWBUF(key, pptr, osize)  gStorageInterface.f_kv_get_value_newbuf(key,pptr,osize)
// #define HELPER_KV_SET(key, value, size)             gStorageInterface.f_kv_set(key,value,size)
// #define HELPER_KV_DEL(key)                          gStorageInterface.f_kv_del(key)

#endif