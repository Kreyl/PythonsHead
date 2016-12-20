/*
 * main.h
 *
 *  Created on: 15 сент. 2014 г.
 *      Author: g.kruglov
 */

#pragma once

#include "ch.h"
#include "kl_lib.h"
#include "uart.h"
#include "evt_mask.h"

#define APP_NAME                "Python's Head"
#define APP_VERSION             __DATE__ " " __TIME__

enum ParamID_t {
    parSetChannels = 0,
    parSetTTop = 1, parSetTBottom = 2,
    parSetBlinkFTop = 3, parSetBlinkFBottom = 4,
};

class LedIndication_t {
private:
    bool IChEnabled(int n) { return (EnableMsk & (1 << (7 - n))); }
public:
    // Params
    int32_t FreqBottom = 1, FreqTop = 36;
    uint8_t EnableMsk = 0xFF;
    int32_t TBottom = 18, TTop = 36;
    void Process(int16_t *pt);
};

class App_t {
private:
    thread_t *PThread;
public:
    uint8_t SetParam(uint8_t ParamID, uint8_t Value);
    // Eternal methods
    void InitThread() { PThread = chThdGetSelfX(); }
    void SignalEvt(eventmask_t Evt) {
        chSysLock();
        chEvtSignalI(PThread, Evt);
        chSysUnlock();
    }
    void SignalEvtI(eventmask_t Evt) { chEvtSignalI(PThread, Evt); }
    void OnCmd(Shell_t *PShell);
    // Inner use
    void ITask();
};

extern App_t App;
