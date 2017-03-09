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


uint8_t  wait_WPSbuttonPress=0;             //判断到需要执行WPS命令
uint8_t	 wait_EasyConfigbuttonPress=0;		//判断到需要执行EasyConfig命令

//;***************************************************************
//;Description: Recv_dataHandle(void)
//;input	  : void
//;Returns    : void
//;Notes      :	RAK接收到的 数据处理 
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
		if(uCmdRspFrame.recvdataFrame.recvdata.socketID==SOCKETA_ID) //接收到SocketA数据 
		{
		   rak_send_Data(SOCKETA_ID,dest_port,dest_ip,uCmdRspFrame.recvdataFrame.recvdata.recvdataBuf,recvLen);
		
		}else
		 if(uCmdRspFrame.recvdataFrame.recvdata.socketID==SOCKETB_ID) //接收到SocketB数据
		 {
		   rak_send_Data(SOCKETB_ID,dest_port,dest_ip,uCmdRspFrame.recvdataFrame.recvdata.recvdataBuf,recvLen);
		 }
	    return RAK_SUCCESS;

	}else
	{
	  return RAK_FAIL;		 //辅助命令模式下接收出错  需复位模块  
	}

#elif (RAK_MODULE_WORK_MODE==EASY_TXRX_TYPE)
    //透传双Socket 判断S0 S1
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
	//完全数据透传
	else
	{
	   rak_UART_send(uCmdRspFrame.uCmdRspBuf,UART_recvDataLen);		
	}
	return RAK_SUCCESS;
#endif

}


int main(void)
{
	int8_t   retval;	//函数执行返回值

	SYS_Init();			//系统初始化
	
	BSP_Init();		    //初始化硬件 

	Reset_RAK();	   //复位WIFI模块

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
	   if(UART_RecieveDataFlag == TRUE)						 //接收到数据处理
	   {		   
		  UART_RecieveDataFlag = FALSE;	
		  Recv_dataHandle();		 
	   }
	  //若客户设计电路 未连接模块配置引脚WPS ，CONFIG  可在命令模式下发送命令配置 根据host触发事件执行
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


