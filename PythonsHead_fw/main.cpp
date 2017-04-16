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
#include "OmronD6Tt.h"
#include "ee.h"

#if 1 // =========================== Locals ====================================
App_t App;
TmrKL_t TmrSns {MS2ST(180), EVT_SNS, tktPeriodic};
const EE_t ee { &i2c2 };

enum ParamID_t {
    parSetChannels = 0,
    parSetTTop = 1, parSetTBottom = 2,
    parSetLedsTop = 3, parSetLedsBottom = 4,
    parFreq = 5,
    parEnableLeds = 6, parEnableLRs = 7,
    parLRPoint1T = 8, parLRPoint1Pwr = 9, parLRPoint2T = 10, parLRPoint2Pwr = 11,
};

#if 1 // ==== LEDs ====
LedOnOff_t LedState(LED_PIN);

PinOutputPWM_t Out0(LED_1);
PinOutputPWM_t Out1(LED_2);
PinOutputPWM_t Out2(LED_3);
PinOutputPWM_t Out3(LED_4);
PinOutputPWM_t Out4(LED_5);
PinOutputPWM_t Out5(LED_6);
PinOutputPWM_t Out6(LED_7);
PinOutputPWM_t Out7(LED_8);
#define LED_CNT     8
const PinOutputPWM_t *Led[LED_CNT] = {&Out0, &Out1, &Out2, &Out3, &Out4, &Out5, &Out6, &Out7};
#endif

#if 1 // ==== Laucaringi ====
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

int32_t EnableMsk = 0xFF;
bool ChEnabled(int n) { return (EnableMsk & (1 << (7 - n))); }

class LedIndication_t {
public:
    int32_t BrtBottom = 1, BrtTop = 100;
    int32_t TBottom = 18, TTop = 36;
    bool Enabled;
    void Process(int16_t *pt);
} LedIndication;

struct TemperaturePwrPoint_t {
    int32_t T, Pwr;
    void Check() {
        if(T < 11 or T > 80) T = 18;
        if(Pwr < -2000 or Pwr > 2000) Pwr = 0;
    }
};

class LRIndication_t {
public:
    TemperaturePwrPoint_t Point1, Point2;
    bool Enabled;
    void Process(int16_t *pt);
} LRIndication;

void LoadSettings();
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

    // Sns
    i2c1.Init();
//    i2c1.ScanBus();

    // Settings
    i2c2.Init();
//    i2c2.ScanBus();
    ee.Init();
    LoadSettings();

    // LEDs
    LedState.Init();
    LedState.On();
    for(uint8_t i=0; i<LED_CNT; i++) {
        Led[i]->Init();
        Led[i]->Set(0);
        Led[i]->SetFrequencyHz(4);
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

    TmrSns.InitAndStart();

    // ==== Main cycle ====
    App.ITask();
}

