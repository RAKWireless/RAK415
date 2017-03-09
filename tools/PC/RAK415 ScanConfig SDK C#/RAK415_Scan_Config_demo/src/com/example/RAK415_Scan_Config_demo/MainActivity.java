package com.example.RAK415_Scan_Config_demo;

import java.util.ArrayList;
import java.util.HashMap;

import com.example.rak415_scan_config.RAK415_Scan_Config;



import android.R.integer;
import android.net.DhcpInfo;
import android.net.wifi.WifiInfo;
import android.net.wifi.WifiManager;
import android.os.Bundle;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.text.format.Formatter;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.AdapterView.OnItemClickListener;

public class MainActivity extends Activity
{
	private int number=100;//可扫描模块个数 
	private byte[][] RAK415Info=new byte[number][43];//接收扫描到的模块信息
	private String BoardCastIP;//广播地址
	
	private Button Search;//扫描按钮	
	private EditText Version;//模块固件版本号
	private EditText Get_Config;//获取到的配置数据
	private EditText Config_Data;//配置数据
	private Button Config;//配置模块
	private Button Reset;//复位模块
	private Button FacReset;//恢复出厂设置
	
	private static adapter_my listItemAdapter; // ListView的适配器
	ArrayList<RAKInfo> RAKNodes;
	private static ArrayList<HashMap<String, Object>> listItem; // ListView的数据源，这里是一个HashMap的列表
	ListView Scanlist;//扫描列表
	AlertDialog.Builder builder;//认证对话框
	View admin1;	
	String ipString="";//选中模块的IP地址
	boolean admin_ok=false;//认证是否成功
	public static WifiManager.MulticastLock lock;//某些手机接收udp广播需解锁（如HTC）
	WifiManager wifiManager;
    DhcpInfo d;
	
	@Override
	protected void onCreate(Bundle savedInstanceState)
	{
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);
		
		Search=(Button)findViewById(R.id.search);
		Search.setOnClickListener(search_clik);
		Version=(EditText)findViewById(R.id.version);
		Get_Config=(EditText)findViewById(R.id.get_config);
		Config_Data=(EditText)findViewById(R.id.config_data);
		Config=(Button)findViewById(R.id.config);
		Config.setOnClickListener(config_clik);
		Reset=(Button)findViewById(R.id.reset);
		Reset.setOnClickListener(reset_clik);
		FacReset=(Button)findViewById(R.id.facreset);
		FacReset.setOnClickListener(facreset_clik);
		
