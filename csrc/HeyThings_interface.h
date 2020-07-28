/*
 *  Copyrith (c) 2020 OPPO. All rights reserved.
 */

/**
 *  @file HeyThings_interface.h
 *
 *  
 *
 */

#ifndef _HeyThings_INTERFACE_H
#define _HeyThings_INTERFACE_H

#include <stdint.h>

#define HEYTHINGS_VERSION_MAJOR 1
#define HEYTHINGS_VSERION_MINOR 6
#define HEYTHINGS_VERSION_PATCH 1

#include "HeyThings_devinfo.h"

/**
 * local_type_t - Local communication message type
 */
typedef enum
{
	LOCAL_TYPE_HEYTHINGS_FORWARD,		  /**< Forward the message, SDK will not process the message */
	LOCAL_TYPE_HEYTHINGS_PROCESS,		  /**< SDK process the message or forward the msg by transaction id */
	LOCAL_TYPE_DEV_STATUS,				  /**< SDK send the program device status */
	LOCAL_TYPE_DEV_STATUS_REQ,			  /**< Program request the device status */
	LOCAL_TYPE_DEV_INFO,				  /**< Program and SDK synchronization device information */
	LOCAL_TYPE_DEV_INFO_REQ,			  /**< Program request the device information*/
	LOCAL_TYPE_CLIENT_DISCONNECT,		  /**< APP client disconnect */
	LOCAL_TYPE_DEV_TIME,				  /**< SDK synchronization the cloud time and send to prgram*/
	LOCAL_TYPE_GET_PROPERTIES_REQ,		  /**< Program request to get service properties */
	LOCAL_TYPE_GET_PROPERTIES_RESP,		  /**< SDK response the service proerties */
	LOCAL_TYPE_SET_PROPERTIES_REQ,		  /**< Program change and set the service properties */
	LOCAL_TYPE_SET_PROPERTIES_RESP,		  /**< SDK response the set result */
	LOCAL_TYPE_OBSERVE_PROPERTIES_REQ,	  /**< Program observe service properties */
	LOCAL_TYPE_OBSERVE_PROPERTIES_RESP,	  /**< SDK response the observe result */
	LOCAL_TYPE_PROPERTIES_CHANGE,		  /**< When service properties change, SDK notification program */
	LOCAL_TYPE_SUBSCIBE_MSG_BY_ADDRESS,	  /**< Program subscibe the heythings message by dst_addr */
	LOCAL_TYPE_SUBSCIBE_MSG_BY_TYPE,	  /**< Program subscibe the heythings message by message type, except the properties message */
	LOCAL_TYPE_SUBSCIBE_MSG_BY_SIID,	  /**< program subscibe the properties message by serivce id */
	LOCAL_TYPE_SUBSCIBE_RESP,			  /**< subscibe response */
	LOCAL_TYPE_UNSUBSCIBE_MSG_BY_ADDRESS, /**< unsubscibe the message by dst_addr */
	LOCAL_TYPE_UNSUBSCIBE_MSG_BY_TYPE,	  /**< unsubscibe the message by type */
	LOCAL_TYPE_UNSUBSCIBE_MSG_BY_SIID,	  /**< unsubscibe the message by service id */
	LOCAL_TYPE_UNSUBSCIBE_RESP,			  /**< unsubscibe response */
	LOCAL_TYPE_GENERATE_DEV_KEY_REQ,
	LOCAL_TYPE_GENERATE_DEV_KEY_RESP,
	LOCAL_TYPE_NETWORK_READY,
	LOCAL_TYPE_BIND_RESET,
	LOCAL_TYPE_SUBSCIBE_EVENT,
	LOCAL_TYPE_UNSUBSCIBE_EVENT,
	LOCAL_TYPE_TRIGGER_EVENT_REQ,
	LOCAL_TYPE_TRIGGER_EVENT_RESP,
	LOCAL_TYPE_SETUP,
	LOCAL_TYPE_CONNECT_CLIENT,
	LOCAL_TYPE_CLIENT_CONNECTED,
	LOCAL_TYPE_LOG,
	LOCAL_TYPE_ACTIVE_TRANSACTION_REQ,
	LOCAL_TYPE_ACTIVE_TRANSACTION_RESP,
	LOCAL_TYPE_PIN_CODE,
} local_type_t;

