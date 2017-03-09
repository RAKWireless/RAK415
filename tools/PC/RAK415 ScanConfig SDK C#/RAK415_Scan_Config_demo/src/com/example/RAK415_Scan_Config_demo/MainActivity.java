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
	private int number=100;//��ɨ��ģ����� 
	private byte[][] RAK415Info=new byte[number][43];//����ɨ�赽��ģ����Ϣ
	private String BoardCastIP;//�㲥��ַ
	
	private Button Search;//ɨ�谴ť	
	private EditText Version;//ģ��̼��汾��
	private EditText Get_Config;//��ȡ������������
	private EditText Config_Data;//��������
	private Button Config;//����ģ��
	private Button Reset;//��λģ��
	private Button FacReset;//�ָ���������
	
	private static adapter_my listItemAdapter; // ListView��������
	ArrayList<RAKInfo> RAKNodes;
	private static ArrayList<HashMap<String, Object>> listItem; // ListView������Դ��������һ��HashMap���б�
	ListView Scanlist;//ɨ���б�
	AlertDialog.Builder builder;//��֤�Ի���
	View admin1;	
	String ipString="";//ѡ��ģ���IP��ַ
	boolean admin_ok=false;//��֤�Ƿ�ɹ�
	public static WifiManager.MulticastLock lock;//ĳЩ�ֻ�����udp�㲥���������HTC��
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
		RAKNodes = new ArrayList<RAKInfo>();//�½�һ��ArrayList������ԴΪRAKInfo
		//list_itemΪÿһ��Ĳ����ļ����֣����һ������ int[] Ϊÿһ��Ĳ����ļ��еĿؼ�ID
		listItemAdapter = new adapter_my(this,listItem, R.layout.list_item,new String[]
				{ "icon", "module","group","mac","ip", "rssi", "chek" }, new int[]
				{ R.id.ItemIcon, R.id.Module_name, R.id.Group_name,R.id.MAC,R.id.IP,
				  R.id.RSSI,R.id.ItemChek });
		Scanlist.setAdapter(listItemAdapter);		
		Scanlist.setOnItemClickListener(list_item_clik);
		// ������֤�Ի���
		builder = new AlertDialog.Builder(this); 
		wifiManager = (WifiManager) getSystemService(Context.WIFI_SERVICE);//��ȡwifi����       
		this.lock= wifiManager.createMulticastLock("UDPwifi");//���Ȩ�ޣ���ֹudp���ղ���  

	}
	
    /*********************************************************************************************************
    ** ����˵������ʼ��wifi����ȡ���ع㲥��ַ
    ********************************************************************************************************/	
	private String WifiInit()
	{		
		if (!wifiManager.isWifiEnabled())//�ж�wifi�Ƿ��� 
        { 
          wifiManager.setWifiEnabled(true);   //δ�������Զ�����
        } 
		d=wifiManager.getDhcpInfo();	       
        int x=~(d.netmask)|(d.gateway);//��������ȡ�����������ؾ��������㲥��ַ   
        String ip =Formatter.formatIpAddress(x);
        return ip;//��ȡ���ع㲥IP��ַ	
	}
	
    /*********************************************************************************************************
    ** ����˵�������ģ����Ϣ����ӵ��б���ʾ
    ********************************************************************************************************/	
	OnClickListener search_clik = new OnClickListener()
	{
		@Override
		public void onClick(View v)
		{
			BoardCastIP=WifiInit();	//��ʼ��wifi����ȡ���ع㲥��ַ
			lock.acquire();//�������udp�㲥
	        RAK415Info=RAK415_Scan_Config.Scan(BoardCastIP);//ɨ��RAK415ģ�飬����ɨ�赽��ģ����Ϣ
	        lock.release();//�ͷ�
	        //���ģ����Ϣ����ӵ��б���ʾ
	        listItem.clear();
	        //ɨ������н�ֹ���
	        Search.setEnabled(false);
	        Scanlist.setEnabled(false);
	        Config.setEnabled(false);
	        Reset.setEnabled(false);
	        FacReset.setEnabled(false);
	        
	        for(int i=0;i<number;i++)
	        {
	        	RAKNodes.add(new RAKInfo(RAK415Info[i]));//��ȡɨ�赽ģ�����Ϣ  
	        	RAKInfo node = RAKNodes.get(i);
	        	if (node.Mac.equals("00:00:00:00:00:00")==false)//�����ݣ�Mac��ȫΪ0�Ļ�
				{	        		
		        	//��ӵ��б���ʾ
		        	AddListItem(node.NickName, node.GroupName, node.Ip,node.Mac,"-"+node.Rssi,0);
				}
	        	else 
	        	{
					break;
				}	        	
	        }
	        RAKNodes.clear();//���ԭ�е�����
	        //ɨ����ʹ�ܵ��
	        Search.setEnabled(true);
	        Scanlist.setEnabled(true);
	        Config.setEnabled(true);
	        Reset.setEnabled(true);
	        FacReset.setEnabled(true);
		}	
	};
	/*********************************************************************************************************
	 ** ����˵����ɨ�赽��ģ�飬��ӵ��б�
	 ********************************************************************************************************/
	private void AddListItem(String nickname,String groupname,String ip, String mac, String rssi,int chek)
	{
		HashMap<String, Object> map = new HashMap<String, Object>();
		// ���ͼƬ
		map.put("icon", R.drawable.a2);
		// ����ı�"module","group","ip", "mac","rssi",
		map.put("module", nickname);
		map.put("group", groupname);
		map.put("ip", ip);
		map.put("mac", mac);
		map.put("rssi", rssi);
		listItem.add(map);
		// ˢ���б�
		listItemAdapter.notifyDataSetChanged();
	}
	
	
	/*********************************************************************************************************
	 ** ����˵����list������¼�������ģ����֤
	 ********************************************************************************************************/
	OnItemClickListener list_item_clik = new OnItemClickListener()
		{
			@Override
			public void onItemClick(AdapterView<?> arg0, View arg1, int arg2,long arg3)
			{
				ipString=listItem.get(arg2).get("ip")+ "";
				listItemAdapter.setSelectedPosition(arg2);			
	  
				admin_confirm();		
				
				// ˢ���б�
				listItemAdapter.notifyDataSetChanged();
			}
		};

		
		
		
	public void admin_confirm()
	{
		//������֤�Ի���
		LayoutInflater inflater=getLayoutInflater();
		admin1=inflater.inflate(R.layout.admin, (ViewGroup)findViewById(R.id.adminpassword));
	
		builder.setTitle("Module Certification")
		   .setIcon(R.drawable.admin)
	       .setCancelable(false) 
	       .setView(admin1)  
	       .setPositiveButton("OK", new DialogInterface.OnClickListener() { 
	          public void onClick(DialogInterface dialog, int id) 
	           { 			        	  		        		  	       		  			        		  		        	 
	              final EditText module_admin =(EditText)admin1.findViewById(R.id.Edit_admin);//��֤��
	        	  final EditText module_password=(EditText)admin1.findViewById(R.id.Edit_password);//��֤����	        	 	        	  
	        	  
	        	  String admin=module_admin.getText().toString();
		    	  String password=module_password.getText().toString();				    	  				    					    					    	  				    	  
	        	  admin_ok=RAK415_Scan_Config.Certificate(admin, password, ipString);
	        	  if(admin_ok)
					{
						DisplayToast("Certificate sueecssed!!!");//��֤�ɹ�
						Version.setText(RAK415_Scan_Config.Get_Version(ipString));//��ȡģ��汾��
						Get_Config.setText(RAK415_Scan_Config.Get_Config(ipString));//��ȡģ��������Ϣ
					}
					else 
					{
						DisplayToast("Certificate failed!!!");
					}
	        	  dialog.cancel(); //�رնԻ���  
	           } 
	       }) 
	       .setNegativeButton("Cancel", new DialogInterface.OnClickListener() { 
	           public void onClick(DialogInterface dialog, int id) 
	           { 
	                dialog.cancel(); //�رնԻ���
	           } 
	       })
	       .show();	
	}	
	
	/*********************************************************************************************************
    ** ����˵��������ģ��
    ********************************************************************************************************/	
	OnClickListener config_clik = new OnClickListener()
	{

		@Override
		public void onClick(View v)
		{
			// TODO Auto-generated method stub
			boolean config_ok=false;
			//�����ÿ��е������������õ�ģ��
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
    ** ����˵������λģ��
    ********************************************************************************************************/	
	OnClickListener reset_clik = new OnClickListener()
	{

		@Override
		public void onClick(View v)
		{
			// TODO Auto-generated method stub
			boolean reset_ok=false;
			//��λģ��
			reset_ok=RAK415_Scan_Config.Reset(ipString);
			if(reset_ok)
			{
				DisplayToast("Reset successed!!!");
				//��λģ�����Ҫ����ɨ��ģ�飬�ſɲ���
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
    ** ����˵�����ָ���������
    ********************************************************************************************************/	
	OnClickListener facreset_clik = new OnClickListener()
	{

		@Override
		public void onClick(View v)
		{
			// TODO Auto-generated method stub
			boolean facreset_ok=false;
			//�ָ���������
			facreset_ok=RAK415_Scan_Config.FACReset(ipString);
			if(facreset_ok)
			{
				DisplayToast("FACReset successed!!!");
				//�ָ��������ú���Ҫ�������ӣ�����ɨ��ģ�飬�ſɲ���
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
    ** ����˵����UI������Ϣ��ʾ
    ********************************************************************************************************/
	public void DisplayToast(String str)
	{
		Toast.makeText(this, str, Toast.LENGTH_SHORT).show();
	}

}

