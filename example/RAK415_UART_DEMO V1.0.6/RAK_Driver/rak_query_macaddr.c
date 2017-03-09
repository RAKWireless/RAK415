//;*****************************************************
//;Company :   rakwireless
//;File Name : rak_query_macaddr.c
//;Author :    Junhua
//;Create Data : 2014-01-12
//;Last Modified : 
//;Description : RAK415 WIFI UART  DRIVER
//;Version :    1.0.5
//;update url:  www.rakwireless.com
//;****************************************************


#include "BSP_Driver.h"

/*=============================================================================*/
/**
 * @fn           int16 rak_query_macaddr(void)
 * @brief        UART, query_macaddr 
 * @param[in]    none 
 * @param[out]   none
 * @return       errCode
 *               -1 = busy 
 *               0  = SUCCESS
 * @section description  
 * 
 */
 /*=============================================================================*/
int8 rak_query_macaddr(void)
{
	int8	     retval=0;
	char rak_sendCmd[20]="";
	strcpy(rak_sendCmd,RAK_QUERY_MACADDR_CMD);
	strcat(rak_sendCmd,RAK_END);
    retval=rak_UART_send((uint8_t*)rak_sendCmd,strlen(rak_sendCmd));//���ʹ�ģ������ӿ�����
	return retval;
}