/**
 * dev_status_t - Device current status
 */
typedef enum dev_status
{
	DEV_STATUS_MIN,
	DEV_STATUS_UNKNOW,			   /**< The SDK has some error */
	DEV_STATUS_BIND_FAILED,		   /**< The SDK with cloud bind failed */
	DEV_STATUS_INIT,			   /**< The SDK will start now */
	DEV_STATUS_WAIT_DEVINFO,	   /**< The SDK wait other program send the devinfo */
	DEV_STATUS_WAIT_BINDINFO,	   /**< The SDK wait APP send the setup information */
	DEV_STATUS_BINDING,			   /**< The SDK is binding now */
	DEV_STATUS_CLOUD_CONNECTING,   /**< The SDK connect the cloud now */
	DEV_STATUS_NORMAL,			   /**< The SDK is normal, then can receive the cloud and app message */
	DEV_STATUS_RESET,			   /**< The SDK is reset */
	DEV_STATUS_WAIT_NETWORK_READY, /**< The SDK wait the network ready */
	DEV_STATUS_MAX
} dev_status_t;

/**
 * HeyThings_handler_t - libheythings library process handler
 */
typedef struct HeyThings_handler HeyThings_handler_t;

/**
 * property_t - The service property 
 */
typedef struct
{
	uint32_t siid; /**< service id */
	uint32_t iid;  /**< property id */
	uint32_t id;   /**< The array property id */
	int length;	   /**< property value length */
	void *data;	   /**< property value data */
} property_t;

typedef struct
{
	uint32_t siid;
	uint32_t iid;
	uint32_t seq;
	uint32_t lost;
	uint64_t timestamp;
	uint64_t ref_notification_id;
	uint32_t importance;
	uint32_t length;
	uint8_t *data;
} event_t;

typedef struct action action_t;

/**
 * HeyThings_handler_callback - The libheythings callback functions.
 */
