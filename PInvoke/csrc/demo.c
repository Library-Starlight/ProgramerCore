#include "stdio.h"
#include "HeyThings_interface.h"
#include "HeyThings_log.h"
#include "devInfo.pb-c.h"

int main(void)
{
    Pack();

    // printf("libHeythings version: %d.%d.%d\n",
    //        HEYTHINGS_VERSION_MAJOR, HEYTHINGS_VSERION_MINOR,
    //        HEYTHINGS_VERSION_PATCH);

    // uint64_t v = *(uint64_t *)"value";
    // printf(v);

    return 0;
}

struct _Iot__Service__DevInfo__Properties
{
    ProtobufCMessage base;
    char *devname;
    char *manufacturer;
    char *model;
    char *brand;
    char *softver;
    uint64_t did;
    uint32_t pid;
    uint32_t cid;
    char *mac;
    char *hardver;
    char *sn;
    uint64_t parentdid;
    int32_t rssi;
    char *bssid;
    char *ip;
    char *devtime;
    uint64_t utc;
    char *timezone;
    char *ssid;
    /*
   *用于上传云端进行家庭签名
   */
    ProtobufCBinaryData devpubkey;
    /*
   *dev identify in cloud
   */
    char *vendordevid;
    Iot__Service__DevInfo__DevCapability *devcapability;
    /*
   *是否在线，0-离线, 1-在线
   */
    protobuf_c_boolean online;
};

typedef struct _Iot__Service__DevInfo__Properties Iot__Service__DevInfo__Properties;

size_t iot__service__dev_info__properties__pack(const Iot__Service__DevInfo__Properties *message,
                                                uint8_t *out)
{
    assert(message->base.descriptor == &iot__service__dev_info__properties__descriptor);
    return protobuf_c_message_pack((const ProtobufCMessage *)message, out);
}

void Pack()
{
    Iot__Service__DevInfo__Properties props = IOT__SERVICE__DEV_INFO__PROPERTIES__INIT;

    props.online = 1;
    
    iot__service__dev_info__properties__pack(&props, buf);
}

void Print()
{
    printf("Hello World!\n");
}

typedef enum
{
    state1,
    state2,
    state3
} status_enum;

void Invoke(status_enum status)
{
    printf("Invoke Successded: %d\n", status);
}

typedef struct StatusCallback
{
    void (*online_callback)(uint32_t value);
    void (*ext_callback1)(uint32_t value);
    void (*ext_callback2)(uint32_t value);
    void (*ext_callback3)(uint32_t value);
    void (*ext_callback4)(uint32_t value);
    void (*ext_callback5)(uint32_t value);
    void (*ext_callback6)(uint32_t value);
    void (*ext_callback7)(uint32_t value);
    void (*ext_callback8)(uint32_t value, char v);
    // void (*ext_callback9)(uint32_t value);
} StatusCallback;

void SetCallback(StatusCallback *callback)
{
    (*callback).online_callback(500);
    //  callback.ext_callback8(500, 'c');
    // callback.ext_callback1(100);
    // callback.ext_callback2(200);
    // callback.ext_callback3(300);
}

// void SetCallback(StatusCallback callback)
// {
//     (callback).online_callback(500);
//     //  callback.ext_callback8(500, 'c');
//     // callback.ext_callback1(100);
//     // callback.ext_callback2(200);
//     // callback.ext_callback3(300);
// }

// 一个复杂的结构体参数

typedef struct child
{
    int32_t len;
    uint8_t *ptr;
} child_string;

typedef struct unique
{
    /* data */
    child_string array[2];
} unique_string;

void SendString(unique_string str, int len)
{
    printf("Hello World!\n");
    printf("Length1: %d\n", str.array->len);
    printf("Length2: %d\n", str.array[1].len);
}

void SendStringPointer(unique_string *str, int len)
{
    printf("Hello World!\n");

    printf("Length1: %d\n", str->array->len);
    printf("Length2: %d\n", str->array[1].len);
}