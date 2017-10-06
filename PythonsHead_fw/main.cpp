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
    // Leds
    parEnableLeds = 1,
    parFreq = 2,
    parLedPoint1T = 3, parLedPoint1Pwr = 4, parLedPoint2T = 5, parLedPoint2Pwr = 6,
    // Laucaringi
    parEnableLRs = 7,
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
int32_t SFreq = 0;

struct TemperaturePwrPoint_t {
    int32_t T, Pwr;
    void Check(int32_t PwrBottom, int32_t PwrTop) {
        if(T < 11 or T > 90) T = 18;
        if(Pwr < PwrBottom or Pwr > PwrTop) Pwr = 0;
    }
};

class LedIndication_t {
public:
    TemperaturePwrPoint_t Point1, Point2;
    bool Enabled;
    void Process(int16_t *pt);
} LedIndication;

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
        Led[i]->SetFrequencyHz(4);
        // Test them one by one
        Led[i]->Set(100);
        chThdSleepMilliseconds(450);
        Led[i]->Set(0);
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
                for(int i=0; i<LR_CNT; i++) {
                    uint32_t v = Adc.GetResult(AdcChannels[i]);
                    v = Adc.Adc2mV(v, VRef_adc);
                    Filt[i].Put(v);
                    if(Filt[i].IsReady()) {
                        uint32_t VLr = Filt[i].GetResult();
                        Filt[i].Flush();
                        Lr[i]->Adjust_mV(VLr);
                    }
                }

