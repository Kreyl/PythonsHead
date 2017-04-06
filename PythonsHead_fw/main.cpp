/*
 * File:   main.cpp
 * Author: Elessar
 * Project: MasonOrder
 *
 * Created on May 27, 2016, 6:37 PM
 */

#include "main.h"
#include "kl_lib.h"
#include "led.h"
#include "Sequences.h"

App_t App;
LedOnOff_t LedState(LED_PIN);

LedSmooth_t Led0 (LED_1);
LedSmooth_t Led1 (LED_2);
LedSmooth_t Led2 (LED_3);
LedSmooth_t Led3 (LED_4);
LedSmooth_t Led4 (LED_5);
LedSmooth_t Led5 (LED_6);
LedSmooth_t Led6 (LED_7);
LedSmooth_t Led7 (LED_8);

//#define LED_CNT     8
//LedSmooth_t *Led[LED_CNT] = {&Led0, &Led1, &Led2, &Led3, &Led4, &Led5, &Led6, &Led7};


int main() {
    // ==== Setup clock ====
    Clk.SetHiPerfMode();
    Clk.UpdateFreqValues();

    // ==== Init OS ====
    halInit();
    chSysInit();
    App.InitThread();

    // ==== Init Hard & Soft ====
    Uart.Init(115200, UART_GPIO, UART_TX_PIN, UART_GPIO, UART_RX_PIN);
    Uart.Printf("\r%S %S\r\n", APP_NAME, BUILD_TIME);
    Clk.PrintFreqs();

    // LEDs
    LedState.Init();
    LedState.On();
//    for(uint8_t i=0; i<LED_CNT; i++) Led[i]->Init();
//    for(uint8_t i=0; i<LED_CNT; i++) {
//        Led[i]->StartOrRestart(lsqStart);
//        chThdSleepMilliseconds(270);
//    }

    // ==== Main cycle ====
    App.ITask();
}

__noreturn
void App_t::ITask() {
    while(true) {
//        Uart.Printf("a");
//        chThdSleepMilliseconds(540);
        uint32_t Evt = chEvtWaitAny(ALL_EVENTS);

        if(Evt & EVT_UART_NEW_CMD) {
            OnCmd((Shell_t*)&Uart);
            Uart.SignalCmdProcessed();
        }
    } // while true
}

#if 1 // ======================= Command processing ============================
void App_t::OnCmd(Shell_t *PShell) {
    Cmd_t *PCmd = &PShell->Cmd;
    __unused int32_t dw32 = 0;  // May be unused in some configurations
//    Uart.Printf("\r%S\r", PCmd->Name);
    // Handle command
    if(PCmd->NameIs("Ping")) PShell->Ack(retvOk);

    else PShell->Ack(retvCmdUnknown);
}
#endif
