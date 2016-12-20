/*
 * OmronD6Tt.h
 *
 *  Created on: 9 февр. 2016 г.
 *      Author: Kreyl
 */

#pragma once

#include "kl_lib.h"
#include "kl_i2c.h"

#define SNS_T_CNT   8

struct SnsData_t {
    int16_t Temperature;
    int16_t Pix[SNS_T_CNT];
    uint8_t Pec;
};
#define SNS_DATA_SZ     sizeof(SnsData_t)

#define SNS_I2C_ADDR    0x0A

class OmronD6T_t {
private:
    i2c_t *i2c;
public:
    SnsData_t Data;
    void Restart();
    uint8_t ReadData();
    OmronD6T_t(i2c_t *pi2c) : i2c(pi2c) {}
};

extern OmronD6T_t Sns;