//                    Uart.Printf("%u %u %u %u   %u %u %u %u\r", VLr[0],VLr[1],VLr[2],VLr[3], VLr[4],VLr[5],VLr[6],VLr[7]);
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
                int32_t Brt = CalcProportion(Point1.T, Point2.T, Point1.Pwr, Point2.Pwr, pt[i]);
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
            if(ChEnabled(i) and (i==0 or i==(SNS_T_CNT-1)))  { // Check if this channel required, enable only first and last
                int32_t Pwr = CalcProportion(Point1.T, Point2.T, Point1.Pwr, Point2.Pwr, pt[i]);
                Lr[i]->SetTarget_mV(Pwr);
                Uart.Printf("LR %d: %d mv\r", i, Pwr);
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
#define ADDR_LedsPoint1_T   8
#define ADDR_LedsPoint1_Pwr 12
#define ADDR_LedsPoint2_T   16
#define ADDR_LedsPoint2_Pwr 20
#define ADDR_LedsEnabled    24

#define ADDR_LRPoint1_T     28
#define ADDR_LRPoint1_Pwr   32
#define ADDR_LRPoint2_T     36
#define ADDR_LRPoint2_Pwr   40
#define ADDR_LREnabled      44

void LoadSettings() {
    int32_t En = 0;
    // Common
    ee.Read<int32_t>(ADDR_Channels, &EnableMsk);
    if(EnableMsk < 0 or EnableMsk > 0xFF) EnableMsk = 0xFF;
    // Leds
    ee.Read<int32_t>(ADDR_LedsEnabled, &En);
    LedIndication.Enabled = (En == 1);
    ee.Read<int32_t>(ADDR_LedsFreq, &SFreq);
    if(SFreq < 1 or SFreq > 4000) SFreq = 4;
    for(uint8_t i=0; i<8; i++) Led[i]->SetFrequencyHz(SFreq);

    ee.Read(ADDR_LedsPoint1_T, &LedIndication.Point1, 8);
    LedIndication.Point1.Check(0, 100);
    ee.Read(ADDR_LedsPoint2_T, &LedIndication.Point2, 8);
    LedIndication.Point2.Check(0, 100);

    // Laucaringi
    ee.Read<int32_t>(ADDR_LREnabled, &En);
    LRIndication.Enabled = (En == 1);
    ee.Read(ADDR_LRPoint1_T, &LRIndication.Point1, 8);
    LRIndication.Point1.Check(-2000, 2000);
    ee.Read(ADDR_LRPoint2_T, &LRIndication.Point2, 8);
    LRIndication.Point2.Check(-2000, 2000);

    Uart.Printf("Channels: %X\r", EnableMsk);
    Uart.Printf("Freq: %d;\rPoint1 T: %d; Point1 Pwr: %d; Point2 T: %d; Point2 Pwr: %d; Enabled: %d\r", SFreq,
            LedIndication.Point1.T, LedIndication.Point1.Pwr, LedIndication.Point2.T, LedIndication.Point2.Pwr, LedIndication.Enabled);
    Uart.Printf("Point1 T: %d; Point1 Pwr: %d; Point2 T: %d; Point2 Pwr: %d; Enabled: %d\r",
            LRIndication.Point1.T, LRIndication.Point1.Pwr, LRIndication.Point2.T, LRIndication.Point2.Pwr, LRIndication.Enabled);
}

void App_t::SetParam(uint8_t ParamID, int32_t Value) {
    Uart.Printf("Param %u, v %d\r", ParamID, Value);
    int32_t dw32;
    switch(ParamID) {
        // ==== Common ====
        case parSetChannels:
            EnableMsk = Value;
            ee.Write<int32_t>(ADDR_Channels, &Value);
            break;
        // ==== LEDs ====
        case parEnableLeds:
            LedIndication.Enabled = (Value != 0);
            dw32 = LedIndication.Enabled? 1 : 0;
            ee.Write<int32_t>(ADDR_LedsEnabled, &dw32);
            break;
        case parFreq:
            SFreq = Value;
            for(uint8_t i=0; i<8; i++) Led[i]->SetFrequencyHz(Value);
            ee.Write<int32_t>(ADDR_LedsFreq, &Value);
            break;

        case parLedPoint1T:
            LedIndication.Point1.T = Value;
            ee.Write<int32_t>(ADDR_LedsPoint1_T, &Value);
            break;
        case parLedPoint2T:
            LedIndication.Point2.T = Value;
            ee.Write<int32_t>(ADDR_LedsPoint2_T, &Value);
            break;
        case parLedPoint1Pwr:
            LedIndication.Point1.Pwr = Value;
            ee.Write<int32_t>(ADDR_LedsPoint1_Pwr, &Value);
            break;
        case parLedPoint2Pwr:
            LedIndication.Point2.Pwr = Value;
            ee.Write<int32_t>(ADDR_LedsPoint2_Pwr, &Value);
            break;

        // ==== Laucaringi ====
        case parEnableLRs:
            LRIndication.Enabled = (Value != 0);
            dw32 = LRIndication.Enabled? 1 : 0;
            ee.Write<int32_t>(ADDR_LREnabled, &dw32);
            break;
        case parLRPoint1T:
            LRIndication.Point1.T = Value;
            ee.Write<int32_t>(ADDR_LRPoint1_T, &Value);
            break;
        case parLRPoint2T:
            LRIndication.Point2.T = Value;
            ee.Write<int32_t>(ADDR_LRPoint2_T, &Value);
            break;
        case parLRPoint1Pwr:
            LRIndication.Point1.Pwr = Value;
            ee.Write<int32_t>(ADDR_LRPoint1_Pwr, &Value);
            break;
        case parLRPoint2Pwr:
            LRIndication.Point2.Pwr = Value;
            ee.Write<int32_t>(ADDR_LRPoint2_Pwr, &Value);
            break;
        default: break;
    }
}

uint8_t App_t::GetParam(uint8_t ParamID, int32_t *PValue) {
    uint8_t Rslt = retvOk;
    switch(ParamID) {
        // ==== Common ====
        case parSetChannels:  *PValue = EnableMsk; break;
        // ==== LEDs ====
        case parEnableLeds:   *PValue = LedIndication.Enabled? 1 : 0; break;
        case parFreq:         *PValue = SFreq; break;
        case parLedPoint1T:   *PValue = LedIndication.Point1.T; break;
        case parLedPoint1Pwr: *PValue = LedIndication.Point1.Pwr; break;
        case parLedPoint2T:   *PValue = LedIndication.Point2.T; break;
        case parLedPoint2Pwr: *PValue = LedIndication.Point2.Pwr; break;
        // ==== Laucaringi ====
        case parEnableLRs:    *PValue = LRIndication.Enabled? 1 : 0; break;
        case parLRPoint1T:    *PValue = LRIndication.Point1.T; break;
        case parLRPoint1Pwr:  *PValue = LRIndication.Point1.Pwr; break;
        case parLRPoint2T:    *PValue = LRIndication.Point2.T; break;
        case parLRPoint2Pwr:  *PValue = LRIndication.Point2.Pwr; break;

        default: Rslt = retvBadValue; break;
    }
    return Rslt;
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
