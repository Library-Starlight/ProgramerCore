#include <stdio.h>
#include <HeyThings_interface.h>

void properties_cb(uint32_t msgid, uint32_t count,
                   property_t properties[], void *user_data)
{
    printf("properties_cb");
}

void set_properties_cb(uint32_t msgid, int result, void *user_data)
{
    printf("set_properties_cb");
}

void properties_change(uint32_t count, property_t properties[],
                       void *user_data)
{
    printf("properties_change");
}

void heythings_msg_cb(uint8_t *msg, int len, void *user_data)
{
    printf("heythings_msg_cb");
}

void dev_status_cb(dev_status_t status, void *user_data)
{
    printf("dev_status_cb");
}

void pin_code_cb(char *pin_code, int len, void *user_data)
{
    printf("pin_code_cb");
}

void client_disconnect_cb(uint32_t client, int reason, void *user_data)
{
    printf("client_disconnect_cb");
}

void dev_info_cb(devinfo_t *info, void *user_data)
{
    printf("dev_info_cb");
}

void subscribe_cb(int result, void *user_data)
{
    printf("subscribe_cb");
}

void dev_key_cb(char *pri_key_pem, uint32_t pri_key_len,
                void *user_data)
{
    printf("dev_key_cb");
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

typedef struct
{
    /* data */
    int running;
    HeyThings_handler_t *handler;
    devinfo_t info;
    dev_status_t status;
} gateway;

int main(void)
{
    printf("libHeythings version: %d.%d.%d\n",
           HEYTHINGS_VERSION_MAJOR, HEYTHINGS_VSERION_MINOR,
           HEYTHINGS_VERSION_PATCH);

    gateway hd = {0x00};
    hd.running = 1;
    hd.handler = HeyThings_handler_init((void *)&hd, &ht_cb);

    // printf("libHeythings handler: %s\n", hd.handler);
}