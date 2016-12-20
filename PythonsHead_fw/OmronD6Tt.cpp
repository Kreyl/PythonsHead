/*
 * OmronD6Tt.cpp
 *
 *  Created on: 9 ����. 2016 �.
 *      Author: Kreyl
 */

#include "OmronD6Tt.h"
#include "board.h"

OmronD6T_t Sns(&i2c2);

const uint8_t Cmd = 0x4C;

uint8_t OmronD6T_t::ReadData() {
    return i2c->WriteRead(SNS_I2C_ADDR, (uint8_t*)&Cmd, 1, (uint8_t*)&Data, SNS_DATA_SZ);
}

void OmronD6T_t::Restart() {
    i2c->Reset();
}
