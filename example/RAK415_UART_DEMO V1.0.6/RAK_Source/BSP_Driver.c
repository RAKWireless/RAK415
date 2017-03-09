//;*****************************************************
//;Company :   rakwireless
//;File Name : BSP_DRIVER.c
//;Author :    Junhua
//;Create Data : 2014-01-12
//;Last Modified : 
//;Description : RAK415 WIFI UART  DRIVER
//;Version :    1.0.5
//;update url:  http://www.rakwireless.com/zh/product/1
//;****************************************************

#include "BSP_Driver.h"


uint8_t volatile   UART_RecieveDataFlag = 0;			//串口接收到模块数据包 标志位
uint16_t   UART_data_len = 0;            //串口接收长度                                                                          
uint16_t   UART_recvDataLen=0;


/*---------------------------------------------------------------------------------------------------------*/
/* ISR to handle UART Channel 1 interrupt event                                                            */
/*---------------------------------------------------------------------------------------------------------*/
void UART1_IRQHandler(void)
{
   	uint8_t bInChar[1]={0};
		
    if(UART1->ISR & UART_ISR_RDA_INT_Msk)                              //检查是否接收中断 Check if receive interrupt
    {
		_TIMER_RESET(TIMER1); 				
		if(_UART_IS_RX_READY(UART1))                                //检查接收到的数据是否有效//en:Check if received data avaliable
        {
            while (UART1->FSR & UART_FSR_RX_EMPTY_Msk);                //Wait until an avaliable char present in RX FIFO
            bInChar[0] = UART1->RBR;                               //读取字符Read the char		
            if(UART_data_len < RXBUFSIZE)                        //测缓冲区满否 Check if buffer full
            {
                uCmdRspFrame.uCmdRspBuf[UART_data_len] = bInChar[0];        //Enqueue the character
                UART_data_len++;
            }	
        }
		_TIMER_START(TIMER1, TIMER_TCSR_CEN_Msk | TIMER_TCSR_IE_Msk | TIMER_TCSR_MODE_PERIODIC | TIMER_TCSR_TDR_EN_Msk ,1);				
    }
}

/********************  rak_UART_send data ************************/
uint8_t rak_UART_send(uint8_t *tx_buf,uint16_t buflen)
{
    uint16_t i;
    for (i=0; i<buflen; i++) {       
    while((UART1->FSR & UART_FSR_TX_FULL_Msk) != 0);                   //发送FIFO满时等待//en:Wait until UART transmit FIFO is not full
    UART1->THR = (uint8_t) tx_buf[i];                           //通过UART0发送一个字符//en:Transmit a char via UART0
    }
    return 0;	
}


/************************  WDT_IRQHandler  ****************************/

void TMR1_IRQHandler(void)
{
//    uint8_t status=0;
    /* Clear TIMER0 Timeout Interrupt Flag */
   _TIMER_CLEAR_CMP_INT_FLAG(TIMER1);
   _TIMER_RESET(TIMER1); 
   	UART_RecieveDataFlag = TRUE;
	UART_recvDataLen = UART_data_len;
	UART_data_len=0;

}

/* RESET CPU core and IP */
void CHIPIP_RST(void)
{
	/* RESET CPU core and IP */
    SYS->IPRSTC1 |= SYS_IPRSTC1_CHIP_RST_Msk;
}

/************************  RESET RAK410  ****************************/
void Reset_RAK(void)
{
    RESET_PORT_PIN=0;
    SYS_SysTickDelay(10000);
    RESET_PORT_PIN=1; 
}
void Delay_ms(int32_t ms)
{
    int32_t i;

    for(i=0;i<ms;i++)
        SYS_SysTickDelay(1000);
}


