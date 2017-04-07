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

#define LED_CNT     8
LedSmooth_t *Led[LED_CNT] = {&Led0, &Led1, &Led2, &Led3, &Led4, &Led5, &Led6, &Led7};

Laucaringe_t LrA (LR_A_PWM, LR_A_DIR1, LR_A_DIR2);
Laucaringe_t LrB (LR_B_PWM, LR_B_DIR1, LR_B_DIR2);
Laucaringe_t LrC (LR_C_PWM, LR_C_DIR1, LR_C_DIR2);
Laucaringe_t LrD (LR_D_PWM, LR_D_DIR1, LR_D_DIR2);
Laucaringe_t LrE (LR_E_PWM, LR_E_DIR1, LR_E_DIR2);
Laucaringe_t LrF (LR_F_PWM, LR_F_DIR1, LR_F_DIR2);
Laucaringe_t LrG (LR_G_PWM, LR_G_DIR1, LR_G_DIR2);
Laucaringe_t LrH (LR_H_PWM, LR_H_DIR1, LR_H_DIR2);

#define LR_CNT      8
Laucaringe_t *Lr[LR_CNT] = { &LrA, &LrB, &LrC, &LrD, &LrE, &LrF, &LrG, &LrH };

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
    for(uint8_t i=0; i<LED_CNT; i++) Led[i]->Init();
    for(uint8_t i=0; i<LED_CNT; i++) {
        Led[i]->StartOrRestart(lsqStart);
        chThdSleepMilliseconds(270);
    }

    // Laucaringi
    for(uint8_t i=0; i<LR_CNT; i++) {
        Lr[i]->Init();
        Lr[i]->Set(25);
    }

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
//    Uart.Printf("\r%S\r", PCmd->Name);
    // Handle command
    if(PCmd->NameIs("Ping")) PShell->Ack(retvOk);

    else if(PCmd->NameIs("Set")) {
        int32_t Indx, Value;
        if(PCmd->GetParams<int32_t>(2, &Indx, &Value) == retvOk) {
            if(Indx > 7) PShell->Ack(retvCmdError);
            else {
                Lr[Indx]->Set(Value);
            }
        }
        else PShell->Ack(retvCmdError);
    }

    else PShell->Ack(retvCmdUnknown);
}
#endif
