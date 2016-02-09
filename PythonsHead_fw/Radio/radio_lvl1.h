/*
 * radio_lvl1.h
 *
 *  Created on: Nov 17, 2013
 *      Author: kreyl
 */

#pragma once

#include "kl_lib.h"
#include "ch.h"
#include "rlvl1_defins.h"
#include "cc1101.h"

class rLevel1_t {
private:
    rPkt_t Pkt;
    void TryToSleep(uint32_t SleepDuration) {
        if(SleepDuration >= MIN_SLEEP_DURATION_MS) CC.EnterPwrDown();
        chThdSleepMilliseconds(SleepDuration);
    }
public:
    thread_t *PThd;
    uint8_t Init();
    // Inner use
    void ITask();
};

extern rLevel1_t Radio;