typedef struct HeyThings_handler_callback
{
	/**
	 * get_properties_callback() - The get properties response
	 * 
	 * @param[in] msgid		The request msg id, by the ::HeyThings_get_properties
	 * @param[in] count		Response properties count
	 * @param[in] properties	Response properties ::property_t
	 * @param[in] user_data 	The user custon data by ::HeyThings_handler_init
	 **/
	void (*get_properties_callback)(uint32_t msgid, uint32_t count,
									property_t properties[], void *user_data);

	/**
	 * set_properties_callback() - The set properties response
	 * 
	 * @param[in] msgid		The request msg id, by the ::HeyThings_set_properties
	 * @param[in] retsult		Response set properties result
	 * @param[in] user_data 	The user custon data by ::HeyThings_handler_init
	 **/
	void (*set_properties_callback)(uint32_t msgid, int result, void *user_data);

	/**
	 * observe_properties_callback() - The observe properties response
	 * 
	 * @param[in] msgid		The request msg id, by the ::HeyThings_observe_properties
	 * @param[in] count		Response observe properties count
	 * @param[in] properties	Response observe properties ::property_t
	 * @param[in] user_data 	The user custon data by ::HeyThings_handler_init
	 **/
	void (*observe_properties_callback)(uint32_t msgid, uint32_t count,
										property_t properties[], void *user_data);

	/**
	 * properties_change_callback() - The SDK notification the change properties
	 * 
	 * @param[in] count		Response observe properties count
	 * @param[in] properties	Response observe properties ::property_t
	 * @param[in] user_data 	The user custon data by ::HeyThings_handler_init
	 */
	void (*properties_change_callback)(uint32_t count, property_t properties[],
									   void *user_data);

	/**
	 * heythings_msg_callback() - The SDK forward heythings message
	 * 
	 * @param[in] msg			The forward heythings message
	 * @param[in] len 			message length
	 * @param[in] user_data 	The user custon data by ::HeyThings_handler_init
	 */
	void (*heythings_msg_callback)(uint8_t *msg, int len, void *user_data);

	/**
	 * dev_status_callback() - The SDK status
	 * 
	 * @param[in] status		device status ::dev_status_t
	 * @param[in] user_data 	The user custon data by ::HeyThings_handler_init
	 */
	void (*dev_status_callback)(dev_status_t status, void *user_data);

	/**
	 * dev_info_callback() - The SDK and program synchronization the device information
	 * 
	 * @param[in] info 		The synchronization device information ::devinfo_t
	 * @param[in] user_data 	The user custon data by ::HeyThings_handler_init
	 */
	void (*dev_info_callback)(devinfo_t *info, void *user_data);

	void (*pin_code_callback)(char *pin_code, int len, void *user_data);

	/**
	 * client_disconnect_callback() - When app disconnect, SDK will notification program.
	 * 
	 * @param[in] client_count		The disconnect APP clound
	 * @param[in] clients			The APP home address array
	 * @param[in] user_data 	The user custon data by ::HeyThings_handler_init
	 */
	void (*client_disconnect_callback)(uint32_t client, int reason, void *user_data);

	void (*client_connected_callback)(uint32_t addr, void *user_data);
	/**
	 * subscibe_callback() - subscibe response
	 * 
	 * @param[in] result	subscibe result 
	 * @param[in] user_data 	The user custon data by ::HeyThings_handler_init
	 **/
	void (*subscibe_callback)(int result, void *user_data);

	/**
	 * unsubscibe_callback() - unsubscibe response
	 * 
	 * @param[in] result	unsubscibe result 
	 * @param[in] user_data 	The user custon data by ::HeyThings_handler_init
	 **/
	void (*unsubscibe_callback)(int result, void *user_data);

	/**
	 * @brief - generate the device ecdsa private key
	 *
	 * This function for the one model one key first run to give the device 
	 * private key.
	 *
	 * @param[in] pri_key_pem	The device private key, the key format is pem.
	 * @param[in] pri_key_len	The private key length.
	 * @param[in] user_data 	The user custon data by ::HeyThings_handler_init
	 */
	void (*generate_dev_key_callback)(char *pri_key_pem, uint32_t pri_key_len,
									  void *user_data);
	/**
	 * @brief - 
	 *
	 */
	void (*trigger_event_callback)(uint32_t event_id, uint64_t notification_id, void *user_data);

	//void (*event_callback)(event_t *event, void *user_data);
	/**
	 * @brief - 
	 *
	 **/
	void (*action_call_callback)(action_t *action,
								 uint32_t siid, uint32_t iid, uint32_t len, uint8_t *data, void *user_data);

	void (*action_return_callback)(action_t *action,
								   uint32_t len, uint8_t *data, void *user_data);

	void (*action_stream_start_callback)(action_t *action,
										 uint32_t siid, uint32_t iid, uint32_t len,
										 uint8_t *data, void *user_data);

	void (*action_stream_msg_callback)(action_t *action,
									   uint32_t seq, uint32_t len, uint8_t *data, void *user_data);

	void (*action_stream_end_callback)(action_t *action);

	/*
	void (*action_start_callback)(uint32_t action_id, uint32_t win,
			uint32_t siid, uint32_t iid, uint32_t len, uint8_t *data, void *user_data);
	void (*action_msg_callback)(uint32_t action_id, uint32_t seq,
			uint32_t len, uint8_t *data, void *user_data); 
	void (*action_end_callback)(uint32_t action_id, void *user_data);
	*/

	// void (*properties_write_callback)(uint32_t msgid, int result, void *user_data);
	// void (*properties_read_callback)(uint32_t msgid, uint8_t *data, size_t data_len,
	// 		void *user_data);
	// void (*properties_observe_callback)(uint32_t msgid, uint8_t *data, size_t data_len,
	// 		 void *user_data);
	// void (*properties_notifcation_callback)(uint32_t msgid, int n_properties,
	// 		property_t *proerties, void *user_data);

} HeyThings_handler_callback;

