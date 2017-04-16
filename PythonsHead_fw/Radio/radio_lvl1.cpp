/*
 * radio_lvl1.cpp
 *
 *  Created on: Nov 17, 2013
 *      Author: kreyl
 */

#include <Radio/cc1101.h>
#include <Radio/radio_lvl1.h>
#include "evt_mask.h"
#include "main.h"
#include "uart.h"

cc1101_t CC(CC_Setup0);

//#define DBG_PINS

#ifdef DBG_PINS
#define DBG_GPIO1   GPIOB
#define DBG_PIN1    0
#define DBG1_SET()  PinSetHi(DBG_GPIO1, DBG_PIN1)
#define DBG1_CLR()  PinSetLo(DBG_GPIO1, DBG_PIN1)
//#define DBG_GPIO2   GPIOB
//#define DBG_PIN2    9
//#define DBG2_SET()  PinSet(DBG_GPIO2, DBG_PIN2)
//#define DBG2_CLR()  PinClear(DBG_GPIO2, DBG_PIN2)
#else
#define DBG1_SET()
#define DBG1_CLR()
#endif

rLevel1_t Radio;
//void OnRadioRx();

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
        if(RxRslt == retvOk) {
//            Uart.Printf("Rssi=%d\r", Rssi);
            // Process cmd
            switch(PktRx.Cmd) {
                case 0: // GetInfo
                    PktInfoTx.Cmd = 0;
                    CC.Transmit(&PktInfoTx);
                    break;
                case 1: // Set param
                    PktTx.Cmd = PktRx.Cmd;
                    PktTx.Result = retvOk; // Who cares what really happened here...
                    CC.Transmit(&PktTx);
                    App.SetParam(PktRx.ParamID, PktRx.Value);
                    break;
                case 2: // Get param
                    PktTx.Cmd = PktRx.Cmd;
                    PktTx.Result = App.GetParam(PktRx.ParamID, &PktTx.Value);
                    CC.Transmit(&PktTx);
                    break;
                default: break;
            } // switch
        } // if ok
    } // while
}
#endif // task

#if 1 // ============================
uint8_t rLevel1_t::Init() {
#ifdef DBG_PINS
    PinSetupOut(DBG_GPIO1, DBG_PIN1, omPushPull);
//    PinSetupOut(DBG_GPIO2, DBG_PIN2, omPushPull);
#endif    // Init radioIC
    if(CC.Init() == retvOk) {
        CC.SetTxPower(CC_PwrPlus5dBm);
        CC.SetPktSize(RPKT_LEN);
        CC.SetChannel(3);
        // Thread
        chThdCreateStatic(warLvl1Thread, sizeof(warLvl1Thread), HIGHPRIO, (tfunc_t)rLvl1Thread, NULL);
        return retvOk;
    }
    else return retvFail;
}
#endif
