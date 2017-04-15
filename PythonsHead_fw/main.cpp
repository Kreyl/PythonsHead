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
#include "kl_adc.h"
#include "radio_lvl1.h"
#include "kl_i2c.h"
#include "Settings.h"

#if 1 // =========================== Locals ====================================
App_t App;
Settings_t Settings;

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

Filter_t Filt[LR_CNT];

#endif
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

    // Settings
    i2c2.Init();
//    i2c2.ScanBus();
    Settings.Init();
    Settings.Read();
    Settings.Print();

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
        Lr[i]->SetIntencity(0);
    }

    // ADC inputs
    PinSetupAnalog(LR_A_ADC);
    PinSetupAnalog(LR_B_ADC);
    PinSetupAnalog(LR_C_ADC);
    PinSetupAnalog(LR_D_ADC);
    PinSetupAnalog(LR_E_ADC);
    PinSetupAnalog(LR_F_ADC);
    PinSetupAnalog(LR_G_ADC);
    PinSetupAnalog(LR_H_ADC);
    PinSetupAnalog(BAT_MEAS_ADC);

    Adc.Init();
    Adc.EnableVRef();
    Adc.TmrInitAndStart();

    // Radio
    Radio.Init();

    // ==== Main cycle ====
    App.ITask();
}

__noreturn
void App_t::ITask() {
    while(true) {
        uint32_t Evt = chEvtWaitAny(ALL_EVENTS);

#if ADC_REQUIRED
        if(Evt & EVT_SAMPLING) Adc.StartMeasurement();
        if(Evt & EVT_ADC_DONE) {
            if(Adc.FirstConversion) Adc.FirstConversion = false;
            else {
//                uint32_t VBat_adc = Adc.GetResult(ADC_CHNL_BATTERY);
                uint32_t VRef_adc = Adc.GetResult(ADC_CHNL_VREFINT);
//                uint32_t Vbat_mv = (156 * Adc.Adc2mV(VBat_adc, VRef_adc)) / 56;   // Resistor divider 56k & 100k
//                Uart.Printf("VBat_adc: %u; Vref_adc: %u; VBat_mv: %u\r", VBat_adc, VRef_adc, Vbat_mv);
                uint32_t VLr[LR_CNT];
                bool Ready = false;
                for(int i=0; i<LR_CNT; i++) {
                    uint32_t v = Adc.GetResult(AdcChannels[i]);
                    v = Adc.Adc2mV(v, VRef_adc);
                    Filt[i].Put(v);
                    if(Filt[i].IsReady()) {
                        VLr[i] = Filt[i].GetResult();
                        Filt[i].Flush();
                        Ready = true;
                    }
                }

                if(Ready) {
//                    Uart.Printf("%u %u %u %u   %u %u %u %u\r", VLr[0],VLr[1],VLr[2],VLr[3], VLr[4],VLr[5],VLr[6],VLr[7]);
                    for(int i=0; i<LR_CNT; i++) Lr[i]->Adjust_mV(VLr[i]);
//                    Lr[0]->Adjust_mV(VLr[0]);
                }
//                int32_t Vbat_mv = (2 * Adc.Adc2mV(VBat_adc, VRef_adc));   // Resistor divider
//                if(Vbat_mv < 3500) SignalEvt(EVT_BATTERY_LOW);
            } // if not big diff
        } // evt
#endif

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

    else if(PCmd->NameIs("ee")) Settings.Read();

    else if(PCmd->NameIs("SetLed")) {
        int32_t Indx, Value;
        if(PCmd->GetParams<int32_t>(2, &Indx, &Value) == retvOk) {
            if(Indx >= 0 and Indx < PARAM_CNT) {
                PShell->Ack(Settings.SetLedParam(Indx, Value));
            }
            else PShell->Ack(retvCmdError);
        }
        else PShell->Ack(retvCmdError);
    }

    else if(PCmd->NameIs("SetI")) {
        int32_t Indx, Value;
        if(PCmd->GetParams<int32_t>(2, &Indx, &Value) == retvOk) {
            if(Indx > 7) PShell->Ack(retvCmdError);
            else {
                Lr[Indx]->SetIntencity(Value);
            }
        }
        else PShell->Ack(retvCmdError);
    }

    else if(PCmd->NameIs("SetA")) {
        int32_t Indx, Value;
        if(PCmd->GetParams<int32_t>(2, &Indx, &Value) == retvOk) {
            if(Indx > 7) PShell->Ack(retvCmdError);
            else {
                Lr[Indx]->SetTarget_mV(Value);
            }
        }
        else PShell->Ack(retvCmdError);
    }

    else PShell->Ack(retvCmdUnknown);
}
#endif
