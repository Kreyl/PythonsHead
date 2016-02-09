/*
 * OmronD6Tt.cpp
 *
 *  Created on: 9 февр. 2016 г.
 *      Author: Kreyl
 */

#include "OmronD6Tt.h"
#include "board.h"

OmronD6T_t Sns;

i2c_t i2c (I2C_SNS, SNS_I2C_GPIO, SNS_I2C_SCL_PIN, SNS_I2C_SDA_PIN, 400000, I2C_SNS_DMA_TX, I2C_SNS_DMA_RX );
const uint8_t Cmd = 0x4C;

void OmronD6T_t::Init() {
    i2c.Init();
//    i2c.BusScan();
}

uint8_t OmronD6T_t::ReadData() {
    return i2c.WriteRead(SNS_I2C_ADDR, (uint8_t*)&Cmd, 1, (uint8_t*)&Data, SNS_DATA_SZ);
}

void OmronD6T_t::Restart() {
    i2c.Reset();
}
