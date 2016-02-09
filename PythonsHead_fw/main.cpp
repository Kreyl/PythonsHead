/*
 * main.cpp
 *
 *  Created on: 20 февр. 2014 г.
 *      Author: g.kruglov
 */

#include "main.h"
#include "board.h"
#include "clocking.h"
#include "radio_lvl1.h"
#include "OmronD6Tt.h"

App_t App;

const PinOutput_t Led {LED_GPIO, LED_PIN};

int main(void) {
    // ==== Init Vcore & clock system ====
    SetupVCore(vcore1V2);
    Clk.UpdateFreqValues();

    // Init OS
    halInit();
    chSysInit();
    App.InitThread();

    // ==== Init hardware ====
    Uart.Init(115200, UART_GPIO, UART_TX_PIN);//, UART_GPIO, UART_RX_PIN);
    Uart.Printf("\r%S %S\r", APP_NAME, APP_VERSION);
    Clk.PrintFreqs();

    Led.Init(omPushPull);
    Led.Hi();
    Sns.Init();

    if(Radio.Init() != OK) {
        for(int i=0; i<11; i++) {
            Led.Toggle();
            chThdSleepMilliseconds(99);
        }
        Led.Lo();
    }

    // Main cycle
    App.ITask();
}

__attribute__ ((__noreturn__))
void App_t::ITask() {
    while(true) {
        Led.Lo();
        if(Sns.ReadData() == OK) {
            chEvtSignal(Radio.PThd, EVT_NEW_SNS_DATA);
            Uart.Printf("%03d ", Sns.Data.Temperature);
            for(uint8_t i=0; i<8; i++) Uart.Printf("%03d ", Sns.Data.Pix[i]);
            Uart.Printf("\r\n");
        }
        else Sns.Restart();
        Led.Hi();
        chThdSleepMilliseconds(180);

    } // while true
}