/**
 * HeyThings_handler_fd() - Get the libheythings handler file descriptor
 * 
 * @param[in] handler		The libheythings handler
 * @retval >=0: handler file descriptor
 * @retval <0: error code
 */
int HeyThings_handler_fd(HeyThings_handler_t *handler);

/**
 * HeyThings_handler_reconnect() - Reconnect to sdk
 * 
 * @param[in] handler		The libheythings handler
 * @retval >=0: connect success and return handler file descriptor
 * @retval <0: connect failed and return the error code
 */
int HeyThings_handler_reconnect(HeyThings_handler_t *handler);

/**
 * HeyThings_handler_init() - Init the libheythings handler
 *
 * @param[in] user_data	The user custon data
 * @param[in] callback		The callback struct ::HeyThings_handler_callback
 * @retval NULL: 			init failed
 * @retval !NULL:			init success
 */
HeyThings_handler_t *HeyThings_handler_init(void *user_data,
											HeyThings_handler_callback *callback);

/**
 * HeyThings_handler_finish() - Finish the libheythings handler
 *
 * @param[in] handler		The finish handler
 */
void HeyThings_handler_finish(HeyThings_handler_t *handler);

/**
 * HeyThings_handler_write() - Send data to SDK by handler
 *
 * @param[in] handler		The libheythings handler, init by ::HeyThings_handler_init 
 * @param[in] msg			Send message
 * @param[in] len 			Message length
 * @retval >0: 				The send succes bytes
 * @retval =0:				The handler will be close.
 * @retval <0:				Some error, see errno
 *
 */
int HeyThings_handler_write(HeyThings_handler_t *handler,
							uint8_t *msg, uint32_t len);

/**
 * HeyThings_get_properties() - get service proerties request
 * @param[in] handler		The libheythings handler, init by ::HeyThings_handler_init 
 * @param[out] msgid		The request message id
 * @param[in] properties	The get properties ::property_t array
 * @param[in] num			The get properties count
 * @retval see as ::HeyThings_handler_write
 */
int HeyThings_get_properties(HeyThings_handler_t *handler, uint32_t *msgid,
							 property_t properties[], uint32_t num);

/**
 * HeyThings_set_properties() - set service proerties request
 * @param[in] handler		The libheythings handler, init by ::HeyThings_handler_init 
 * @param[out] msgid		The request message id
 * @param[in] properties	The set properties ::property_t array
 * @param[in] num			The set properties count
 * @retval see as ::HeyThings_handler_write
 */
int HeyThings_set_properties(HeyThings_handler_t *handler, uint32_t *msgid,
							 property_t properties[], uint32_t num);

/**
 * HeyThings_observe_properties() - set service proerties request
 * @param[in] handler		The libheythings handler, init by ::HeyThings_handler_init 
 * @param[out] msgid		The request message id
 * @param[in] properties	The observe properties ::property_t array
 * @param[in] num			The observe properties count
 * @retval see as ::HeyThings_handler_write
 */
int HeyThings_observe_properties(HeyThings_handler_t *handler, uint32_t *msgid,
								 property_t properties[], uint32_t num);

/**
 * HeyThings_get_dev_status() - get device status request
 * @param[in] handler		The libheythings handler, init by ::HeyThings_handler_init 
 * @param[out] msgid		The request message id
 * @retval see as ::HeyThings_handler_write
 */
int HeyThings_get_dev_status(HeyThings_handler_t *handler, uint32_t *msgid);

/**
 * HeyThings_get_devinfo() - get device information from SDK
 * 
 * @param[in] handler		The libheythings handler, init by ::HeyThings_handler_init 
 * @param[out] msgid		The request message id
 * @retval see as ::HeyThings_handler_write
 */
