/*
 * File:   main.cpp
 * Author: Elessar
 * Project: MasonOrder
 *
 * Created on May 27, 2016, 6:37 PM
 */

#include "main.h"
#include "kl_lib.h"
//#include "SimpleSensors.h"
//#include "led.h"
//#include "Sequences.h"

App_t App;

int main() {
    // ==== Setup clock ====
//    uint8_t ClkResult = FAILURE;
//    Clk.SetupFlashLatency(12);  // Setup Flash Latency for clock in MHz
//    // 12 MHz/6 = 2; 2*192 = 384; 384/8 = 48 (preAHB divider); 384/8 = 48 (USB clock)
//    Clk.SetupPLLDividers(6, 192, pllSysDiv8, 8);
//    // 48/4 = 12 MHz core clock. APB1 & APB2 clock derive on AHB clock
//    Clk.SetupBusDividers(ahbDiv4, apbDiv1, apbDiv1);
//    if((ClkResult = Clk.SwitchToPLL()) == 0) Clk.HSIDisable();
    Clk.UpdateFreqValues();

    // ==== Init OS ====
    halInit();
    chSysInit();
    App.InitThread();

    // ==== Init Hard & Soft ====
    Uart.Init(115200, UART_GPIO, UART_TX_PIN, UART_GPIO, UART_RX_PIN);
    Uart.Printf("\r%S %S\r\n", APP_NAME, BUILD_TIME);
    Clk.PrintFreqs();

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
