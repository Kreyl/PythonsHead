/*
 * Settings.cpp
 *
 *  Created on: 8 дек. 2016 г.
 *      Author: Kreyl
 */

#include "Settings.h"
#include "kl_lib.h"
#include "kl_i2c.h"
#include "uart.h"
#include "ee.h"

#ifdef EE_PWR_PIN
const EE_t ee { &i2c3, EE_PWR_PIN };
#else
const EE_t ee { &i2c2 };
#endif

void Settings_t::Init() {
    ee.Init();
}

uint8_t Settings_t::Read() {
    uint8_t rslt = ee.Read(EE_ADDR_SETTINGS, this, SETTINGS_SZ);
//    if(ID < ID_MIN or ID > ID_MAX or rslt != retvOk) {
//        Uart.Printf("\rUsing default ID\r");
//        ID = ID_DEFAULT;
//    }
    return rslt;
}

uint8_t Settings_t::IWriteCommon(uint32_t Offset, uint8_t Data) {
    return ee.Write((EE_ADDR_SETTINGS + Offset), &Data, 1);
}
uint8_t Settings_t::IWriteCommon(uint32_t Offset, int32_t Data) {
    return ee.Write((EE_ADDR_SETTINGS + Offset), &Data, 4);
}

//uint8_t Settings_t::SetAllExceptID(uint8_t *ptr) {
//    uint8_t r = ee.Write((EE_ADDR_SETTINGS + 1), ptr, (SETTINGS_SZ - 1));
//    if(r == retvOk) {
//        uint8_t *innp = (uint8_t*)&Type;
//        for(uint32_t i=0; i<(SETTINGS_SZ - 1); i++) *innp++ = *ptr++;
//    }
//    return r;
//}
