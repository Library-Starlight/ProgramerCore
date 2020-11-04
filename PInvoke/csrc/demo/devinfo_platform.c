#include <stdio.h>
#include <stdlib.h>
#include <stdint.h>
#include <string.h>
#include <strings.h>
#include <ctype.h>
#include <errno.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <unistd.h>

#include "HeyThings_log.h"
#include "HeyThings_mem.h"
#include "HeyThings_devinfo.h"

#include "devinfo_platform.h"

#define DEV_INFO_FILE_BIN "devinfo.bin"
#define DEV_INFO_CERT_BIN "devinfo_cert.bin"
static const char *devinfo_type_string[DEVINFO_TYPE_MAX] = {
	"did",
	"pid",
	"cid",
	"vid",
	"mac",
	"model",
	"manufacturer",
	"brand",
	"sn",
	"root_cert",
	"dev_cert",
	"dev_pri_key",
	"dev_pub_key",
	"ecdh_pri_key",
	"product_cert",
	"pin",
	"dev_name",
	"bind_status",
	"connect_type",
	"config_type",
	"soft_ver",
	"hard_ver",
	"parent_did",
	"ssid",
	"psk",
	"bssid",
	"rssi",
	"ip",
	"netmask",
	"gateway",
	"dev_time",
	"timezone",
	"logitude",
	"latitude",
	"cloud_url",
	"access_url",
	"home_id",
	"home_address",
	"home_sign",
	"home_pubkey"
};


static int get_devinfo_bin(devinfo_t *info)
{
	int i = 0;
	struct stat statbuf = {0x00};
	stat(DEV_INFO_FILE_BIN, &statbuf);
	off_t fsize = statbuf.st_size;
	FILE *fp = fopen(DEV_INFO_FILE_BIN, "rb");
	if (!fp ) {
		ht_log_err("File open devinfo bin file error: %s\n", strerror(errno));
		return -1;
	}
	char *buf = ht_malloc(fsize);
	if (!buf) {
		return -1;
	}
	fread(buf, fsize, 1, fp);
	fclose(fp);

	HeyThings_devinfo_unpack(info, buf, fsize);
	for (i = DEVINFO_TYPE_DID; i < DEVINFO_TYPE_MAX; i++ ) {
		ht_log_debug("%s(%d-%d): %s", devinfo_type_string[i],
				i, info->filed[i].len, info->filed[i].s);
	}
	ht_xfree(buf);
	return 0;
}

static int save_devinfo_bin(devinfo_t *info)
{
	FILE *fp = fopen(DEV_INFO_FILE_BIN, "wb+");
	if (!fp) {
		ht_log_err("fopen %s error: %s\n", 
				DEV_INFO_FILE_BIN, strerror(errno));
		return -1;
	}
	uint16_t len = 0;
	uint8_t *data = HeyThings_devinfo_pack(info, 0, &len); 
	if (data) {
		ht_log_debug("Save the devinfo bin.");
		//ht_debug_hex(data+sizeof(uint32_t)*3, len-sizeof(uint32_t)*3);
		fwrite(data+sizeof(uint32_t)*3, len-sizeof(uint32_t)*3, 1,fp);
	}
	ht_xfree(data);
	fclose(fp);
	return 0;
}

#define PID "rtaK"
#define MAC "B8:C9:B5:E5:C0:3D"
#define MODEL_DEV_PRI_KEY "OneModelOneKey/dev_pri.key"
#define MODEL_DEV_CERT "OneModelOneKey/dev.cert"
#define MODEL_PRODUCT_CERT "OneModelOneKey/product.cert"
#define MODEL_PRODUCT_PRI_KEY "OneModelOneKey/product_pri.key"
#define MODEL_ECDH_PRI_KEY "OneModelOneKey/ecdh_pri.key"
#define MODEL_ROOT_CERT "OneModelOneKey/root.cert"

int save_1m1k_pri_key(char *pem, uint32_t len)
{
	struct stat statbuf = {0x00};
	int ret = stat(MODEL_DEV_PRI_KEY, &statbuf);
	off_t fsize = statbuf.st_size;
	
	if (ret == 0 && fsize > 0) {
		ht_log_err("The dev pri key has the data. Please check the data.");
		return -1;
	}

	FILE *fp = fopen(MODEL_DEV_PRI_KEY, "w+");
	if (!fp) {
		ht_log_err("Can't open the %s file: %s",
				MODEL_DEV_PRI_KEY, strerror(errno));
		return -2;
	}

	ht_log_debug("the pri pem is :%s", pem);

	fwrite(pem, strlen(pem), 1, fp);
	fclose(fp);
	return 0;
}

static int read_file(char *filename, devinfo_t *info, devinfo_type_t type)
{
	struct stat statbuf = {0x00};
	stat(filename, &statbuf);
	off_t fsize = statbuf.st_size;
	if (fsize == 0) {
		ht_log_err("The file no data.");
		return -1;
	}
	FILE *fp = fopen(filename, "rb");
	if (!fp ) {
		ht_log_err("File open %s file error: %s\n", filename, strerror(errno));
		return -1;
	}
	char *buf = ht_malloc(fsize+1);
	if (!buf) {
		ht_log_err("OOM at %s:%s:%d",
				__FILE__, __func__, __LINE__);
		fclose(fp);
		return -1;
	}
	fread(buf, fsize, 1, fp);
	fclose(fp);

	string_free(&info->filed[type]);
	string_assign(&info->filed[type], buf, fsize+1);
	return 0;
}

int get_1m1k_devinfo(devinfo_t *info)
{
	string_assign(&info->filed[DEVINFO_TYPE_MAC], MAC, sizeof(MAC));
	string_assign(&info->filed[DEVINFO_TYPE_PID], PID, sizeof(PID));

	if (read_file(MODEL_DEV_CERT, info, DEVINFO_TYPE_DEV_CERT)) {
		return -1;
	}

	if (read_file(MODEL_PRODUCT_CERT, info, DEVINFO_TYPE_PRODUCT_CERT)) {
		return -1;
	}

	if (read_file(MODEL_PRODUCT_PRI_KEY, info, DEVINFO_TYPE_PRODUCT_PRI_KEY)) {
		return -1;
	}

	if (read_file(MODEL_ECDH_PRI_KEY, info, DEVINFO_TYPE_ECDH_PRI_KEY)) {
		return -1;
	}

	if (read_file(MODEL_ROOT_CERT, info, DEVINFO_TYPE_ROOT_CERT)) {
		return -1;
	}
	
	read_file(MODEL_DEV_PRI_KEY, info, DEVINFO_TYPE_DEV_PRI_KEY);
	return 0;
}

int get_devinfo(devinfo_t *info, int flag)
{
	if (get_devinfo_bin(info)) {
		if (flag) {
			if (get_1m1k_devinfo(info)) {
				ht_log_err("get one model one key information error.");
				return -1;
			}
		} else {
			if (!devinfo_read_original_bin(info, DEV_INFO_CERT_BIN)) {
				if (!info->filed[DEVINFO_TYPE_PID].s) {
#define TEST_PID "sAdO"
					string_assign(&info->filed[DEVINFO_TYPE_PID],
							TEST_PID, strlen(TEST_PID));
				}
			}
		}
	}
	return 0;
}

int save_devinfo(devinfo_t *info) 
{
	return save_devinfo_bin(info);
}
