#include "stdio.h"
#include "HeyThings_interface.h"
#include "HeyThings_log.h"

int main(void)
{
    printf("libHeythings version: %d.%d.%d\n",
           HEYTHINGS_VERSION_MAJOR, HEYTHINGS_VSERION_MINOR,
           HEYTHINGS_VERSION_PATCH);

    return 0;
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
} StatusCallback;

void SetCallback(StatusCallback callback)
{
    callback.online_callback(500);
}