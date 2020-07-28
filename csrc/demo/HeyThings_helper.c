
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>
#include <unistd.h>
#include <netdb.h>
#include <netinet/in.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <arpa/inet.h>
#include <netinet/ip.h>
#include <sys/select.h>
#include <sys/un.h>
#include <errno.h>
#include <signal.h>
#include <sys/time.h>

#include "HeyThings_mem.h"
#include "HeyThings_log.h"
#include "HeyThings_interface.h"
#include "devinfo_platform.h"

typedef struct
{
	int running;
	HeyThings_handler_t *handler;
	devinfo_t info;
	dev_status_t status;
} help_t;

void properties_cb(uint32_t msgid, uint32_t count,
				   property_t proerties[], void *user_data)
{
	return;
}

void set_properties_cb(uint32_t msgid, int result,
					   void *user_data)
{
	return;
}

void properties_change(uint32_t count, property_t proerties[],
					   void *user_data)
{
	return;
}

void heythings_msg_cb(uint8_t *msg, int len, void *user_data)
{
	return;
}

void dev_status_cb(dev_status_t status, void *user_data)
{
	ht_log_debug("recv the sdk status is %d", status);

	help_t *help = (help_t *)user_data;
	help->status = status;

	switch (status)
	{
	case DEV_STATUS_UNKNOW:
	case DEV_STATUS_BIND_FAILED:
		//need reboot the dev or sdk
		ht_log_info("Bind failed or status unknow. Need rebooting..");
		break;
	case DEV_STATUS_WAIT_DEVINFO:
		//send the read devinfo to the sdk
		HeyThings_network_ready(help->handler, 0);
		ht_log_info("Wait devinfo...");
		if (help->info.filed[DEVINFO_TYPE_DEV_PRI_KEY].s)
		{
			uint16_t dilen = 0;
			uint8_t *di = HeyThings_devinfo_pack(&help->info, 0, &dilen);
			HeyThings_handler_write(help->handler, di, (uint32_t)dilen);
			ht_xfree(di);
		}
		else
		{
			ht_log_debug("Wait the dev pri key.");
			HeyThings_generate_dev_key(help->handler, NULL);
		}
		break;
	case DEV_STATUS_WAIT_BINDINFO:
		//network start and wait the BINDINFO
		ht_log_debug("Wait BindInfo");
#if 0 
			HeyThings_setup(help->handler, 
					"7e08e314f7454f7e859ad1c11110edb4", 32,
					NULL, 0, 
					NULL, 0,
					123456, 
					"test", 
					NULL, 0, 
					0, NULL, NULL);
#endif
		break;
	case DEV_STATUS_BINDING:
		ht_log_debug("Binding....");
		break;
	case DEV_STATUS_WAIT_NETWORK_READY:
		ht_log_debug("Wait network ready.");
		HeyThings_network_ready(help->handler, 1);
		break;
	case DEV_STATUS_CLOUD_CONNECTING:
		ht_log_debug("Connecting cloud.....");
		break;
	case DEV_STATUS_NORMAL:
		ht_log_debug("Normal.....");
		break;
	case DEV_STATUS_RESET:
		string_free(&help->info.filed[DEVINFO_TYPE_BIND_STATUS]);
		string_assign(&help->info.filed[DEVINFO_TYPE_BIND_STATUS], "0", 1);
		save_devinfo(&help->info);
		break;
	}
}

void dev_info_cb(devinfo_t *info, void *user_data)
{
	ht_log_debug("Recv the devinfo...");

	int i = 0;
	help_t *help = (help_t *)user_data;

	for (i = 0; i < DEVINFO_TYPE_MAX; i++)
	{
		if (info->filed[i].s &&
			info->filed[i].len > 0)
		{
			ht_log_debug("%d(%d): %s", i, info->filed[i].len, info->filed[i].s);
			string_cpy(&help->info.filed[i], &info->filed[i]);
		}
	}

	devinfo_out(&help->info);
	save_devinfo(&help->info);
}

void pin_code_cb(char *pin, int len, void *user_data)
{
	ht_log_info("The pin code is: %s, len: %d", pin, len);
}

void dev_time_cb(struct timeval *ts, void *user_data)
{
	//Sync the system time.
}

