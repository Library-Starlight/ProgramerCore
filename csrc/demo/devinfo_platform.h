#ifndef _DEVINFO_PLATFORM_H
#define _DEVINFO_PLATFORM_H

#include "HeyThings_devinfo.h"

int get_devinfo(devinfo_t *info, int flag);
int save_devinfo(devinfo_t *info);
int devinfo_read_original_bin(devinfo_t *info, char *filename);
int save_1m1k_pri_key(char *pem, uint32_t len);

#endif
