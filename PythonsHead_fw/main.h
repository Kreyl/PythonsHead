#pragma once

#include "ch.h"
#include "hal.h"
#include "kl_lib.h"
#include "uart.h"
#include "evt_mask.h"
//#include "kl_adc.h"
#include "board.h"

class App_t {
private:
    thread_t *PThread; // Main thread
public:
    void InitThread() { PThread = chThdGetSelfX(); }
    void SignalEvt(uint32_t EvtMsk) {
        chSysLock();
        chEvtSignalI(PThread, EvtMsk);
        chSysUnlock();
    }
    void SignalEvtI(eventmask_t Evt) { chEvtSignalI(PThread, Evt); }
    void OnCmd(Shell_t *PShell);
    // Inner use
    void ITask();
};
extern App_t App;

// Lauca is warmth, ringe is cold
class Laucaringe_t {
private:
    const PinOutputPWM_t IChnl;
    const PinOutput_t Dir1, Dir2;
    int32_t Target_mV;
public:
    void Init() {
        IChnl.Init();
        IChnl.Set(0);
        Dir1.Init();
        Dir2.Init();
    }

    void SetIntencity(int32_t Intencity) {
        if(Intencity > 0) {
            Dir1.SetHi();
            Dir2.SetLo();
        }
        else if(Intencity < 0) {
            Dir1.SetLo();
            Dir2.SetHi();
            Intencity = -Intencity;
        }
        else { // 0
            Dir1.SetLo();
            Dir2.SetLo();
        }
        IChnl.Set(Intencity);
    }

    void SetTarget_mV(int32_t TargetValue) {
        Target_mV = TargetValue;
        if(TargetValue > 0) {
            Dir1.SetHi();
            Dir2.SetLo();
            Target_mV = TargetValue;
        }
        else if(TargetValue < 0) {
            Dir1.SetLo();
            Dir2.SetHi();
            Target_mV = -TargetValue;
        }
        else {
            Dir1.SetLo();
            Dir2.SetLo();
            IChnl.Set(0);
        }
    }

    void Adjust_mV(int32_t Current_mV) {
        uint32_t CurrInt = IChnl.Get();
        if(Current_mV > Target_mV and CurrInt > 0) CurrInt--;
        else if(Current_mV < Target_mV and CurrInt < LR_PWM_TOP) CurrInt++;
//        Uart.Printf("Curr=%d; Target=%d; Int=%d\r", Current_mV, Target_mV, CurrInt);
        IChnl.Set(CurrInt);
    }

    Laucaringe_t(const PwmSetup_t APwmSetup, GPIO_TypeDef *APGPIO1, uint16_t APin1, GPIO_TypeDef *APGPIO2, uint16_t APin2) :
                IChnl(APwmSetup),
                Dir1(APGPIO1, APin1, omPushPull), Dir2(APGPIO2, APin2, omPushPull),
                Target_mV(0) {}
};

#define FILT_SZ     11

class Filter_t {
private:
    uint32_t Buf[FILT_SZ];
    uint32_t Cnt = 0;
public:
    void Put(uint32_t v) { if(Cnt < FILT_SZ) Buf[Cnt++] = v; }
    void Flush() { Cnt = 0; }
    bool IsReady() { return (Cnt == FILT_SZ); }
    uint32_t GetResult() { return FindMediana<uint32_t>(Buf, FILT_SZ);  }
};
