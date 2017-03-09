//;*****************************************************
//;Company :   rakwireless
//;File Name : USER_APP_TASK.c
//;Author :    Junhua
//;Create Data : 2014-01-12
//;Last Modified : 
//;Description : RAK415 WIFI UART  DRIVER
//;Version :    1.0.6
//;update url:  http://www.rakwireless.com/zh/product/1
//;****************************************************

#include "BSP_Driver.h"
#include "BSP_Init.h"


uint8_t  wait_WPSbuttonPress=0;             //�жϵ���Ҫִ��WPS����
uint8_t	 wait_EasyConfigbuttonPress=0;		//�жϵ���Ҫִ��EasyConfig����

//;***************************************************************
//;Description: Recv_dataHandle(void)
//;input	  : void
//;Returns    : void
//;Notes      :	RAK���յ��� ���ݴ��� 
//;***************************************************************
int8_t Recv_dataHandle(void)
{
    uint16_t  recvLen=0;
	uint16_t  dest_port=0;
	uint32_t  dest_ip=0;

#if (RAK_MODULE_WORK_MODE==ASSIST_CMD_TYPE)   
	if(strncmp((char*)uCmdRspFrame.recvdataFrame.recvdata.recvheader,RAK_RECIEVE_DATA_CMD,strlen(RAK_RECIEVE_DATA_CMD))==0)
	{
	   	dest_port=uCmdRspFrame.recvdataFrame.recvdata.destPort[0]|
		          uCmdRspFrame.recvdataFrame.recvdata.destPort[1]<<8;

		dest_ip  =uCmdRspFrame.recvdataFrame.recvdata.destIp[0]|
				  uCmdRspFrame.recvdataFrame.recvdata.destIp[1]<<8 |
				  uCmdRspFrame.recvdataFrame.recvdata.destIp[2]<<16|
				  uCmdRspFrame.recvdataFrame.recvdata.destIp[3]<<24;

		recvLen=uCmdRspFrame.recvdataFrame.recvdata.recDataLen[0]+uCmdRspFrame.recvdataFrame.recvdata.recDataLen[1]*256;
		if(uCmdRspFrame.recvdataFrame.recvdata.socketID==SOCKETA_ID) //���յ�SocketA���� 
		{
		   rak_send_Data(SOCKETA_ID,dest_port,dest_ip,uCmdRspFrame.recvdataFrame.recvdata.recvdataBuf,recvLen);
		
		}else
		 if(uCmdRspFrame.recvdataFrame.recvdata.socketID==SOCKETB_ID) //���յ�SocketB����
		 {
		   rak_send_Data(SOCKETB_ID,dest_port,dest_ip,uCmdRspFrame.recvdataFrame.recvdata.recvdataBuf,recvLen);
		 }
	    return RAK_SUCCESS;

	}else
	{
	  return RAK_FAIL;		 //��������ģʽ�½��ճ���  �踴λģ��  
	}

#elif (RAK_MODULE_WORK_MODE==EASY_TXRX_TYPE)
    //͸��˫Socket �ж�S0 S1
	if(strncmp((char*)uCmdRspFrame.recvdataFrame.recvdata.recvheader,SOCKETA_HDER,strlen(SOCKETA_HDER))==0)
	{
	   rak_UART_send(SOCKETA_HDER,strlen(SOCKETA_HDER));
	   rak_UART_send(&uCmdRspFrame.uCmdRspBuf[2],UART_recvDataLen-2);
	}else
	if(strncmp((char*)uCmdRspFrame.recvdataFrame.recvdata.recvheader,SOCKETB_HDER,strlen(SOCKETB_HDER))==0)
	{
	   rak_UART_send(SOCKETA_HDER,strlen(SOCKETB_HDER));
	   rak_UART_send(&uCmdRspFrame.uCmdRspBuf[2],UART_recvDataLen-2);
	}
	//��ȫ����͸��
	else
	{
	   rak_UART_send(uCmdRspFrame.uCmdRspBuf,UART_recvDataLen);		
	}
	return RAK_SUCCESS;
#endif

}


int main(void)
{
	int8_t   retval;	//����ִ�з���ֵ

	SYS_Init();			//ϵͳ��ʼ��
	
	BSP_Init();		    //��ʼ��Ӳ�� 

	Reset_RAK();	   //��λWIFIģ��

#if 0
	retval=WIFI_CONFIG_INIT(); 
	if(retval<0)
	{
	   return retval;
	}
#endif
	
#if RAK_MODULE_WORK_MODE==ASSIST_CMD_TYPE
	retval=Module_Enter_CmdMode(); 
	if(retval<0)
	{
	   return retval;
	}
#endif


 while(1)
	{
	   if(UART_RecieveDataFlag == TRUE)						 //���յ����ݴ���
	   {		   
		  UART_RecieveDataFlag = FALSE;	
		  Recv_dataHandle();		 
	   }
	  //���ͻ���Ƶ�· δ����ģ����������WPS ��CONFIG  ��������ģʽ�·����������� ����host�����¼�ִ��
#if   RAK_MODULE_WORK_MODE==ASSIST_CMD_TYPE 
	  if(wait_WPSbuttonPress)
	  {
	  	 wait_WPSbuttonPress=0;
		 Send_WPS_Cmd();
	  }
	   if(wait_EasyConfigbuttonPress)
	  {
	  	 wait_EasyConfigbuttonPress=0;
		 Send_EasyConfig_Cmd();
	  }
#endif
	  Delay_ms(2); 
  
	}
}


