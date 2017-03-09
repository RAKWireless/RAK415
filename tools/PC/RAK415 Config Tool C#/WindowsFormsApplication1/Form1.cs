using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class RAKConfigure : Form
    {
        bool read_flg = false;
        int len = 0;
        byte[] buf = new byte[5000];
        UInt32 ovtime = 1000000;
        System.Timers.Timer timer0 = new System.Timers.Timer();
        OpenFileDialog getconfigdata = new OpenFileDialog();
        SaveFileDialog saveFileDialogcfg = new SaveFileDialog();
        public RAKConfigure()
        {
            InitializeComponent();
        }

        private void RAKConfigure_Load(object sender, EventArgs e)
        {
            timer0.Elapsed += new System.Timers.ElapsedEventHandler(timer0_Elapsed);
            timer0.AutoReset = false;
            timer0.Enabled = false;
            try
            {
                string[] szPorts = SerialPort.GetPortNames(); //获取当前可用的串口列表
                comboPortName.Items.AddRange(szPorts);
                comboPortName.SelectedIndex = 0;               
            }
            catch (Win32Exception win32ex) //获取串口出错
            {
                MessageBox.Show(win32ex.ToString());
            }
            comboBaudrate.Text = "115200";
        }

        private void buttonOpenserial_Click(object sender, EventArgs e)
        {
            if (buttonOpenserial.Text == "Open")
            {
                serialPort1.Close();
                serialPort1.PortName = comboPortName.Text;
                serialPort1.BaudRate = int.Parse(comboBaudrate.Text);
                serialPort1.DataBits = int.Parse("8");
                serialPort1.StopBits = StopBits.One;
                serialPort1.Parity = Parity.None;
                try
                {
                    serialPort1.Open();                  
                }
                catch (Exception ex) //打开串口出错
                {
                    MessageBox.Show(ex.Message);
                    serialPort1.Close();
                    serialPort1.Dispose();
                    buttonOpenserial.Text = "Open";
                    return;
                }
                if (serialPort1.IsOpen)
                {
                    byte[] sendata1 = new byte[1];
                    int num = 0;
                    do{
                        buf[0] = 0;
                        read_flg = false;
                        timer0.Interval = 200;
                        serialPort1.Write("+++");
                        timer0.Enabled = true;
                        timer0.Start();
                        while ((timer0.Enabled == true)&&(buf[0] != 0x55)) ;
                        read_flg = false;
                    } while ((buf[0] != 0x55) && ((num++) < 6));
                 //   if (read_flg == true)
                    {
/*                        for (int i = 0; i < len; i++)
                        {
                            textBox_Serial_text.Text += Convert.ToString((char)buf[i]);
                        }
*/
                       //textBox_Serial_text.Text+= Encoding.GetEncoding("gb2312").GetString(buf, 0, len).Replace("\0", "").Trim()+"\r\n";
                    }
                    if (buf[0] != 0x55)
                    {
                        serialPort1.Close();
                        MessageBox.Show("Open error!");
                        return;
                    }
                    else
                    {
                        
                        string[] text = new string[20];
                        sendata1[0] = 0x55;
                        buf[0] = 0;
                        buf[1] = 0;
                        System.Threading.Thread.Sleep(100);
                        read_flg = false;
                        serialPort1.Write(sendata1,0,1);
                        ovtime = 1000000000;
                        while (read_flg == false)
                        {
                            if ((ovtime--) < 2)
                                break;
                        }
                        if (read_flg != true)
                        {
                            MessageBox.Show("Error!");
                            return;
                        }
                        if (read_flg == true)
                        {
/*                            for (int i = 0; i < len; i++)
                            {
                                textBox_Serial_text.Text += Convert.ToString((char)buf[i]);
                            }
*/
                            textBox_Serial_text.Text += Encoding.GetEncoding("gb2312").GetString(buf, 0, len).Replace("\0", "").Trim() + "\r\n";
                        }
                        if ((buf[0] != 0x4f) || (buf[1] != 0x4b))//error
                        {
                            MessageBox.Show("Open ATCMD error!");
                            return;
                        }
                        buttonOpenserial.Text = "Close";
                        read_flg = false;
                        serialPort1.Write("at+version\r\n");
                        while (read_flg == false);
                        ovtime = 1000000;
                        while (read_flg == false)
                        {
                            if ((ovtime--) < 2)
                                break;
                        }
                        if (read_flg != true)
                        {
                            MessageBox.Show("Error!");
                            return;
                        }
                        if (read_flg == true)
                        { 
/*                            for (int i = 0; i < len; i++)
                            {
                                textBox_Serial_text.Text += Convert.ToString((char)buf[i]);
                            }
*/
                           // textBox_Serial_text.Text += Encoding.GetEncoding("gb2312").GetString(buf, 0, len).Replace("\0", "").Trim() + "\r\n";
                        }
                        read_flg = false;
                        if ((buf[0] != 0x4f) || (buf[1] != 0x4b))//error
                        {
                            MessageBox.Show("Query version error!");
                            return;
                        }
                        textBox_Version.Text = "";
                        for (int i = 2; i < len; i++)
                        {
                            textBox_Version.Text += Convert.ToChar(buf[i]);
                        }
                        read_flg = false;
                        serialPort1.Write("at+mac\r\n");
                        while (read_flg == false) ;
                        ovtime = 1000000;
                        while (read_flg == false)
                        {
                            if ((ovtime--) < 2)
                                break;
                        }
                        if (read_flg != true)
                        {
                            MessageBox.Show("Error!");
                            return;
                        }
                        if (read_flg == true)
                        {
/*                            for (int i = 0; i < len; i++)
                            {
                                textBox_Serial_text.Text += Convert.ToString((char)buf[i]);
                            }
*/
                            //textBox_Serial_text.Text += Encoding.GetEncoding("gb2312").GetString(buf, 0, len).Replace("\0", "").Trim()+"\r\n";
                        }
                        read_flg = false;
                        if ((buf[0] != 0x4f) || (buf[1] != 0x4b))//error
                        {
                            MessageBox.Show("Query mac error!");
                            return;
                        }
                        textBox_mac.Text = "";
                        textBox_mac.Text += String.Format("{0:X}", buf[2]);
                        for (int i = 3; i < 8; i++)
                        {                           
                            textBox_mac.Text += ":";
                            if ((buf[i] >= 0) && (buf[i] < 16))
                                textBox_mac.Text += "0";
                            textBox_mac.Text += String.Format("{0:X}", buf[i]);
                            
                        }
                        this.Refresh();
                        System.Threading.Thread.Sleep(1500);
                        button_scan.Enabled = true;
                        textBox_web_username.Enabled = true;
                        textBox_web_password.Enabled = true;
                        button_save_webcfg.Enabled = true;
                        linkLabelClear.Enabled = true;
                        button_import_cfg.Enabled = true;
                        button_export_cfg.Enabled = true;
                        button_readcfg.Enabled = true;
                        button_save_cfg.Enabled = true;
                        button_read_factorycfg.Enabled = true;
                        button_save_factorycfg.Enabled = true;
                        textBox_cfg_text.Enabled = true;                        
                    }              
                }
            }
            else
            {
                read_flg = false;
                serialPort1.Write("at+easy_txrx\r\n");
                ovtime = 100000000;
                while (read_flg == false)
                {
                    if ((ovtime--) < 2)
                        break;   
                }
                if (read_flg == true)
                {
/*                    for (int i = 0; i < len; i++)
                    {
                        textBox_Serial_text.Text += Convert.ToString((char)buf[i]);
                    }
*/
                    //textBox_Serial_text.Text = Encoding.GetEncoding("gb2312").GetString(buf, 0, len).Replace("\0", "").Trim() + "\r\n";
                }
                read_flg = false;
                if ((buf[0] != 0x4f) || (buf[1] != 0x4b))//error
                {
                    MessageBox.Show("Enter pass-through mode error!");
                    return;
                }
                serialPort1.Close();
                buttonOpenserial.Text = "Open";

                button_scan.Enabled = false;
                textBox_web_username.Enabled = false;
                textBox_web_password.Enabled = false;
                button_save_webcfg.Enabled = false;
                linkLabelClear.Enabled = false;
                button_import_cfg.Enabled = false;
                button_export_cfg.Enabled = false;
                button_readcfg.Enabled = false;
                button_save_cfg.Enabled = false;
                button_read_factorycfg.Enabled = false;
                button_save_factorycfg.Enabled = false;
                textBox_cfg_text.Enabled = false;
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int n = 0;
            read_flg = false;
            System.Threading.Thread.Sleep(100);            
            if (serialPort1.IsOpen)
            {
                n = serialPort1.BytesToRead;
                len = n;
                serialPort1.Read(buf, 0, n);//读取缓冲数据
                read_flg = true;
                //this.Invoke((EventHandler)(delegate
                //{
                //    for (int i = 0; i < len; i++)
                //    {
                //        textBox_Serial_text.Text += Convert.ToString((char)buf[i]);//Convert.ToString(buf[i]);
                //    }
                        
                //}));
            }
        }
        private void button_scan_Click(object sender, EventArgs e)
        {
            int scan_num = 0;
            string send_data = "";
            string[] security = new string[]{"WPA2","WPA","WEP","802.1X","PSK","WEP","TKIP","CCMP"};
          //  byte[] buf2 = new byte[] { 0x4F, 0x4B, 0x54, 0x50, 0x2D, 0x4C, 0x49, 0x4E, 0x4B, 0x5F, 0x32, 0x2E, 0x34, 0x47, 0x48, 0x7A, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 0x8C, 0x21, 0x0A, 0xD9, 0xEB, 0x7B, 0x06, 0xCC, 0x0, 0x54, 0x50, 0x2D, 0x4C, 0x49, 0x4E, 0x4B, 0x5F, 0x32, 0x2E, 0x34, 0x47, 0x48, 0x7A, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 0x8C, 0x21, 0x0A, 0xD9, 0xEB, 0x7B, 0x06, 0xCC, 0xC9, 0x54, 0x50, 0x2D, 0x4C, 0x49, 0x4E, 0x4B, 0x5F, 0x32, 0x2E, 0x34, 0x47, 0x48, 0x7A, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 0x8C, 0x21, 0x0A, 0xD9, 0xEB, 0x7B, 0x06, 0xCC, 0, 0x0D, 0x0A};
            if (serialPort1.IsOpen)
            {
                read_flg = false;
                serialPort1.Write("at+scan=0\r\n");
                while (read_flg == false) ;
                ovtime = 1000000;
                while (read_flg == false)
                {
                    if ((ovtime--) < 2)
                        break;
                }
                if (read_flg != true)
                {
                    MessageBox.Show("Error!");
                    return;
                }
                read_flg = false;
                if ((buf[0] == 0x4f) && (buf[1] == 0x4b))//OK
                {
                    scan_num = buf[2];
                    textBox_Serial_text.Text += "OK" + scan_num + "\r\n";
                    this.dataGridView1.Rows.Clear();
                    for (int i = 1; i < scan_num; i++)
                        dataGridView1.Rows.Add();
                    send_data = "at+get_scan=";
                    send_data += Convert.ToString(scan_num);
                    send_data += "\r\n";
                    read_flg = false;
                    serialPort1.Write(send_data);
                    while (read_flg == false) ;
                    if (read_flg == true)
                    {
/*                        for (int i = 0; i < len; i++)
                        {
                            textBox_Serial_text.Text += Convert.ToString((char)buf[i]);
                        }
*/
                       // textBox_Serial_text.Text += Encoding.GetEncoding("gb2312").GetString(buf, 0, len).Replace("\0", "").Trim();
                    }
                    this.Refresh();
                    read_flg = false;
                    if ((buf[0] == 0x4f) && (buf[1] == 0x4b))//OK
                    {
                        for (int i = 0,j = 2,k = 2; i < scan_num;i++ )
                        {
                            for (;  j< k + 33; j++)//ssid
                            {
                                byte[] ssid=new byte[33];                              
                                {
                                    for (int c = 0; c < 32; c++)
                                    {
                                        if (buf[j] != 0)
                                            ssid[c] = buf[j + c];
                                        else
                                            break;
                                    }
                                    
                                    string ss = "";
                                    ss=Encoding.GetEncoding("gb2312").GetString(ssid, 0, ssid.Length).Replace("\0", "").Trim();
                                    textBox_Serial_text.Text += ss+"\r\n";
                                    this.dataGridView1.Rows[i].Cells[0].Value += ss;
                                    j = k + 33;
                                    break;
                                    //this.dataGridView1.Rows[i].Cells[0].Value = Convert.ToString((char)buf[j]);
                                }
                            }
                           k = j + 6;
                            for (j = k; j < k + 1; j++)//channel
                            {
                                this.dataGridView1.Rows[i].Cells[1].Value += Convert.ToString(buf[j]);
                            }
                            k = j;
                            for (; j < k + 1; j++)//rssi
                            {
                                this.dataGridView1.Rows[i].Cells[2].Value += "-";
                                this.dataGridView1.Rows[i].Cells[2].Value += Convert.ToString(0xff - buf[j]);
                            }
                            k = j;
                            for (; j < k + 1; j++)//security
                            {
                                byte n = 0;
                                byte z = buf[j];
                                bool flg = false;
                                if (buf[j] != 0)
                                {
                                    for (int m = 0; m < 8; m++)
                                    {
                                        n = (byte)(z & 0x80);
                                        if (n == 0x80)
                                        {
                                            if (flg == true)
                                            {
                                                this.dataGridView1.Rows[i].Cells[3].Value += "-";
                                            }
                                            this.dataGridView1.Rows[i].Cells[3].Value += security[m];
                                            flg = true;
                                        }
                                        z = (byte)(z << 1);
                                    }
                                }
                                else
                                {
                                    this.dataGridView1.Rows[i].Cells[3].Value = "Open";
                                }

                            }
                            k = j;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Scan error!");
                    return;
                }
            }            
        }
        void timer0_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timer0.Stop();
            timer0.Enabled = false;
        }

        private void button_import_cfg_Click(object sender, EventArgs e)
        {
            getconfigdata.Filter = "Config Flie(*.cfg)|*.cfg";
            if (getconfigdata.ShowDialog() == DialogResult.OK)
            {
                string folder = getconfigdata.FileName;
                textBox_read_location.Text = getconfigdata.FileName;
                try
                {
                    System.IO.StreamReader objreaddata = new System.IO.StreamReader(folder,UnicodeEncoding.GetEncoding("GB2312"));

                    string cfg_data = objreaddata.ReadToEnd();
                    textBox_cfg_text.Text = cfg_data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {

            }
        }
        private void button_export_cfg_Click(object sender, EventArgs e)
        {
            saveFileDialogcfg.Filter = "Config Flie(*.cfg)|*.cfg";
            saveFileDialogcfg.FileName = "RAKconfig";
            if (saveFileDialogcfg.ShowDialog() == DialogResult.OK)
            {
                string fName = saveFileDialogcfg.FileName;
                textBox_read_location.Text = fName;
                //byte[] read_data = Encoding.GetEncoding("gb2312").GetBytes(textBox_cfg_text.Text);
                File.WriteAllText(fName, textBox_cfg_text.Text, Encoding.Default);
            }
        }

        private void button_save_webcfg_Click(object sender, EventArgs e)
        {
            if ((serialPort1.IsOpen) && (textBox_web_username.Text != "") && (textBox_web_password.Text != ""))
            {
                string send_data = "";
                string config_data = "";
                config_data = "user_name=" + textBox_web_username.Text + "&user_password=" + textBox_web_password.Text;
                send_data += "at+write_config=";
                send_data += config_data.Length;
                send_data += ",";
                send_data += config_data;
                send_data += "\r\n";
                read_flg = false;
                serialPort1.Write(send_data);
                while (read_flg == false) ;
                ovtime = 1000000;
                while (read_flg == false)
                {
                    if ((ovtime--) < 2)
                        break;
                }
                if (read_flg != true)
                {
                    MessageBox.Show("Error!");
                    return;
                }
                if (read_flg == true)
                {
/*                    for (int i = 0; i < len; i++)
                    {
                        textBox_Serial_text.Text += Convert.ToString((char)buf[i]);
                    }
*/
                    textBox_Serial_text.Text += Encoding.GetEncoding("gb2312").GetString(buf, 0, len).Replace("\0", "").Trim() + "\r\n";
                }
                read_flg = false;
                if ((buf[0] == 0x4f) && (buf[1] == 0x4b))//error
                {
                    MessageBox.Show("Save OK!");
                    return;
                }
                else
                {
                    MessageBox.Show("Save error!");
                    return;
                }           
            }
            else
            {
                MessageBox.Show("Save error!");
                return;
            } 
        }

        private void button_readcfg_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                read_flg = false;
                serialPort1.Write("at+read_config\r\n");
                while (read_flg == false);
                ovtime = 1000000;
                while (read_flg == false)
                {
                    if ((ovtime--) < 2)
                        break;
                }
                if (read_flg != true)
                {
                    MessageBox.Show("Error!");
                    return;
                }
                if (read_flg == true)
                {
/*                    for (int i = 0; i < len; i++)
                    {
                        textBox_Serial_text.Text += Convert.ToString((char)buf[i]);
                    }
*/
                    textBox_Serial_text.Text += Encoding.GetEncoding("gb2312").GetString(buf, 0, len).Replace("\0", "").Trim() + "\r\n";
                }
                read_flg = false;
                if ((buf[0] == 0x4f) && (buf[1] == 0x4b))//ok
                {
                    System.Threading.Thread.Sleep(100);
                    textBox_cfg_text.Text = ""; 
/*                    for (int i = 2; i < len - 2; i++)
                     {
                         textBox_cfg_text.Text += Convert.ToChar(buf[i]);
                     }
*/
                    textBox_cfg_text.Text = Encoding.GetEncoding("gb2312").GetString(buf, 2, len - 2).Replace("\0", "").Trim();
                }
                else
                {
                    MessageBox.Show("Read error!");
                    return;
                }
            }
        }

        private void button_read_factorycfg_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                read_flg = false;
                serialPort1.Write("at+read_restoreconfig\r\n");
                while (read_flg == false) ;
                ovtime = 1000000;
                while (read_flg == false)
                {
                    if ((ovtime--) < 2)
                        break;
                }
                if (read_flg != true)
                {
                    MessageBox.Show("Error!");
                    return;
                }
                if (read_flg == true)
                {
/*                    for (int i = 0; i < len; i++)
                    {
                        textBox_Serial_text.Text += Convert.ToString((char)buf[i]);
                    }
*/
                    textBox_Serial_text.Text += Encoding.GetEncoding("gb2312").GetString(buf, 0, len).Replace("\0", "").Trim() + "\r\n";
                }
                read_flg = false;
                if ((buf[0] == 0x4f) && (buf[1] == 0x4b))//ok
                {
                    textBox_cfg_text.Text = "";
/*                    for (int i = 2; i < len - 2; i++)
                    {
                        textBox_cfg_text.Text += Convert.ToChar(buf[i]);
                    }
*/
                    textBox_cfg_text.Text = Encoding.GetEncoding("gb2312").GetString(buf, 2, len - 2).Replace("\0", "").Trim();
                }
                else
                {
                    MessageBox.Show("Read error!");
                    return;
                }
            }
        }

        private void button_save_cfg_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                string send_data = "";
                send_data += "at+write_config=";
                send_data += Encoding.GetEncoding("gb2312").GetBytes(textBox_cfg_text.Text).Length;
                send_data += ",";
                send_data += textBox_cfg_text.Text;
                send_data += "\r\n";
                read_flg = false;
                byte[] senddatas = Encoding.GetEncoding("gb2312").GetBytes(send_data);
                //serialPort1.Write(send_data);
                serialPort1.Write(senddatas, 0, senddatas.Length);
                while (read_flg == false) ;
                ovtime = 1000000;
                while (read_flg == false)
                {
                    if ((ovtime--) < 2)
                        break;
                }
                if (read_flg != true)
                {
                    MessageBox.Show("Error!");
                    return;
                }
                if (read_flg == true)
                {
/*                    for (int i = 0; i < len; i++)
                    {
                        textBox_Serial_text.Text += Convert.ToString((char)buf[i]);
                    }
*/
                    textBox_Serial_text.Text += Encoding.GetEncoding("gb2312").GetString(buf, 0, len).Replace("\0", "").Trim() + "\r\n";  
                }
                read_flg = false;
                if ((buf[0] == 0x4f) && (buf[1] == 0x4b)&&(len == 4))//ok
                {
                    MessageBox.Show("Save OK!");
                    return;
                }
                else
                {
                    MessageBox.Show("Save error!");
                    return;
                }
            }
        }

        private void button_save_factorycfg_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                string send_data = "";
                send_data += "at+write_restoreconfig=";
                send_data += Encoding.GetEncoding("gb2312").GetBytes(textBox_cfg_text.Text).Length;
                send_data += ",";
                send_data += textBox_cfg_text.Text;
                send_data += "\r\n";
                read_flg = false;
                byte[] senddatas = Encoding.GetEncoding("gb2312").GetBytes(send_data);
                //serialPort1.Write(send_data);
                serialPort1.Write(senddatas, 0, senddatas.Length);
                while (read_flg == false) ;
                ovtime = 1000000;
                while (read_flg == false)
                {
                    if ((ovtime--) < 2)
                        break;
                }
                if (read_flg != true)
                {
                    MessageBox.Show("Error!");
                    return;
                }
                if (read_flg == true)
                {
/*                    for (int i = 0; i < len; i++)
                        {
                            textBox_Serial_text.Text += Convert.ToString((char)buf[i]);
                        }
*/
                    textBox_Serial_text.Text += Encoding.GetEncoding("gb2312").GetString(buf, 0, len).Replace("\0", "").Trim() + "\r\n";
                }
                read_flg = false;
                if ((buf[0] == 0x4f) && (buf[1] == 0x4b) && (len == 4))//ok
                {
                    MessageBox.Show("Save OK!");
                    return;
                }
                else
                {
                    MessageBox.Show("Save error!");
                    return;
                }                
            }
        }

        private void linkLabelClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                textBox_Serial_text.Text = "";
            }
        }

        private void linkLabelClear2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                textBox_cfg_text.Text = "";
            }
        }

        
    }
}
