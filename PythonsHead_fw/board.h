/*
 * board.h
 *
 *  Created on: 12 сент. 2015 г.
 *      Author: Kreyl
 */

#pragma once

#include <inttypes.h>

// ==== General ====
#define BOARD_NAME          "Python2PCB"
#define APP_NAME            "Python2"

// MCU type as defined in the ST header.
#define STM32F205xx

// Freq of external crystal if any. Leave it here even if not used.
#define CRYSTAL_FREQ_HZ 16000000

#define SYS_TIM_CLK     (Clk.APB1FreqHz) // OS timer settings
#define I2C_REQUIRED    FALSE
#define ADC_REQUIRED    FALSE
#define SIMPLESENSORS_ENABLED   FALSE

#if 1 // ========================== GPIO =======================================
// UART
#define UART_GPIO       GPIOA
#define UART_TX_PIN     9
#define UART_RX_PIN     10
#define UART_AF         AF7 // for USART2 @ GPIOA

// LEDs
#define LED_PIN         GPIOB, 9, omPushPull
#define LED_TOP_BRT     100
#define LED_1           { GPIOB, 4,  TIM3, 1, invInverted, omPushPull, LED_TOP_BRT }
#define LED_2           { GPIOB, 5,  TIM3, 2, invInverted, omPushPull, LED_TOP_BRT }
#define LED_3           { GPIOB, 0,  TIM3, 3, invInverted, omPushPull, LED_TOP_BRT }
#define LED_4           { GPIOB, 1,  TIM3, 4, invInverted, omPushPull, LED_TOP_BRT }
#define LED_5           { GPIOD, 12, TIM4, 1, invInverted, omPushPull, LED_TOP_BRT }
#define LED_6           { GPIOD, 13, TIM4, 2, invInverted, omPushPull, LED_TOP_BRT }
#define LED_7           { GPIOD, 14, TIM4, 3, invInverted, omPushPull, LED_TOP_BRT }
#define LED_8           { GPIOD, 15, TIM4, 4, invInverted, omPushPull, LED_TOP_BRT }

// Laucaringi
#define LR_PWM_TOP      255
#define LR_A_PWM        { GPIOE, 9,  TIM1, 1, invInverted, omPushPull, LR_PWM_TOP}
#define LR_A_DIR1       GPIOE, 7
#define LR_A_DIR2       GPIOE, 8

#define LR_B_PWM        { GPIOE, 11,  TIM1, 2, invInverted, omPushPull, LR_PWM_TOP}
#define LR_B_DIR1       GPIOE, 10
#define LR_B_DIR2       GPIOE, 12

#define LR_C_PWM        { GPIOE, 13,  TIM1, 3, invInverted, omPushPull, LR_PWM_TOP}
#define LR_C_DIR1       GPIOE, 15
#define LR_C_DIR2       GPIOB, 12

#define LR_D_PWM        { GPIOE, 14,  TIM1, 4, invInverted, omPushPull, LR_PWM_TOP}
#define LR_D_DIR1       GPIOB, 13
#define LR_D_DIR2       GPIOB, 14

#define LR_E_PWM        { GPIOC, 6,  TIM8, 1, invInverted, omPushPull, LR_PWM_TOP}
#define LR_E_DIR1       GPIOD, 0
#define LR_E_DIR2       GPIOD, 1

#define LR_F_PWM        { GPIOC, 7,  TIM8, 2, invInverted, omPushPull, LR_PWM_TOP}
#define LR_F_DIR1       GPIOD, 2
#define LR_F_DIR2       GPIOD, 3

#define LR_G_PWM        { GPIOC, 8,  TIM8, 3, invInverted, omPushPull, LR_PWM_TOP}
#define LR_G_DIR1       GPIOD, 4
#define LR_G_DIR2       GPIOD, 5

#define LR_H_PWM        { GPIOC, 9,  TIM8, 4, invInverted, omPushPull, LR_PWM_TOP}
#define LR_H_DIR1       GPIOD, 6
#define LR_H_DIR2       GPIOD, 7

#endif // GPIO

#if I2C_REQUIRED // ====================== I2C =================================
#define I2C1_ENABLED     TRUE
#define I2C_PIN       { GPIOA, 9, 10, I2C1_AF, I2C1_BAUDRATE, I2C1_DMA_TX, I2C1_DMA_RX}
#endif

#if 1 // ========================== USART ======================================
#define UART            USART1
#define UART_TX_REG     UART->DR
#define UART_RX_REG     UART->DR
#endif

#if ADC_REQUIRED // ======================= Inner ADC ==========================
// Clock divider: clock is generated from the APB2
#define ADC_CLK_DIVIDER		adcDiv4

// ADC channels
#define BAT_CHNL 	        10

//#define ADC_VREFINT_CHNL    17  // All 4xx and F072 devices. Do not change.
#define ADC_CHANNELS        { BAT_CHNL }//{ BAT_CHNL, ADC_VREFINT_CHNL }
#define CallConst           450
#define ADC_CHANNEL_CNT     1   // Do not use countof(AdcChannels) as preprocessor does not know what is countof => cannot check
#define ADC_SAMPLE_TIME     ast239d5Cycles
#define ADC_SAMPLE_CNT      16   // How many times to measure every channel

#define ADC_MAX_SEQ_LEN     16  // 1...16; Const, see ref man
#define ADC_SEQ_LEN         (ADC_SAMPLE_CNT * ADC_CHANNEL_CNT)
#if (ADC_SEQ_LEN > ADC_MAX_SEQ_LEN) || (ADC_SEQ_LEN == 0)
#error "Wrong ADC channel count and sample count"
#endif
#endif

#if 1 // =========================== DMA =======================================
#define STM32_DMA_REQUIRED  TRUE
// ==== Uart ====
// Remap is made automatically if required
#define UART_DMA_TX     STM32_DMA2_STREAM7
#define UART_DMA_RX     STM32_DMA2_STREAM5
#define UART_DMA_CHNL   4

#if ADC_REQUIRED
/* DMA request mapped on this DMA channel only if the corresponding remapping bit is cleared in the SYSCFG_CFGR1
 * register. For more details, please refer to Section10.1.1: SYSCFG configuration register 1 (SYSCFG_CFGR1) on
 * page173 */
#define ADC_DMA         STM32_DMA2_STREAM4
#define ADC_DMA_MODE    STM32_DMA_CR_CHSEL(0) |   /* DMA2 Stream4 Channel0 */ \
                        DMA_PRIORITY_LOW | \
                        STM32_DMA_CR_MSIZE_HWORD | \
                        STM32_DMA_CR_PSIZE_HWORD | \
                        STM32_DMA_CR_MINC |       /* Memory pointer increase */ \
                        STM32_DMA_CR_DIR_P2M |    /* Direction is peripheral to memory */ \
                        STM32_DMA_CR_TCIE         /* Enable Transmission Complete IRQ */
#endif // ADC

#endif // DMA
