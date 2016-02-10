/*
 * board.h
 *
 *  Created on: 12 ����. 2015 �.
 *      Author: Kreyl
 */

#pragma once

#include <inttypes.h>

// ==== General ====
#define BOARD_NAME          "Locket3"
// MCU type as defined in the ST header.
#define STM32L151xB

// Freq of external crystal if any. Leave it here even if not used.
#define CRYSTAL_FREQ_HZ 12000000

#define SYS_TIM_CLK     (Clk.APB1FreqHz)

// ==== Different modules requirement ====
#define I2C_REQUIRED            TRUE
#define ADC_REQUIRED            FALSE
#define SIMPLESENSORS_ENABLED   FALSE
#define SEQUENCES_REQUIRED      FALSE

#if 1 // ========================== GPIO =======================================
// UART
#define UART_GPIO       GPIOA
#define UART_TX_PIN     9
#define UART_RX_PIN     10
#define UART_AF         AF7 // for USART1 @ GPIOA

// LED
#define LED_GPIO        GPIOB
#define LED_PIN         15

// Outputs
#define PWM_GPIO        GPIOB
#define PWM7_PIN        8
#define PWM8_PIN        9

// Sns
#define SNS_I2C_GPIO    GPIOB
#define SNS_I2C_SCL_PIN 10
#define SNS_I2C_SDA_PIN 11

#endif // GPIO

#if 1 // ========================= Timer =======================================
// PWM
#define PWM7_TIM        TIM4
#define PWM8_TIM        TIM4
#define PWM7_CH         3
#define PWM8_CH         4

#endif // Timer

#if I2C_REQUIRED // ====================== I2C =================================
#define I2C_SNS         I2C2
#endif

#if 1 // =========================== SPI =======================================
#endif

#if 1 // ========================== USART ======================================
#define UART            USART1
#define UART_TX_REG     UART->DR
#define UART_RX_REG     UART->DR
#endif

#if ADC_REQUIRED // ======================= Inner ADC ==========================
// Clock divider: clock is generated from the APB2
#define ADC_CLK_DIVIDER		adcDiv2

// ADC channels
#define BAT_CHNL 	        1

#define ADC_VREFINT_CHNL    17  // All 4xx and F072 devices. Do not change.
#define ADC_CHANNELS        { BAT_CHNL, ADC_VREFINT_CHNL }
#define ADC_CHANNEL_CNT     2   // Do not use countof(AdcChannels) as preprocessor does not know what is countof => cannot check
#define ADC_SAMPLE_TIME     ast55d5Cycles
#define ADC_SAMPLE_CNT      8   // How many times to measure every channel

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
//#define UART_DMA_TX     STM32_DMA1_STREAM4
//#define UART_DMA_RX     STM32_DMA1_STREAM5
#define UART_DMA_CHNL   0   // Dummy

#if 1 // ==== I2C ====
#define I2C_SNS_DMA_TX  STM32_DMA1_STREAM4
#define I2C_SNS_DMA_RX  STM32_DMA1_STREAM5
#endif

#if ADC_REQUIRED
/* DMA request mapped on this DMA channel only if the corresponding remapping bit is cleared in the SYSCFG_CFGR1
 * register. For more details, please refer to Section10.1.1: SYSCFG configuration register 1 (SYSCFG_CFGR1) on
 * page173 */
#define ADC_DMA         STM32_DMA1_STREAM1
#define ADC_DMA_MODE    STM32_DMA_CR_CHSEL(0) |   /* DMA2 Stream4 Channel0 */ \
                        DMA_PRIORITY_LOW | \
                        STM32_DMA_CR_MSIZE_HWORD | \
                        STM32_DMA_CR_PSIZE_HWORD | \
                        STM32_DMA_CR_MINC |       /* Memory pointer increase */ \
                        STM32_DMA_CR_DIR_P2M |    /* Direction is peripheral to memory */ \
                        STM32_DMA_CR_TCIE         /* Enable Transmission Complete IRQ */
#endif // ADC

#endif // DMA