void client_disconnect_cb(uint32_t addr, int reason, void *user_data)
{
	ht_log_info("The client(%lld) has disconnect by %d.", addr, reason);
	return;
}

void client_connected_cb(uint32_t addr, void *user_data)
{
	ht_log_info("The client(%lld) has connected.", addr);
	return;
}

void subscribe_cb(int result, void *user_data)
{
	return;
}

void dev_key_cb(char *pri_key_pem, uint32_t len, void *user_data)
{
	help_t *help = (help_t *)user_data;
	if (!pri_key_pem || help->status != DEV_STATUS_WAIT_DEVINFO)
	{
		ht_log_err("The key can't use.");
		return;
	}

	ht_log_debug("dev pri key pem: %s", pri_key_pem);

	if (save_1m1k_pri_key(pri_key_pem, len) == 0)
	{
		string_assign(&(help->info.filed[DEVINFO_TYPE_DEV_PRI_KEY]),
					  pri_key_pem, strlen(pri_key_pem));

		uint16_t dilen = 0;
		uint8_t *di = HeyThings_devinfo_pack(&help->info, 0, &dilen);
		HeyThings_handler_write(help->handler, di, (uint32_t)dilen);
		ht_xfree(di);
	}
	return;
}

static HeyThings_handler_callback ht_cb = {
	.get_properties_callback = properties_cb,
	.set_properties_callback = set_properties_cb,
	.observe_properties_callback = properties_cb,
	.properties_change_callback = properties_change,
	.heythings_msg_callback = heythings_msg_cb,
	.dev_status_callback = dev_status_cb,
	.pin_code_callback = pin_code_cb,
	.dev_info_callback = dev_info_cb,
	.client_disconnect_callback = client_disconnect_cb,
	.subscibe_callback = subscribe_cb,
	.generate_dev_key_callback = dev_key_cb,
};

int main(int argc, char *argv[])
{
	help_t help = {0x00};
	help.running = 1;

	int flag = 0;
	if (argc >= 1 && argv[1] && strcmp(argv[1], "--1m1k") == 0)
	{
		ht_log_debug("One model one key.....");
		flag = 1;
	}

	ht_log_info("libHeythings version: %d.%d.%d",
				HEYTHINGS_VERSION_MAJOR, HEYTHINGS_VSERION_MINOR,
				HEYTHINGS_VERSION_PATCH);

	signal(SIGPIPE, SIG_IGN);

	devinfo_t info = {0x00};
	if (get_devinfo(&help.info, flag) < 0)
	{
		ht_log_err("Can't read the devinfo.");
		return -1;
	}

	devinfo_out(&help.info);

	help.handler = HeyThings_handler_init((void *)&help, &ht_cb);
	if (!help.handler)
	{
		ht_log_err("HeyThings handler init error.");
		return 0;
	}

	fd_set rfds;
	int max_fd = 0, ret = 0;
	uint32_t reconnect_times = 0;
	int sdk_fd = HeyThings_handler_fd(help.handler);
	while (help.running)
	{
		//ht_log_debug("sdk fd =%d\n", sdk_fd);
		FD_ZERO(&rfds);
		if (sdk_fd >= 0)
		{
			FD_SET(sdk_fd, &rfds);
			max_fd = sdk_fd + 1;
		}
		struct timeval timeout = {1, 500};
		ret = select(max_fd, &rfds, NULL, NULL, &timeout);
		if (ret == 0)
		{ //time out
			//	ht_log_debug("time out...");
			if (sdk_fd < 0)
			{ //reconnect to sdk
				sdk_fd = HeyThings_handler_reconnect(help.handler);
				if (sdk_fd < 0)
				{
					reconnect_times++;
				}
				else
				{
					reconnect_times = 0;
				}
			}
			//time out to update the devInfo
		}
		else if (ret < 0)
		{
			//some error
			ht_log_err("select has error: %s\n", strerror(errno));
		}
		else
		{
			if (FD_ISSET(sdk_fd, &rfds))
			{
				ret = HeyThings_handler_process(help.handler);
				ht_log_debug("HeyThings handler return %d\n", ret);
				if (ret <= 0)
				{
					close(sdk_fd);
					sdk_fd = -1;
				}
			}
		}
	}
	HeyThings_handler_finish(help.handler);
	return 0;
}
