#ifndef _HEYTHINGS_DEVINFO_H
#define _HEYTHINGS_DEVINFO_H

#include <stdint.h>

/**
 * devino_type_t - The device information type
 */

typedef enum {
	DEVINFO_TYPE_DID,
	DEVINFO_TYPE_PID,
	DEVINFO_TYPE_CID,
	DEVINFO_TYPE_VID, //vendor device id
	DEVINFO_TYPE_MAC,
	DEVINFO_TYPE_MODEL,
	DEVINFO_TYPE_MANUFACTURER,
	DEVINFO_TYPE_BRAND,
	DEVINFO_TYPE_SN,
	DEVINFO_TYPE_ROOT_CERT,
	DEVINFO_TYPE_DEV_CERT,
	DEVINFO_TYPE_DEV_PRI_KEY,
	DEVINFO_TYPE_PRODUCT_PRI_KEY,
	DEVINFO_TYPE_ECDH_PRI_KEY,
	DEVINFO_TYPE_PRODUCT_CERT,
	DEVINFO_TYPE_PIN,
	DEVINFO_TYPE_DEV_NAME,
	DEVINFO_TYPE_BIND_STATUS,
	DEVINFO_TYPE_CONNECT_TYPE,
	DEVINFO_TYPE_CONFIG_TYPE,
	DEVINFO_TYPE_SOFT_VER,
	DEVINFO_TYPE_HARD_VER,
	DEVINFO_TYPE_PARENT_DID,
	DEVINFO_TYPE_SSID,
	DEVINFO_TYPE_PSK,
	DEVINFO_TYPE_BSSID,
	DEVINFO_TYPE_RSSI,
	DEVINFO_TYPE_IP,
	DEVINFO_TYPE_NETMASK,
	DEVINFO_TYPE_GATEWAY,
	DEVINFO_TYPE_DEV_TIME,
	DEVINFO_TYPE_TIMEZONE,
	DEVINFO_TYPE_LOGITUDE,
	DEVINFO_TYPE_LATITUDE,
	DEVINFO_TYPE_CLOUD_URL,
	DEVINFO_TYPE_ACCESS_URL,
	DEVINFO_TYPE_HOMEID,
	DEVINFO_TYPE_HOMEADDRESS,
	DEVINFO_TYPE_HOMESIGN,
	DEVINFO_TYPE_HOMEPUBKEY,
	DEVINFO_TYPE_MASTER_KEY,
	DEVINFO_TYPE_EVENT_UUID_SEQ,
	DEVINFO_TYPE_MAX
}devinfo_type_t;

/**
 * string_t - The device information store struct
 */
typedef struct {
	uint16_t len;
	uint8_t *s;
}string_t;

/**
 * devino_t - The device information
 */
typedef struct devinfo {
	string_t field[DEVINFO_TYPE_MAX];
}devinfo_t;

#define __CHECK_FILED(i, x) (i->field[x].s && i->field[x].len > 0)

/**
 * string_cpy() - string copy, copies the src string to dst
 * 
 * @param[in] dst The destination string ::string_t 
 * @param[in] src The srouce string ::string_t
 *
 * @retval 1: if copy success
 * @return 0: if copy failed
 */
int string_cpy(string_t *dst, string_t *src);

/**
 * string_assign() - assign the string, char* to string_t
 * 
 * @param[out] s The out string ::string_t 
 * @param[in] ts The char* string
 * @param[in] len The char* string length
 *
 * @retval 1: if copy success
 * @return 0: if copy failed
 */
int string_assign(string_t *s, char *ts, int len);

/**
 * string_free() - free the string_t
 *
 * @param[in] s Need free string
 */
void string_free(string_t *s);

/**
 * HeyThings_devinfo_pack() - Pack the ::devinfo_t to heythings binray data
 *
 * @param[in] info	The pack ::devinfo_t information.
 * @param[in] msgid The pack message id
 * @param[out] len  The packed message length
 *
 * @retval NULL: if pack failed
 * @retval uint8_t: if pack success
 */
uint8_t *HeyThings_devinfo_pack(devinfo_t *info, uint32_t msgid, uint16_t *len);

/**
 * HeyThings_devinfo_unpack() - Unpack the heythings binray data to ::devinfo_t
 *
 * @param[out] info	The unpacked ::devinfo_t information
 * @param[in] buf The unpack message
 * @param[in] len  The pack message length
 *
 * @retval NULL: if pack failed
 * @retval uint8_t: if pack success
 */
int HeyThings_devinfo_unpack(devinfo_t *info, uint8_t *buf, uint16_t len);

/**
 * check_least_devinfo() - The SDK least device information
 *
 * @param[in] info The check ::devinfo_t
 * 
 * @retval 0 if check failed
 * @return 1 if check success
 */
int check_least_devinfo(devinfo_t *info);

/**
 * HeyThings_devinfo_free() - Free the ::devinfo_t
 *
 * @param[in] info
 */
void HeyThings_devinfo_free(devinfo_t *info);

void devinfo_out(devinfo_t *info);

#endif
