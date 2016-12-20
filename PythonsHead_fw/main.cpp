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
LedIndication_t LedIndication;

const PinOutput_t LedState {LED_GPIO, LED_PIN, omPushPull};

#define PWM_TOP     100

PinOutputPWM_t Out0({PWM_GPIO, PWM1_PIN, PWM1_TIM, PWM1_CH, invNotInverted, omPushPull, PWM_TOP});
PinOutputPWM_t Out1({PWM_GPIO, PWM2_PIN, PWM2_TIM, PWM2_CH, invNotInverted, omPushPull, PWM_TOP});
PinOutputPWM_t Out2({PWM_GPIO, PWM3_PIN, PWM3_TIM, PWM3_CH, invNotInverted, omPushPull, PWM_TOP});
PinOutputPWM_t Out3({PWM_GPIO, PWM4_PIN, PWM4_TIM, PWM4_CH, invNotInverted, omPushPull, PWM_TOP});
PinOutputPWM_t Out4({PWM_GPIO, PWM5_PIN, PWM5_TIM, PWM5_CH, invNotInverted, omPushPull, PWM_TOP});
PinOutputPWM_t Out5({PWM_GPIO, PWM6_PIN, PWM6_TIM, PWM6_CH, invNotInverted, omPushPull, PWM_TOP});
PinOutputPWM_t Out6({PWM_GPIO, PWM7_PIN, PWM7_TIM, PWM7_CH, invNotInverted, omPushPull, PWM_TOP});
PinOutputPWM_t Out7({PWM_GPIO, PWM8_PIN, PWM8_TIM, PWM8_CH, invNotInverted, omPushPull, PWM_TOP});

const PinOutputPWM_t *Led[8] = {&Out0, &Out1, &Out2, &Out3, &Out4, &Out5, &Out6, &Out7};

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

    // LEDs
    LedState.Init();
    LedState.SetHi();
    for(uint8_t i=0; i<8; i++) {
        Led[i]->Init();
        Led[i]->Set(0);
        Led[i]->SetFrequencyHz(4);
    }

    i2c2.Init();    // Sns uses it

    if(Radio.Init() != OK) {
        for(int i=0; i<18; i++) {
            LedState.Toggle();
            chThdSleepMilliseconds(99);
        }
        LedState.SetLo();
    }

    // Main cycle
    App.ITask();
}

__noreturn
void App_t::ITask() {
    while(true) {
        LedState.SetLo();
        if(Sns.ReadData() == OK) {
            // Copy data for transmitting
            chSysLock();
            Radio.PktInfoTx.Cmd = 0;    // 0 means GetInfo
            for(uint8_t i=0; i<SNS_T_CNT; i++) Radio.PktInfoTx.t[i] = Sns.Data.Pix[i];
            chSysUnlock();

//            Uart.Printf("%03d ", Sns.Data.Temperature);
//            for(uint8_t i=0; i<8; i++) Uart.Printf("%03d ", Sns.Data.Pix[i]);
//            Uart.Printf("\r\n");
            // Process indication
            LedIndication.Process(&Sns.Data.Pix[0]);
        }
        else Sns.Restart();
        LedState.SetHi();
        chThdSleepMilliseconds(180);
    } // while true
}

uint8_t App_t::SetParam(uint8_t ParamID, uint8_t Value) {
    Uart.Printf("Param %u, v %u\r", ParamID, Value);
    switch(ParamID) {
        case parSetChannels:     LedIndication.EnableMsk = Value; break;
        case parSetTTop:         LedIndication.TTop = Value; break;
        case parSetTBottom:      LedIndication.TBottom = Value; break;
        case parSetBlinkFTop:    LedIndication.FreqTop = Value; break;
        case parSetBlinkFBottom: LedIndication.FreqBottom = Value; break;
        default: return CMD_UNKNOWN;
    }
    return OK;
}

#if 1 // ========================= LED indication ==============================
void LedIndication_t::Process(int16_t *pt) {
    for(int i=0; i<SNS_T_CNT; i++) {
        // Check if this channel required
        if(IChEnabled(i)) {
            int32_t t = pt[i];
//            Uart.Printf("%d\r", t);
            // Saturate temperature
            int32_t Top = TTop * 10, Bottom = TBottom * 10;
            if(t > Top) t = Top;
            if(t < Bottom) t = Bottom;
            // Calculate proportion
            int32_t FreqHz = Proportion<int32_t>(Bottom, Top, FreqBottom, FreqTop, t);
            Uart.Printf("%u  t=%d, f=%d\r", i, t, FreqHz);
            // Setup LED
            Led[i]->Set(50);
            Led[i]->SetFrequencyHz(FreqHz);
        }
        else Led[i]->Set(0);
    }
}
#endif
