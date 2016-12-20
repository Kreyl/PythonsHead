/*
 * main.cpp
 *
 *  Created on: 20 февр. 2014 г.
 *      Author: g.kruglov
 */

#include "main.h"
#include "board.h"
#include "radio_lvl1.h"
#include "OmronD6Tt.h"

App_t App;

const PinOutput_t Led {LED_GPIO, LED_PIN, omPushPull};

#define PWM_TOP     255

PinOutputPWM_t Out0({PWM_GPIO, PWM1_PIN, PWM1_TIM, PWM1_CH, invNotInverted, omPushPull, PWM_TOP});
PinOutputPWM_t Out1({PWM_GPIO, PWM2_PIN, PWM2_TIM, PWM1_CH, invNotInverted, omPushPull, PWM_TOP});
PinOutputPWM_t Out2({PWM_GPIO, PWM3_PIN, PWM3_TIM, PWM1_CH, invNotInverted, omPushPull, PWM_TOP});
PinOutputPWM_t Out3({PWM_GPIO, PWM4_PIN, PWM4_TIM, PWM1_CH, invNotInverted, omPushPull, PWM_TOP});
PinOutputPWM_t Out4({PWM_GPIO, PWM5_PIN, PWM5_TIM, PWM1_CH, invNotInverted, omPushPull, PWM_TOP});
PinOutputPWM_t Out5({PWM_GPIO, PWM6_PIN, PWM6_TIM, PWM1_CH, invNotInverted, omPushPull, PWM_TOP});
PinOutputPWM_t Out6({PWM_GPIO, PWM7_PIN, PWM7_TIM, PWM1_CH, invNotInverted, omPushPull, PWM_TOP});
PinOutputPWM_t Out7({PWM_GPIO, PWM8_PIN, PWM8_TIM, PWM1_CH, invNotInverted, omPushPull, PWM_TOP});

const PinOutputPWM_t *Out[8] = {&Out0, &Out1, &Out2, &Out3, &Out4, &Out5, &Out6, &Out7};

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

    Led.Init();
    Led.SetHi();
    // PWM
    for(uint8_t i=0; i<8; i++) {
        Out[i]->Init();
        Out[i]->Set(18);
    }

    i2c2.Init();    // Sns uses it

    if(Radio.Init() != OK) {
        for(int i=0; i<18; i++) {
            Led.Toggle();
            chThdSleepMilliseconds(99);
        }
        Led.SetLo();
    }

    // Main cycle
    App.ITask();
}

__attribute__ ((__noreturn__))
void App_t::ITask() {
    while(true) {
        Led.SetLo();
        if(Sns.ReadData() == OK) {
            // Copy data for transmitting
            chSysLock();
            Radio.PktInfoTx.Cmd = 0;    // 0 means GetInfo
            for(uint8_t i=0; i<8; i++) Radio.PktInfoTx.t[i] = Sns.Data.Pix[i];
            chSysUnlock();

//            chEvtSignal(Radio.PThd, EVT_NEW_SNS_DATA);
//            Uart.Printf("%03d ", Sns.Data.Temperature);
//            for(uint8_t i=0; i<8; i++) Uart.Printf("%03d ", Sns.Data.Pix[i]);
//            Uart.Printf("\r\n");
//            CalculatePWM();
        }
        else Sns.Restart();
        Led.SetHi();
        chThdSleepMilliseconds(180);
    } // while true
}

uint8_t App_t::SetParam(uint8_t ParamID, uint8_t Value) {
    Uart.Printf("Param %u, v %u", ParamID, Value);
    return OK;
}


void CalculatePWM() {
    // Copy data to local buf
    for(uint8_t i=0; i<8; i++) LBuf[i] = Sns.Data.Pix[i];
    // Find min value
//    int16_t Min = 32000;
//    for(uint8_t i=0; i<8; i++) {
//        if(LBuf[i] < Min) Min = LBuf[i];
//    }

#define MIN_T   300

    for(uint8_t i=0; i<=7; i++) {
        LBuf[i] -= MIN_T;
        LBuf[i] *= 2;
        if(LBuf[i] > 255) LBuf[i] = 255;
        if(LBuf[i] < 0) LBuf[i] = 0;
    }

    LBuf[2] = 0;
    LBuf[3] = 0;
    LBuf[4] = 0;
    LBuf[5] = 0;


    for(uint8_t i=0; i<8; i++) Out[i]->Set(LBuf[i]);

//    // Subtract Min
//    //for(uint8_t i=0; i<8; i++) LBuf[i] -= Min;
//    int32_t Pwm1, Pwm2;
//    Pwm1 = LBuf[7] - Min;
//    Pwm2 = LBuf[0] - Min;
//    // Multiply
//    Pwm1 = (Pwm1 * 25) / 10;
//    Pwm2 = (Pwm2 * 25) / 10;
//    // Limit value
//    if(Pwm1 > 255) Pwm1 = 255;
//    if(Pwm2 > 255) Pwm2 = 255;
//    Uart.Printf("Min=%d; Pwm1=%d; Pwm2=%d\r", Min, Pwm1, Pwm2);
//    Out1.Set(Pwm1);
//    Out2.Set(Pwm2);
}
