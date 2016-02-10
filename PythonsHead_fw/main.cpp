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

#define PWM_TOP     255

PinOutputPWM_t<PWM_TOP, invNotInverted, omPushPull> Out1({PWM_GPIO, PWM7_PIN, PWM7_TIM, PWM7_CH});
PinOutputPWM_t<PWM_TOP, invNotInverted, omPushPull> Out2({PWM_GPIO, PWM8_PIN, PWM8_TIM, PWM8_CH});

static void CalculatePWM();
static int32_t LBuf[8];

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
    Out1.Init();
    Out2.Init();

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
            CalculatePWM();
        }
        else Sns.Restart();
        Led.Hi();
        chThdSleepMilliseconds(180);
    } // while true
}


void CalculatePWM() {
    // Copy data to local buf
    for(uint8_t i=0; i<8; i++) LBuf[i] = Sns.Data.Pix[i];
    // Find min value
    int16_t Min = 32000;
    for(uint8_t i=0; i<8; i++) {
        if(LBuf[i] < Min) Min = LBuf[i];
    }
    // Subtract Min
    //for(uint8_t i=0; i<8; i++) LBuf[i] -= Min;
    int32_t Pwm1, Pwm2;
    Pwm1 = LBuf[7] - Min;
    Pwm2 = LBuf[0] - Min;
    // Multiply
    Pwm1 = (Pwm1 * 25) / 10;
    Pwm2 = (Pwm2 * 25) / 10;
    // Limit value
    if(Pwm1 > 255) Pwm1 = 255;
    if(Pwm2 > 255) Pwm2 = 255;
    Uart.Printf("Min=%d; Pwm1=%d; Pwm2=%d\r", Min, Pwm1, Pwm2);
    Out1.Set(Pwm1);
    Out2.Set(Pwm2);
}
