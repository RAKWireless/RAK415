package com.example.RAK415_Scan_Config_demo;

public class RAKInfo
{
	private static final int BeaconSize = 43;
	private static final int GroupNameSize = 16;
	private static final int NickNameSize = 16;
	private static final int IpSize = 4;
	private static final int MacSize = 6;
	private static final int RssiSize = 1;
	public String GroupName; // ������
	public String NickName; // ����
	public String Ip; // IP��ַ
	public String Mac; // mac��ַ
	public int Rssi; // �ź�ǿ��

	public RAKInfo(byte[] beacon)
	{
		if (beacon.length < BeaconSize)
			return;
		int pos = 0;//ָ��λ��
		int cnt;//����
		//NickName
		for(cnt=0;cnt<16;cnt++)
		{
			if(beacon[cnt]==0x00)
			{
				break;
			}
		}
		NickName = new String(beacon, pos, cnt);				
		pos += 16;
        //GroupName
		//cnt = 16;
		for(cnt=0;cnt<16;cnt++)
		{
			if(beacon[cnt+16]==0x00)
			{
				break;
			}
		}
		GroupName = new String(beacon, pos, cnt);
		pos += 16;
        //IP
		Ip = (int) (beacon[pos++] & 0xFF) + "." + (int) (beacon[pos++] & 0xFF)
				+ "." + (int) (beacon[pos++] & 0xFF) + "."
				+ (int) (beacon[pos++] & 0xFF);
        //MAC
		Mac = "";
		for (int i = 0; i < MacSize; i++)
		{
			if (i > 0)
				Mac += ':';
			String bmac = Integer.toHexString(beacon[pos++] & 0xFF)
					.toUpperCase();
			if (bmac.length() == 1)
				bmac = '0' + bmac;
			Mac += bmac;
		}
		//RSSI
		Rssi = (int) (beacon[pos] & 0xFF);
	}
	
}
