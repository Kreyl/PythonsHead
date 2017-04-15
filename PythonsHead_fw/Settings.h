/*
 * eeSettings.h
 *
 *  Created on: 8 дек. 2016 г.
 *      Author: Kreyl
 */

#pragma once

#include "inttypes.h"
#include "uart.h"

// EEAddresses
#define EE_ADDR_SETTINGS        0

enum ParamID_t {
        parChannelsCnt = 0,
        parTTop = 1, parTBottom = 2, parValueTop = 3, parValueBottom = 4,
        parFreq = 5
};
#define PARAM_CNT   6

// Settings defaults

class Settings_t {
private:
    uint8_t IWriteCommon(uint32_t Offset, uint8_t Data);
    uint8_t IWriteCommon(uint32_t Offset, int32_t Data);
public:
    // ==== Values ====
    int32_t ParamsLed[PARAM_CNT], ParamsLR[PARAM_CNT];
    // ==== Methods ====
    void Init();
    void Print() {
        Uart.Printf("Settings:\rLeds ");
        for(int i=0; i<PARAM_CNT; i++) Uart.Printf("%d ", ParamsLed[i]);
        Uart.Printf("\rLRs  ");
        for(int i=0; i<PARAM_CNT; i++) Uart.Printf("%d ", ParamsLR[i]);
        Uart.Printf("\r");
    }
    uint8_t Read();
    uint8_t SetLedParam(int32_t ParID, int32_t Value) {
        ParamsLed[ParID] = Value;
        return IWriteCommon(ParID * 4, Value);
    }
    uint8_t SetLRParam(ParamID_t ParID, int32_t Value) {
        ParamsLed[ParID] = Value;
        return IWriteCommon(ParID * 4, Value);
    }
//    uint8_t SetID(uint8_t NewID)             { ID = NewID; return IWriteCommon(0, NewID); }
//    uint8_t SetType(DeviceType_t NewType)    { Type = NewType; return IWriteCommon(1, (uint8_t)NewType); }
//    uint8_t SetGroup(uint8_t NewGroup)       { Group = NewGroup; return IWriteCommon(2, NewGroup); }
//    uint8_t SetIRPower(uint8_t NewIRPower)   { IRPower = NewIRPower; return IWriteCommon(3, NewIRPower); }
//    uint8_t SetIRDamage(uint8_t NewIRDamage) { IRDamage = NewIRDamage; return IWriteCommon(4, NewIRDamage); }
//    uint8_t SetHitCount(uint8_t NewHitCount) { HitCount = NewHitCount; return IWriteCommon(5, NewHitCount); }
//    uint8_t SetShotCount(uint8_t NewShotCount) { ShotCount = NewShotCount; return IWriteCommon(6, ShotCount); }
//    uint8_t SetReloadTime(uint8_t NewReloadTime) { ReloadTime = NewReloadTime; return IWriteCommon(7, NewReloadTime); }
//    uint8_t SetRepairTime(uint8_t NewRepairTime) { RepairTime = NewRepairTime; return IWriteCommon(8, NewRepairTime); }
//    uint8_t SetState(uint8_t NewState)       { State = NewState; return IWriteCommon(9, State); }
//
//    uint8_t SetAllExceptID(uint8_t *ptr);
};

#define SETTINGS_SZ     sizeof(Settings_t)