__noreturn
void App_t::ITask() {
    while(true) {
        uint32_t Evt = chEvtWaitAny(ALL_EVENTS);

        if(Evt & EVT_SNS) {
            LedState.Off();
            if(Sns.ReadData() == retvOk) {
                // Copy data for transmitting
                chSysLock();
                Radio.PktInfoTx.Cmd = 0;    // 0 means GetInfo
                for(uint8_t i=0; i<SNS_T_CNT; i++) Radio.PktInfoTx.t[i] = Sns.Data.Pix[i];
                chSysUnlock();
//                Uart.Printf("%03d ", Sns.Data.Temperature);
//                for(uint8_t i=0; i<8; i++) Uart.Printf("%03d ", Sns.Data.Pix[i]);
//                Uart.Printf("\r\n");
                // Process indication
                LedIndication.Process(&Sns.Data.Pix[0]);
                LRIndication.Process(&Sns.Data.Pix[0]);
            }
            else Sns.Restart();
            LedState.On();
        }

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

#if 1 // ============================= Indication ==============================
int32_t CalcProportion(int32_t TBottom, int32_t TTop, int32_t PwrBottom, int32_t PwrTop, int32_t t) {
    TTop *= 10;
    TBottom *= 10;
    // Saturate temperature
    if(t > TTop) t = TTop;
    if(t < TBottom) t = TBottom;
    return Proportion<int32_t>(TBottom, TTop, PwrBottom, PwrTop, t);
}


void LedIndication_t::Process(int16_t *pt) {
    if(Enabled) {
        for(int i=0; i<SNS_T_CNT; i++) {
            if(ChEnabled(i)) { // Check if this channel required
                int32_t Brt = CalcProportion(TBottom, TTop, BrtBottom, BrtTop, pt[i]);
//                Uart.Printf("%u  t=%d, v=%d\r", i, t, Brt);
                Led[i]->Set(Brt);
            }
            else Led[i]->Set(0);
        }
    }
    else for(uint8_t i=0; i<LED_CNT; i++) Led[i]->Set(0);
}

void LRIndication_t::Process(int16_t *pt) {
    if(Enabled) {
        for(int i=0; i<SNS_T_CNT; i++) {
            if(ChEnabled(i)) { // Check if this channel required
                int32_t Pwr = CalcProportion(Point1.T, Point2.T, Point1.Pwr, Point2.Pwr, pt[i]);
                Lr[i]->SetTarget_mV(Pwr);
            }
            else Lr[i]->SetTarget_mV(0);
        }
    }
    else for(uint8_t i=0; i<LR_CNT; i++) Lr[i]->SetTarget_mV(0);
}
#endif

#if 1 // ============================== Settings ===============================
#define ADDR_Channels       0

#define ADDR_LedsFreq       4
#define ADDR_BrtBottom      8
#define ADDR_BrtTop         12
#define ADDR_TBottom        16
#define ADDR_TTop           20
#define ADDR_LedsEnabled    24

#define ADDR_Point1_T       28
#define ADDR_Point1_Pwr     32
#define ADDR_Point2_T       36
#define ADDR_Point2_Pwr     40
#define ADDR_LREnabled      44

void LoadSettings() {
    int32_t En = 0;
    // Common
    ee.Read<int32_t>(ADDR_Channels, &EnableMsk);
    if(EnableMsk < 0 or EnableMsk > 0xFF) EnableMsk = 0xFF;
    // Leds
    int32_t Freq;
    ee.Read<int32_t>(ADDR_LedsFreq, &Freq);
    if(Freq < 1 or Freq > 4000) Freq = 4;
    for(uint8_t i=0; i<8; i++) Led[i]->SetFrequencyHz(Freq);

    ee.Read<int32_t>(ADDR_BrtBottom, &LedIndication.BrtBottom);
    if(LedIndication.BrtBottom < 0 or LedIndication.BrtBottom > 100) LedIndication.BrtBottom = 0;
    ee.Read<int32_t>(ADDR_BrtTop, &LedIndication.BrtTop);
    if(LedIndication.BrtTop < 0 or LedIndication.BrtTop > 100) LedIndication.BrtTop = 0;
    ee.Read<int32_t>(ADDR_TBottom, &LedIndication.TBottom);
    if(LedIndication.TBottom < 10 or LedIndication.TBottom > 80) LedIndication.TBottom = 18;
    ee.Read<int32_t>(ADDR_TTop, &LedIndication.TTop);
    if(LedIndication.TTop < 10 or LedIndication.TTop > 80) LedIndication.TTop = 45;
    ee.Read<int32_t>(ADDR_LedsEnabled, &En);
    LedIndication.Enabled = (En == 1);

    // Laucaringi
    ee.Read(ADDR_Point1_T, &LRIndication.Point1, 8);
    LRIndication.Point1.Check();
    ee.Read(ADDR_Point2_T, &LRIndication.Point2, 8);
    LRIndication.Point2.Check();
    ee.Read<int32_t>(ADDR_LREnabled, &En);
    LRIndication.Enabled = (En == 1);

    Uart.Printf("Channels: %X\r", EnableMsk);
    Uart.Printf("Freq: %d;\rBrtBottom: %d; BrtTop: %d; TBottom: %d; TTop: %d; Enabled: %d\r", Freq,
            LedIndication.BrtBottom, LedIndication.BrtTop, LedIndication.TBottom, LedIndication.TTop, LedIndication.Enabled);
    Uart.Printf("Point1 T: %d; Point1 Pwr: %d; Point2 T: %d; Point2 Pwr: %d; Enabled: %d\r",
            LRIndication.Point1.T, LRIndication.Point1.Pwr, LRIndication.Point2.T, LRIndication.Point2.Pwr, LRIndication.Enabled);
}

void App_t::SetParam(uint8_t ParamID, int32_t Value) {
    Uart.Printf("Param %u, v %d\r", ParamID, Value);
    int32_t dw32;
    switch(ParamID) {
        // ==== LEDs ====
        case parEnableLeds:
            LedIndication.Enabled = (Value != 0);
            dw32 = LedIndication.Enabled? 1 : 0;
            ee.Write<int32_t>(ADDR_LedsEnabled, &dw32);
            break;
        case parSetChannels:
            EnableMsk = Value;
            ee.Write<int32_t>(ADDR_Channels, &Value);
            break;
        case parSetTTop:
            LedIndication.TTop = Value;
            ee.Write<int32_t>(ADDR_TTop, &Value);
            break;
        case parSetTBottom:
            LedIndication.TBottom = Value;
            ee.Write<int32_t>(ADDR_TBottom, &Value);
            break;
        case parSetLedsTop:
            LedIndication.BrtTop = Value;
            ee.Write<int32_t>(ADDR_BrtTop, &Value);
            break;
        case parSetLedsBottom:
            LedIndication.BrtBottom = Value;
            ee.Write<int32_t>(ADDR_BrtBottom, &Value);
            break;
        case parFreq:
            for(uint8_t i=0; i<8; i++) Led[i]->SetFrequencyHz(Value);
            ee.Write<int32_t>(ADDR_LedsFreq, &Value);
            break;

        // ==== Laucaringi ====
        case parEnableLRs:
            LRIndication.Enabled = (Value != 0);
            dw32 = LRIndication.Enabled? 1 : 0;
            ee.Write<int32_t>(ADDR_LREnabled, &dw32);
            break;
        case parLRPoint1T:
            LRIndication.Point1.T = Value;
            ee.Write<int32_t>(ADDR_Point1_T, &Value);
            break;
        case parLRPoint2T:
            LRIndication.Point2.T = Value;
            ee.Write<int32_t>(ADDR_Point2_T, &Value);
            break;
        case parLRPoint1Pwr:
            LRIndication.Point1.Pwr = Value;
            ee.Write<int32_t>(ADDR_Point1_Pwr, &Value);
            break;
        case parLRPoint2Pwr:
            LRIndication.Point2.Pwr = Value;
            ee.Write<int32_t>(ADDR_Point2_Pwr, &Value);
            break;
        default: break;
    }
}

#endif


#if 1 // ======================= Command processing ============================
void App_t::OnCmd(Shell_t *PShell) {
    Cmd_t *PCmd = &PShell->Cmd;
//    Uart.Printf("\r%S\r", PCmd->Name);
    // Handle command
    if(PCmd->NameIs("Ping")) PShell->Ack(retvOk);

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