		Scanlist=(ListView)findViewById(R.id.Scan_list);
		listItem = new ArrayList<HashMap<String, Object>>();
		RAKNodes = new ArrayList<RAKInfo>();//新建一个ArrayList，数据源为RAKInfo
		//list_item为每一项的布局文件名字，最后一个参数 int[] 为每一项的布局文件中的控件ID
		listItemAdapter = new adapter_my(this,listItem, R.layout.list_item,new String[]
				{ "icon", "module","group","mac","ip", "rssi", "chek" }, new int[]
				{ R.id.ItemIcon, R.id.Module_name, R.id.Group_name,R.id.MAC,R.id.IP,
				  R.id.RSSI,R.id.ItemChek });
		Scanlist.setAdapter(listItemAdapter);		
		Scanlist.setOnItemClickListener(list_item_clik);
		// 弹出认证对话框
		builder = new AlertDialog.Builder(this); 
		wifiManager = (WifiManager) getSystemService(Context.WIFI_SERVICE);//获取wifi服务       
		this.lock= wifiManager.createMulticastLock("UDPwifi");//添加权限，防止udp接收不到  

	}
	
    /*********************************************************************************************************
    ** 功能说明：初始化wifi，获取本地广播地址
    ********************************************************************************************************/	
	private String WifiInit()
	{		
		if (!wifiManager.isWifiEnabled())//判断wifi是否开启 
        { 
          wifiManager.setWifiEnabled(true);   //未开启则自动开启
        } 
		d=wifiManager.getDhcpInfo();	       
        int x=~(d.netmask)|(d.gateway);//子网掩码取反，或上网关就是子网广播地址   
        String ip =Formatter.formatIpAddress(x);
        return ip;//获取本地广播IP地址	
	}
	
    /*********************************************************************************************************
    ** 功能说明：获得模块信息，添加到列表显示
    ********************************************************************************************************/	
	OnClickListener search_clik = new OnClickListener()
	{
		@Override
		public void onClick(View v)
		{
			BoardCastIP=WifiInit();	//初始化wifi，获取本地广播地址
			lock.acquire();//允许接收udp广播
	        RAK415Info=RAK415_Scan_Config.Scan(BoardCastIP);//扫描RAK415模块，返回扫描到的模块信息
	        lock.release();//释放
	        //获得模块信息后，添加到列表显示
	        listItem.clear();
	        //扫描过程中禁止点击
	        Search.setEnabled(false);
	        Scanlist.setEnabled(false);
	        Config.setEnabled(false);
	        Reset.setEnabled(false);
	        FacReset.setEnabled(false);
	        
	        for(int i=0;i<number;i++)
	        {
	        	RAKNodes.add(new RAKInfo(RAK415Info[i]));//获取扫描到模块的信息  
	        	RAKInfo node = RAKNodes.get(i);
	        	if (node.Mac.equals("00:00:00:00:00:00")==false)//有数据，Mac不全为0的话
				{	        		
		        	//添加到列表显示
		        	AddListItem(node.NickName, node.GroupName, node.Ip,node.Mac,"-"+node.Rssi,0);
				}
	        	else 
	        	{
					break;
				}	        	
	        }
	        RAKNodes.clear();//清空原有的数据
	        //扫描完使能点击
	        Search.setEnabled(true);
	        Scanlist.setEnabled(true);
	        Config.setEnabled(true);
	        Reset.setEnabled(true);
	        FacReset.setEnabled(true);
		}	
	};
	/*********************************************************************************************************
	 ** 功能说明：扫描到的模块，添加到列表
	 ********************************************************************************************************/
	private void AddListItem(String nickname,String groupname,String ip, String mac, String rssi,int chek)
	{
		HashMap<String, Object> map = new HashMap<String, Object>();
		// 添加图片
		map.put("icon", R.drawable.a2);
		// 添加文本"module","group","ip", "mac","rssi",
		map.put("module", nickname);
		map.put("group", groupname);
		map.put("ip", ip);
		map.put("mac", mac);
		map.put("rssi", rssi);
		listItem.add(map);
		// 刷新列表
		listItemAdapter.notifyDataSetChanged();
	}
	
	
	/*********************************************************************************************************
	 ** 功能说明：list点击的事件，进行模块认证
	 ********************************************************************************************************/
	OnItemClickListener list_item_clik = new OnItemClickListener()
		{
			@Override
			public void onItemClick(AdapterView<?> arg0, View arg1, int arg2,long arg3)
			{
				ipString=listItem.get(arg2).get("ip")+ "";
				listItemAdapter.setSelectedPosition(arg2);			
	  
				admin_confirm();		
				
				// 刷新列表
				listItemAdapter.notifyDataSetChanged();
			}
		};

		
		
		
	public void admin_confirm()
	{
		//弹出认证对话框
		LayoutInflater inflater=getLayoutInflater();
		admin1=inflater.inflate(R.layout.admin, (ViewGroup)findViewById(R.id.adminpassword));
	
		builder.setTitle("Module Certification")
		   .setIcon(R.drawable.admin)
	       .setCancelable(false) 
	       .setView(admin1)  
	       .setPositiveButton("OK", new DialogInterface.OnClickListener() { 
	          public void onClick(DialogInterface dialog, int id) 
	           { 			        	  		        		  	       		  			        		  		        	 
	              final EditText module_admin =(EditText)admin1.findViewById(R.id.Edit_admin);//认证名
	        	  final EditText module_password=(EditText)admin1.findViewById(R.id.Edit_password);//认证密码	        	 	        	  
	        	  
	        	  String admin=module_admin.getText().toString();
		    	  String password=module_password.getText().toString();				    	  				    					    					    	  				    	  
	        	  admin_ok=RAK415_Scan_Config.Certificate(admin, password, ipString);
	        	  if(admin_ok)
					{
						DisplayToast("Certificate sueecssed!!!");//认证成功
						Version.setText(RAK415_Scan_Config.Get_Version(ipString));//获取模块版本号
						Get_Config.setText(RAK415_Scan_Config.Get_Config(ipString));//获取模块配置信息
					}
					else 
					{
						DisplayToast("Certificate failed!!!");
					}
	        	  dialog.cancel(); //关闭对话框  
	           } 
	       }) 
	       .setNegativeButton("Cancel", new DialogInterface.OnClickListener() { 
	           public void onClick(DialogInterface dialog, int id) 
	           { 
	                dialog.cancel(); //关闭对话框
	           } 
	       })
	       .show();	
	}	
	
	/*********************************************************************************************************
    ** 功能说明：配置模块
    ********************************************************************************************************/	
	OnClickListener config_clik = new OnClickListener()
	{

		@Override
		public void onClick(View v)
		{
			// TODO Auto-generated method stub
			boolean config_ok=false;
			//将配置框中的配置数据配置到模块
			config_ok=RAK415_Scan_Config.Config(Config_Data.getText().toString(), ipString);
			if(config_ok)
			{
				DisplayToast("Config successed!!!");
			}
			else 
			{
				DisplayToast("Config failed!!!");
			}
		}
		
	};
	/*********************************************************************************************************
    ** 功能说明：复位模块
    ********************************************************************************************************/	
	OnClickListener reset_clik = new OnClickListener()
	{

		@Override
		public void onClick(View v)
		{
			// TODO Auto-generated method stub
			boolean reset_ok=false;
			//复位模块
			reset_ok=RAK415_Scan_Config.Reset(ipString);
			if(reset_ok)
			{
				DisplayToast("Reset successed!!!");
				//复位模块后，需要重新扫描模块，才可操作
		        Scanlist.setEnabled(false);
		        Config.setEnabled(false);
		        Reset.setEnabled(false);
		        FacReset.setEnabled(false);
			}
			else 
			{
				DisplayToast("Reset failed!!!");
			}
		}
		
	};
	/*********************************************************************************************************
    ** 功能说明：恢复出厂设置
    ********************************************************************************************************/	
	OnClickListener facreset_clik = new OnClickListener()
	{

		@Override
		public void onClick(View v)
		{
			// TODO Auto-generated method stub
			boolean facreset_ok=false;
			//恢复出厂设置
			facreset_ok=RAK415_Scan_Config.FACReset(ipString);
			if(facreset_ok)
			{
				DisplayToast("FACReset successed!!!");
				//恢复出厂设置后，需要重新连接，重新扫描模块，才可操作
				Scanlist.setEnabled(false);
		        Config.setEnabled(false);
		        Reset.setEnabled(false);
		        FacReset.setEnabled(false);
			}
			else 
			{
				DisplayToast("FACReset failed!!!");
			}
		}
		
	};
    /*********************************************************************************************************
    ** 功能说明：UI界面消息显示
    ********************************************************************************************************/
	public void DisplayToast(String str)
	{
		Toast.makeText(this, str, Toast.LENGTH_SHORT).show();
	}

}

