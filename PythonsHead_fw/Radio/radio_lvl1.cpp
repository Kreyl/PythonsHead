/*
 * radio_lvl1.cpp
 *
 *  Created on: Nov 17, 2013
 *      Author: kreyl
 */

#include "radio_lvl1.h"
#include "evt_mask.h"
#include "main.h"
#include "cc1101.h"
#include "uart.h"
//#include "led.h"

//#define DBG_PINS

#ifdef DBG_PINS
#define DBG_GPIO1   GPIOB
#define DBG_PIN1    4
#define DBG1_SET()  PinSet(DBG_GPIO1, DBG_PIN1)
#define DBG1_CLR()  PinClear(DBG_GPIO1, DBG_PIN1)
#define DBG_GPIO2   GPIOB
#define DBG_PIN2    9
#define DBG2_SET()  PinSet(DBG_GPIO2, DBG_PIN2)
#define DBG2_CLR()  PinClear(DBG_GPIO2, DBG_PIN2)
#else
#define DBG1_SET()
#define DBG1_CLR()
#endif

rLevel1_t Radio;

#if 1 // ================================ Task =================================
static THD_WORKING_AREA(warLvl1Thread, 256);
__noreturn
static void rLvl1Thread(void *arg) {
    chRegSetThreadName("rLvl1");
    Radio.ITask();
}

__noreturn
void rLevel1_t::ITask() {
    while(true) {
        // Receive cmd
        uint8_t RxRslt = CC.Receive(36, &PktRx, &Rssi);
        if(RxRslt == OK) {
//            Uart.Printf("Rssi=%d\r", Rssi);
            // Process cmd
            if(PktRx.Cmd == 0) {    // GetInfo
                PktInfoTx.Cmd = 0;
                CC.Transmit(&PktInfoTx);
            }
            else if(PktRx.Cmd == 1) {   // Set param
                PktTx.Cmd = PktRx.Cmd;
                PktTx.Result = App.SetParam(PktRx.Data1, PktRx.Data2);
                CC.Transmit(&PktTx);
            }
        } // if ok
//        chThdSleepMilliseconds(999);
    }
}
#endif // task

#if 1 // ============================
uint8_t rLevel1_t::Init() {
#ifdef DBG_PINS
    PinSetupOut(DBG_GPIO1, DBG_PIN1, omPushPull);
    PinSetupOut(DBG_GPIO2, DBG_PIN2, omPushPull);
#endif    // Init radioIC
    if(CC.Init() == OK) {
        CC.SetTxPower(CC_PwrPlus5dBm);
        CC.SetPktSize(RPKT_LEN);
        CC.SetChannel(3);
//        CC.EnterPwrDown();
        // Thread
        PThd = chThdCreateStatic(warLvl1Thread, sizeof(warLvl1Thread), HIGHPRIO, (tfunc_t)rLvl1Thread, NULL);
        return OK;
    }
    else return FAILURE;
}
#endif
