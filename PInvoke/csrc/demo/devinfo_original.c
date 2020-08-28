#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <errno.h>

#include "HeyThings_log.h"
#include "HeyThings_mem.h"
#include "HeyThings_devinfo.h"

#pragma pack(1)

#define DEV_SN_BYTES            32
#define DEV_DID_BYTES           8
#define MAX_PIN_BYTES           16
#define MAX_CERTS_IN_CHAIN      5

typedef struct partition_dat {
	unsigned char  version;             // start from 01.
	unsigned char  one_dev_one_secert;  // one decice one secert = 1 / one produce one secert = 0.
	unsigned char  has_pin;             // have pin = 1  add device by EC-JPAKE. otherwise ECDH
	unsigned char  resv[1];             // reserved for future.
	unsigned int   tot_len;             //total length of all partition data.

	unsigned int   ecc1;                //crc-32 , set to 0 before calculating.
	unsigned int   ecc2;

	unsigned char  sn[ DEV_SN_BYTES + 1 ];
	unsigned char  resv2[ 7 ];          //sn filed align with 8

	unsigned char did[ DEV_DID_BYTES + 1 ];
	unsigned char resv3[ 7 ];           //did filed aligh with 8

	unsigned char pin[MAX_PIN_BYTES + 1];
	unsigned char resv4[ 7 ];           // pin filed aligh with 8

	unsigned int  dev_pri_key_len;      //device cert private key length, include the last '\0'
	unsigned int  dev_cert_len;         //device cert.
	unsigned int  ecdh_pri_key_len;  //product cert private key.
	unsigned int  product_cert_len;     //product cert.

	unsigned int  ca_chain_num;         //ca certificates in chain
	unsigned int  ca_size[ MAX_CERTS_IN_CHAIN ]; //each ca cert size.

	unsigned char resv5[ 4 ];
}partition_dat_t;

int devinfo_read_original_bin(devinfo_t *info, char *filename)
{
	int root_ca_len = 0, i, ret = -1;
	char *dev_pri_key = NULL;
	char *dev_cert = NULL;
	char *ecdh_pri_key = NULL;
	char *product_cert = NULL;
	char *root_ca = NULL;

	partition_dat_t part = {0x00};
	FILE *fp = fopen(filename, "rb");
	if (!fp) {
		ht_log_err("fopen %s error: %s\n", filename, strerror(errno));
		return 0;
	}
	if (fread((void *)&part, sizeof(part), 1, fp) != 1) {
		ht_log_err("fread header error: %s\n", strerror(errno));
		goto read_bin_end;
	}

	info->filed[DEVINFO_TYPE_DEV_PRI_KEY].s = ht_malloc( part.dev_pri_key_len);
	info->filed[DEVINFO_TYPE_DEV_PRI_KEY].len =  part.dev_pri_key_len-1;
	info->filed[DEVINFO_TYPE_DEV_CERT].s = ht_malloc(part.dev_cert_len);
	info->filed[DEVINFO_TYPE_DEV_CERT].len = part.dev_cert_len-1;
	info->filed[DEVINFO_TYPE_ECDH_PRI_KEY].s = ht_malloc(part.ecdh_pri_key_len);
	info->filed[DEVINFO_TYPE_ECDH_PRI_KEY].len = part.ecdh_pri_key_len-1;
	info->filed[DEVINFO_TYPE_PRODUCT_CERT].s = ht_malloc(part.product_cert_len);
	info->filed[DEVINFO_TYPE_PRODUCT_CERT].len = part.product_cert_len-1;

	root_ca_len = 0;
	for (i = 0; i < part.ca_chain_num; i++) {
		root_ca_len += part.ca_size[i]-1;
	}
	info->filed[DEVINFO_TYPE_ROOT_CERT].s = ht_malloc(root_ca_len+1);
	info->filed[DEVINFO_TYPE_ROOT_CERT].len = root_ca_len;

	if (!info->filed[DEVINFO_TYPE_DEV_PRI_KEY].s ||
		!info->filed[DEVINFO_TYPE_DEV_CERT].s ||
		!info->filed[DEVINFO_TYPE_ECDH_PRI_KEY].s ||
		!info->filed[DEVINFO_TYPE_PRODUCT_CERT].s ||
		!info->filed[DEVINFO_TYPE_ROOT_CERT].s) {
		ht_log_err("OOM at %s:%s:%d",
				__FILE__, __func__, __LINE__);
		goto read_bin_end;
	}

#define xfread(p, len) \
	do {\
		if (fread(p, len, 1, fp) != 1) {\
			ht_log_err("fread %s error: %s\n", #p, strerror(errno));\
			goto read_bin_end;\
		}\
	}while (0)
	
		xfread(info->filed[DEVINFO_TYPE_DEV_PRI_KEY].s, part.dev_pri_key_len);
		xfread(info->filed[DEVINFO_TYPE_DEV_CERT].s, part.dev_cert_len);
		xfread(info->filed[DEVINFO_TYPE_ECDH_PRI_KEY].s, part.ecdh_pri_key_len);
		xfread(info->filed[DEVINFO_TYPE_PRODUCT_CERT].s, part.product_cert_len);
	
	root_ca_len = 0;
	for (i = 0; i < part.ca_chain_num; i++) {
		xfread(info->filed[DEVINFO_TYPE_ROOT_CERT].s + root_ca_len,
				part.ca_size[i] );
		root_ca_len += part.ca_size[i];
	}


	string_assign(&info->filed[DEVINFO_TYPE_DID], 
			part.did, strlen(part.did));

	/*
	ht_log_debug("did: %s\n", info->filed[DEVINFO_TYPE_DID].s);
	ht_log_debug("dev_pri_key[%d]: %s\n", part.dev_pri_key_len, info->filed[DEVINFO_TYPE_DEV_PRI_KEY].s);
	ht_log_debug("dev_cert: %s\n", info->filed[DEVINFO_TYPE_DEV_CERT].s);
	ht_log_debug("ecdh_pri_key: %s\n", info->filed[DEVINFO_TYPE_ECDH_PRI_KEY].s);
	ht_log_debug("product_cert: %s\n", info->filed[DEVINFO_TYPE_PRODUCT_CERT].s);
	ht_log_debug("root_ca: %s\n", info->filed[DEVINFO_TYPE_ROOT_CERT].s);
	*/
	ret = 0;

read_bin_end:

	if (ret == -1) {
		string_free(&info->filed[DEVINFO_TYPE_DEV_PRI_KEY]);
		string_free(&info->filed[DEVINFO_TYPE_DEV_CERT]);
		string_free(&info->filed[DEVINFO_TYPE_ECDH_PRI_KEY]);
		string_free(&info->filed[DEVINFO_TYPE_PRODUCT_CERT]);
		string_free(&info->filed[DEVINFO_TYPE_ROOT_CERT]);
		string_free(&info->filed[DEVINFO_TYPE_DID]);
	}
	if (fp) {
		fclose(fp);
		fp = NULL;
	}
	return ret;
}