int HeyThings_get_devinfo(HeyThings_handler_t *handler, uint32_t *msgid);

/**
 * HeyThings_subscibe_addr_msg() - subscibe the heything message by address request
 * 
 * @param[in] handler		The libheythings handler, init by ::HeyThings_handler_init 
 * @param[in] addr			The subscibe request address
 * @retval see as ::HeyThings_handler_write
 */
int HeyThings_subscibe_addr_msg(HeyThings_handler_t *handler, uint32_t addr);

/**
 * HeyThings_unsubscibe_addr_msg() - unsubscibe the heything message by address request
 * 
 * @param[in] handler		The libheythings handler, init by ::HeyThings_handler_init 
 * @param[in] addr			The unsubscibe address
 * @retval see as ::HeyThings_handler_write
 */
int HeyThings_unsubscibe_addr_msg(HeyThings_handler_t *handler, uint32_t addr);

/**
 * HeyThings_subscibe_type_msg() - subscibe the heything message by type request
 * 
 * @param[in] handler		The libheythings handler, init by ::HeyThings_handler_init 
 * @param[in] type			The subscibe type
 * @retval see as ::HeyThings_handler_write
 */
int HeyThings_subscibe_type_msg(HeyThings_handler_t *handler, uint32_t type);

/**
 * HeyThings_unsubscibe_type_msg() - unsubscibe the heything message by type request
 * 
 * @param[in] handler		The libheythings handler, init by ::HeyThings_handler_init 
 * @param[in] type			The unsubscibe type
 * @retval see as ::HeyThings_handler_write
 */
int HeyThings_unsubscibe_type_msg(HeyThings_handler_t *handler, uint32_t type);

/**
 * HeyThings_subscibe_siid_msg() - subscibe the heything message by service id request
 * 
 * @param[in] handler		The libheythings handler, init by ::HeyThings_handler_init 
 * @param[in] siid			The subscibe service id
 * @retval see as ::HeyThings_handler_write
 */
int HeyThings_subscibe_siid_msg(HeyThings_handler_t *handler, uint32_t siid);

/**
 * HeyThings_unsubscibe_siid_msg() - unsubscibe the heything message by service id request
 * 
 * @param[in] handler		The libheythings handler, init by ::HeyThings_handler_init 
 * @param[in] siid			The unsubscibe service id
 * @retval see as ::HeyThings_handler_write
 */
int HeyThings_unsubscibe_siid_msg(HeyThings_handler_t *handler, uint32_t siid);

/**
 * HeyThings_forward_msg() - Tell SDK forward the message
 *
 * @param[in] handler		The libheythings handler, init by ::HeyThings_handler_init() 
 * @param[in] msg			The message need SDK forward
 * @param[in] len			The message length
 * @retval see as ::HeyThings_handler_write
 */
int HeyThings_forward_msg(HeyThings_handler_t *handler, uint8_t *msg, int len);

/**
 * HeyThings_msg_send() - Tell SDK process the message
 *
 * @param[in] handler		The libheythings handler, init by ::HeyThings_handler_init 
 * @param[in] msg			The message need SDK process
 * @param[in] len			The message length
 * @retval see as ::HeyThings_handler_write
 */
int HeyThings_msg_send(HeyThings_handler_t *handler, uint8_t *msg, int len);

/**
 * HeyThings_handler_process() - The libheythings main process
 *
 *
 * @param[in] handler		The libheythings handler, init by ::HeyThings_handler_init 
 * 
 * @retval = 0:		Nothing need process, need continue to wait data 
 * @retval > 0:		Process success
 * @retval < 0:		Some error or SDK disconnect
 */
int HeyThings_handler_process(HeyThings_handler_t *handler);

