#ifndef _RAKCONFIG_H_
#define _RAKCONFIG_H_

/**/
/**
 * Global Defines
 */
typedef enum Mode{
  EASY_TXRX_TYPE  =0 ,
  ASSIST_CMD_TYPE 	
}Mode_TYPE;
 
//模块正常工作模式选择  透传数据模式 或 辅助命令模式

#define  RAK_MODULE_WORK_MODE 	   	    ASSIST_CMD_TYPE      //EASY_TXRX_TYPE

/***************模块模式选择配置 ********************/

#define  RAK_WLAN_MODE                                "wifi_mode=AP"	   //@Param:  AP , STA
#define  RAK_POWER_MODE                               "power_mode=full"    //@Param:  full , save


/***************AP模式参数配置 ********************/
#define  RAK_AP_SSID                                  "ap_ssid=RAK_AP_SSID"
#define  RAK_AP_CHANNEL                               "ap_channel=6"	       //@Param: 1-13
#define  RAK_AP_SECU_EN                               "ap_secu_en=0"	       //@Param: 0:不加密 1：加密
#define  RAK_AP_PSK                                   "ap_psk=1234567890"
#define  RAK_AP_IPADDR                                "ap_ipaddr=192.168.7.1"
#define  RAK_AP_NETMASK                               "ap_netmask=255.255.255.0"
#define  RAK_AP_BDCAST_EN                             "ap_bdcast_en=1"
//#define  RAK_AP_DHCP_EN                             "ap_dhcp_en=1"	        //默认使能 暂不支持修改


/***************STA模式参数配置 ********************/
#define  RAK_STA_SSID                                 "sta_ssid=TP-LINK_2.4GHz"	 
#define  RAK_STA_SECU_EN                              "sta_secu_en=1"		     //@Param: 0:不加密 1：加密   
#define  RAK_STA_PSK                                  "sta_psk=1234567890"
#define  RAK_STA_DHCP_EN                              "sta_dhcp_en=1"		  	 //@Param: 0：静态设置  1：动态从路由获取IP (DHCP方式)
#define  RAK_STA_IPADDR                               "sta_ipaddr=192.168.1.100"
#define  RAK_STA_NETMASK                              "sta_netmask=255.255.255.0"
#define  RAK_STA_GATEWAY                              "sta_gateway=192.168.1.1"
#define  RAK_STA_DNSS1                                "sta_dnssever1=192.68.1.1"
#define  RAK_STA_DNSS2                                "sta_dnssever2=0.0.0.0"

/***************UART通信参数配置 ********************/

#define  RAK_UART_BAUD                                 "uart_baudrate=115200"   //@Param: autobaud（暂不支持），9600,19200,38400,57600,115200,230400,380400,460800,921600
#define  RAK_UART_DATALEN                              "uart_datalen=8"		    //@Param: 5-8
#define  RAK_UART_PARITYEN                             "uart_parity_en=0"	    //@Param: 0：None 1：Odd  3:Even
#define  RAK_UART_STOPLEN                              "uart_stoplen=1"		    //@Param: 1-2
//#define  RAK_UART_TIMEOUT                            "uart_timeout=20"		  //暂不支持
//#define  RAK_UART_RECVLENOUT                         "uart_recvlenout=500"
#define  RAK_UART_RTSCTSEN                             "uart_rtscts_en=0"	    //@Param: 0:Disable  1:Enable  2:RS485/RTS (RTS作为收发切换脚 高电平发送）


/***************Socket通信参数配置 ********************/
#define  RAK_SOCKET_BAUD							    "socket_multi_en=0"		      //@Param: 0:单Socket模式  1：双Socket模式

#define  RAK_SOCKETA_TYPE                               "socketA_type=ltcp"		       //@Param: tcp,ltcp,udp,ludp (TCP客户端，TCP服务器，UDP客户端，UDP服务器)
#define  RAK_SOCKETA_LPORT                              "socketA_localport=25000"	   //@Param: 1-65535
#define  RAK_SOCKETA_DESTIP                             "socketA_destip=192.168.1.101"
#define  RAK_SOCKETA_DPORT                              "socketA_destport=25000"	   //@Param: 1-65535
#define  RAK_SOCKETA_TCPTIMEOUT                         "socketA_tcp_timeout=0"		   //@Param: 1-600 S	 0：禁用

#define  RAK_SOCKETB_TYPE                               "socketB_type=ltcp"			    //@Param: tcp,ltcp,udp,ludp (TCP客户端，TCP服务器，UDP客户端，UDP服务器)
#define  RAK_SOCKETB_LPORT                              "socketB_localport=25001"		//@Param: 1-65535
#define  RAK_SOCKETB_DESTIP                             "socketB_destip=192.168.1.101"
#define  RAK_SOCKETB_DPORT                              "socketB_destport=25001"	    //@Param: 1-65535
#define  RAK_SOCKETB_TCPTIMEOUT                         "socketB_tcp_timeout=0"		    //@Param: 1-600 S	 0：禁用


/***************模块名称配置 ***********************/
#define  RAK_MODULE_NAME                                "module_name=rak415"			//@Param: 最大16字节
#define  RAK_MODULE_GROUP                               "module_group=rak415"			//@Param: 最大16字节


/***************内置WEB服务器设置 ***********************/
#define  RAK_WEB_USERNAME                               "user_name=admin"				//@Param: 最大16字节
#define  RAK_WEB_PASSWORD                               "user_password=admin"			//@Param: 最大16字节
//#define  RAK_WEB_SWITCH                               "web_switch=0"					//@Param: 0:原厂出厂网页   1：用户定制网页
#define  RAK_WEB_DLANGUAGE                              "web_en=1"					    //@Param: 0：英文WEB   1：中文WEB  第一次打开WEB时显示语言





/***************部分功能可选设置 ***********************/
#define  RAK_WEB_FUNCEN                                "web_func_en=1"	                // 0：关闭WEB功能   1：开启WEB功能接口   默认开启
#define  RAK_UDP_FUNCEN                                "udp_func_en=1"                  // 0：关闭UDP功能   2：开启UDP功能接口   默认开启
#define  RAK_MODE_PIN                                  "mode_pin=wps"                   // wps:切换模块mode引脚为WPS引脚   easy：切换为进入命令引脚

//更多功能正在添加。。。
   
#endif
