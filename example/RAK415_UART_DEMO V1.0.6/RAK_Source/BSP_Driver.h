#ifndef __BSP_DRIVER_H__
#define __BSP_DRIVER_H__

#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include "M051Series.h"
#include "rak_config.h"
#include "rak_global.h"


extern  volatile uint8_t  UART_RecieveDataFlag;
extern  uint16_t  UART_recvDataLen;	//UART 当前接收长度
extern  uint32_t  rak_Timer;	  //软件计时


int8     rak_bytes2ToAscii(uint16 hex,uint8 *strBuf);
uint8_t  rak_UART_send(uint8_t *tx_buf,uint16_t buflen);


void    Reset_RAK(void);
void    Delay_ms(int32_t ms);
int8    WIFI_CONFIG_INIT(void);
int8    Module_Enter_CmdMode(void);
int8    Send_WPS_Cmd(void);
int8    Send_EasyConfig_Cmd(void);
#endif
