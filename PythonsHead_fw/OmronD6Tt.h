/*
 * OmronD6Tt.h
 *
 *  Created on: 9 февр. 2016 г.
 *      Author: Kreyl
 */

#pragma once

#include "kl_lib.h"

struct SnsData_t {
    int16_t Temperature;
    int16_t Pix[8];
    uint8_t Pec;
};
#define SNS_DATA_SZ     sizeof(SnsData_t)

#define SNS_I2C_ADDR    0x0A

class OmronD6T_t {
public:
    SnsData_t Data;
    void Init();
    void Restart();
    uint8_t ReadData();
};

extern OmronD6T_t Sns;