/**
 * @brief Generate the device keypair.
 *
 * When the device use the one model one key, then the device first 
 * to connect cloud and bind, the cloud will send the device certificate 
 * to the device.
 *
 * Cloud need the device public key to generate the device certificate.
 * So other programs need to save the private key.
 *
 * @param[in] handler The libheythings handler, init by ::HeyThings_handler_init 
 * @param[out] msgid The message id.
 *
 * @retval = 0 : Generate key request success.
 * @retval < 0 : Request faile.
 */
int HeyThings_generate_dev_key(HeyThings_handler_t *handler, uint32_t *msgid);

/**
 * @brief Tell HeyThings SDK the device network is ready.
 *
 * Note: 	The HeyThings SDK network status default is ready.
 *			When use the AP model to recive the setup infmation,
 *			need to tell the sdk the network does't ready when the 
 *			device start the AP. 
 *			When other program tell the SDK network is ready, the 
 *			SDK will start bind or start to connect to cloud.
 *
 *
 * @param[in] hander
 * @param[in] ready
 *
 * @retval = 0 : Tell the SDK　success. 
 * @retval < 0 :  Some error happend, more see errno. 
 *
 *
 */

int HeyThings_network_ready(HeyThings_handler_t *handler, int ready);

/**
 * @brief Tell the SDK reset the bind status
 * 
 * Note: When the SDK status is't the BINDING and BIND_FAILED, 
 * the SDK after receiving the message will be do nothing.
 * 
 * @param[in] handler
 * 
 * @retval > 0: Do success.
 * @retval <= 0: Some error happend, more see errno.
 * 
 **/
int HeyThings_bind_reset(HeyThings_handler_t *handler);

/**
 * @brief Tell the SDK the program trigger single event.
 *
 * @param[in] handler
 * @param[in] event 
 *
 * @retval > 0: Success 
 * @retval <=0: 
 *
 **/
int HeyThings_trigger_event(HeyThings_handler_t *handler, event_t *event, uint32_t *event_t);

int HeyThings_action_return(HeyThings_handler_t *handler,
							action_t *action, uint32_t len, uint8_t *data);

int HeyThings_action_stream_msg(HeyThings_handler_t *handler,
								action_t *action, uint32_t seq, uint32_t len, uint8_t *data);

int HeyThings_action_stream_end(HeyThings_handler_t *handler, action_t *action);

//active
int HeyThings_action_call(HeyThings_handler_t *handler,
						  uint32_t *action_id, uint32_t siid, uint32_t iid,
						  uint32_t len, uint8_t *data);

int HeyThings_setup(HeyThings_handler_t *handler,
					uint8_t *bindkey, int bindkey_len,
					uint8_t *ssid, int ssid_len,
					uint8_t *psk, int psk_len,
					uint64_t timestamp,
					char *domain,
					uint8_t *cloud_url, int cloudurl_len,
					int n_access_url,
					uint8_t **acess_url, int *accessurl_len);

int HeyThings_connect_device(HeyThings_handler_t *handler,
							 uint32_t address, char *ip, int port);

int HeyThings_properties_write(HeyThings_handler_t *handler,
							   uint32_t *msgid, uint32_t address,
							   uint32_t siid, int n_iid, uint32_t *iid,
							   uint8_t *data, int data_len);

int HeyThings_properties_read(HeyThings_handler_t *handler,
							  uint32_t *msgid, uint32_t address,
							  uint32_t siid, int n_iid, uint32_t *iid);

int HeyThings_properties_observe(HeyThings_handler_t *handler,
								 uint32_t *msgid, uint32_t address,
								 uint32_t siid, int n_iid, uint32_t *iid);

/*
int HeyThings_action_start(HeyThings_handler_t *handler, 
		uint32_t *action_id, uint32_t siid, uint32_t iid, 
		uint32_t win, uint32_t len, uint8_t *data);

int HeyThings_action_msg(HeyThings_handler_t *handler, 
		uint32_t action_id, uint32_t seq, uint32_t win,
		uint32_t len, uint8_t *data); 

int HeyThings_action_end(HeyThings_handler_t *handler, 
		uint32_t action_id);
*/
#endif
