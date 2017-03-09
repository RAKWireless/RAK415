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

rak_cfg_t         rak_userCfgstr;	   //�����û������ṹ��
rak_cfg_t         rak_restoreCfgstr;   //������������ṹ��
rak_uCmdRsp	      uCmdRspFrame;		   //����ķ��ؽṹ
uint32_t          rak_Timer=0;	       //�����ʱ


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
//;Notes      :	�Գ���RAK WIFI���г������ÿͻ���  �ò��ֺ���ֻ��Ҫִ��һ��   
//;*****************************************************************************
int8_t WIFI_CONFIG_INIT(void)
{

   	int8_t   retval=0;
   
    //��ģ�������ӿ�  �ɹ���ģ����AT�����ʽ��������
    retval=rak_open_cmdMode();
	if(retval<0)
	{
	   return retval;
	}
	Delay_ms(1000);       

   //�ͻ����Լ���Ĭ�����ò����滻ԭ���ĳ������ã���Ԥ�ú�ͨ��Socket����	���ȴ��û����õ���ȷ�����绷��������UDP���ã�WEB���ã�WPS��Easyconfig��ʽ����
   
   retval=rak_write_restoreConfig();
   if(retval<0)
   {
	   return retval;
	}else
	{
	  //��ȡAT����ķ���
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
    //���ͻ�����Ҫ�޸�ģ��ĳ������� ����ֱ�ӵ���rak_write_userConfig();ģ�齫ֱ�ӹ������û�������

	//��������� ��λ����Ч
	Reset_RAK();	   //��λWIFIģ��

	Delay_ms(1000);	   //�ȴ�ģ����ز���  Not Necessary

	return RAK_SUCCESS; 
}

int8_t  Module_Enter_CmdMode(void)
{

	 int8_t   retval=0;
   
    //��ģ�������ӿ�  �ɹ���ģ����AT�����ʽ��������
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
	  //��ȡAT����ķ���
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


   //�ȴ�ģ������������
   do
   {
	   retval=rak_query_constatus();
	   if(retval<0)
		{
		   return retval;
		}else
		{
		  //��ȡAT����ķ���
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

	//�ȴ�����IP�ɹ�
    do
     {	
	   retval= rak_query_ipconifg();
	   if(retval<0)
		{
		   return retval;
		}else
		{
		  //��ȡAT����ķ���
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
		  //��ȡAT����ķ���
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
		  //��ȡAT����ķ���
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
