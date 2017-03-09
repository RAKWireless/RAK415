//;*****************************************************
//;Company :   rakwireless
//;File Name : RAK_WIFI_Driver.c
//;Author :    Junhua
//;Create Data : 2014-01-12
//;Last Modified : 
//;Description : RAK415 WIFI UART  DRIVER
//;Version :    1.0.5
//;update url:  http://www.rakwireless.com/zh/product/1
//;****************************************************

#include "BSP_Driver.h"
#include "rak_config.h"
#include "rak_global.h"

rak_cfg_t         rak_userCfgstr;	   //保存用户参数结构体
rak_cfg_t         rak_restoreCfgstr;   //保存出厂参数结构体
rak_uCmdRsp	      uCmdRspFrame;		   //命令的返回结构
uint32_t          rak_Timer=0;	       //软件计时


/*@ Command response timeout */
int8_t RAK_RESPONSE_TIMEOUT(uint32_t timeOut) 
{   		   
	 int8_t   retval=0;

	 RAK_RESET_TIMER; 
	 while((UART_RecieveDataFlag == FALSE) && (RAK_INC_TIMER<=timeOut));	                                                                             
	 if (RAK_INC_TIMER > timeOut)  
	 {					     
		retval = RAK_FAIL;			 					  
	 }
	 return retval;
} 
   
void  rak_clearPktFlag(void)
{
   UART_RecieveDataFlag =FALSE;  
}

//;*****************************************************************************
//;Description: WIFI_CONFIG_INIT() 
//;input	  : void
//;Returns    : int8_t
//;Notes      :	对出厂RAK WIFI进行出厂配置客户化  该部分函数只需要执行一次   
//;*****************************************************************************
int8_t WIFI_CONFIG_INIT(void)
{

   	int8_t   retval=0;
   
    //打开模块的命令接口  成功后模块以AT命令格式交换数据
    retval=rak_open_cmdMode();
	if(retval<0)
	{
	   return retval;
	}
	Delay_ms(1000);       

   //客户将自己的默认配置参数替换原厂的出厂配置，并预置好通信Socket参数	，等待用户配置到正确的网络环境（无线UDP配置，WEB配置，WPS，Easyconfig方式）。
   
   retval=rak_write_restoreConfig();
   if(retval<0)
   {
	   return retval;
	}else
	{
	  //读取AT命令的返回
	  retval=RAK_RESPONSE_TIMEOUT(RAK_WRCFGTIMEOUT);
	  if(retval<0)
	  {
	  	return retval;
	  }else
	  {
	  	rak_clearPktFlag();
		if(strncmp((char *)uCmdRspFrame.uCmdRspBuf,"OK",2) == 0)
		{
		  retval= RAK_SUCCESS; 
		}else
		{
		  return RAK_CFG_ERROR;		
		}
	  }	
	}
    //若客户不需要修改模块的出厂配置 可以直接调用rak_write_userConfig();模块将直接工作于用户参数。

	//保存过参数 复位后生效
	Reset_RAK();	   //复位WIFI模块

	Delay_ms(1000);	   //等待模块加载参数  Not Necessary

	return RAK_SUCCESS; 
}

int8_t  Module_Enter_CmdMode(void)
{

	 int8_t   retval=0;
   
    //打开模块的命令接口  成功后模块以AT命令格式交换数据
    retval=rak_open_cmdMode();
	if(retval<0)
	{
	   return retval;
	}
//	Delay_ms(1000);

    retval=rak_read_userConfig();
    if(retval<0)
	{
	   return retval;
	}else
	{
	  //读取AT命令的返回
	  retval=RAK_RESPONSE_TIMEOUT(RAK_RDCFGTIMEOUT);
	  if(retval<0)
	  {
	  	return retval;
	  }else
	  {
	  	rak_clearPktFlag();
		if(strncmp((char *)uCmdRspFrame.uCmdRspBuf,"OK",2) == 0)
		{
		  rak_init_config((char *)&uCmdRspFrame.uCmdRspBuf[2],&rak_userCfgstr);
		  retval= RAK_SUCCESS; 
		}else
		{
		  return RAK_CFG_ERROR;		
		}
	  }	
	}


   //等待模块连接上网络
   do
   {
	   retval=rak_query_constatus();
	   if(retval<0)
		{
		   return retval;
		}else
		{
		  //读取AT命令的返回
		  retval=RAK_RESPONSE_TIMEOUT(RAK_QCONSATUSTIMEOUT);
		  if(retval<0)
		  {
		  	return retval;
		  }else
		  {
		  	rak_clearPktFlag();
			if(strncmp((char *)uCmdRspFrame.uCmdRspBuf,"OK",2) == 0 &&(uCmdRspFrame.qryconstatusResponse.qryconstatusframe.status==0x01))
			{
			  retval= RAK_SUCCESS; 
			}else
			{
			  retval= RAK_FAIL;
			}
		  }	
		}
	  Delay_ms(200);
	}while(retval!= RAK_SUCCESS);

	//等待分配IP成功
    do
     {	
	   retval= rak_query_ipconifg();
	   if(retval<0)
		{
		   return retval;
		}else
		{
		  //读取AT命令的返回
		  retval=RAK_RESPONSE_TIMEOUT(RAK_QIPCFGTIMEOUT);
		  if(retval<0)
		  {
		  	return retval;
		  }else
		  {
		  	rak_clearPktFlag();
			if(strncmp((char *)uCmdRspFrame.uCmdRspBuf,"OK",2) == 0)
			{
			  if(uCmdRspFrame.qryipconfigResponse.qryipconfigframe.ipAddr[0]==0x01 &&
			  	 uCmdRspFrame.qryipconfigResponse.qryipconfigframe.ipAddr[1]==0x00 &&
			  	 uCmdRspFrame.qryipconfigResponse.qryipconfigframe.ipAddr[2]==0x00 &&
				 uCmdRspFrame.qryipconfigResponse.qryipconfigframe.ipAddr[3]==0x7F 
			  )
			  {
			    retval= RAK_FAIL ;
			  }else
			  {
			    retval= RAK_SUCCESS;
			  } 
			}
		  }	
		}
	  Delay_ms(200);
	}while(retval!= RAK_SUCCESS);

	return RAK_SUCCESS; 
}


int8   Send_WPS_Cmd(void)
{
	  int8_t   retval=0;
	  retval=rak_wps_config();
	  if(retval<0)
	  {
		   return retval;
		}else
		{
		  //读取AT命令的返回
		  retval=RAK_RESPONSE_TIMEOUT(RAK_WPSTIMEOUT);
		  if(retval<0)
		  {
		  	return retval;
		  }else
		  {
		  	rak_clearPktFlag();
			if(strncmp((char *)uCmdRspFrame.uCmdRspBuf,"OK",2) == 0)
			{
			  retval= RAK_SUCCESS; 
			}
		  }	
		}

	return RAK_SUCCESS; 
}


int8   Send_EasyConfig_Cmd(void)
{
	  int8_t   retval=0;
	  retval=rak_easy_config();
	  if(retval<0)
	  {
		   return retval;
		}else
		{
		  //读取AT命令的返回
		  retval=RAK_RESPONSE_TIMEOUT(RAK_EASYCFGTIMEOUT);
		  if(retval<0)
		  {
		  	return retval;
		  }else
		  {
		  	rak_clearPktFlag();
			if(strncmp((char *)uCmdRspFrame.uCmdRspBuf,"OK",2) == 0)
			{
			  retval= RAK_SUCCESS; 
			}
		  }	
		}

	return RAK_SUCCESS; 
}
