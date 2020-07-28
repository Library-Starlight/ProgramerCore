#include <stdio.h>
#include <HeyThings_interface.h>

int main(void)
{
    ht_log_info("libHeythings version: %d.%d.%d",
                HEYTHINGS_VERSION_MAJOR, HEYTHINGS_VSERION_MINOR,
                HEYTHINGS_VERSION_PATCH);

    return 0;
}