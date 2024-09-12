#include <hidapi/hidapi.h>
#include <stdio.h>

extern "C" {

    int HID_API_EXPORT hidapi_init() {
        return hid_init();
    }

    int HID_API_EXPORT hidapi_exit() {
        return hid_exit();
    }

    hid_device* HID_API_EXPORT hidapi_open(unsigned short vendor_id, unsigned short product_id, const wchar_t *serial_number) {
        return hid_open(vendor_id, product_id, serial_number);
    }

    int HID_API_EXPORT hidapi_close(hid_device *device) {
        hid_close(device);
        return 0;
    }

    // Other functions you need...

}