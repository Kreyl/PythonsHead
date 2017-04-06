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
