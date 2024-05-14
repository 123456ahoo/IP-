using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Management;
using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;//获取某些东西
using System.Management;
using System;
using System.Diagnostics;
using System.Threading;

//using System.Windows.Forms;
//引用动态库 Drawing

namespace WPF设置任务栏闪烁桌面图标
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window 
    {
        
        //NotifyIcon
        NotifyIcon a;
        string output = "";
        string outputMASK = "";
        string outputDHCPTIME = "";
        string outputMAC = "";
        string outputcpu = "";
        string outputcpumake = "";
        string outputwinname = "";
        string outputjincheng = "";
        string customFormat = "yyyy-MM-dd HH:mm:ss";

        public MainWindow()
        {
            InitializeComponent();
            aaa(); //对aaa进行初始化 
        }
        private void aaa()
        {
            a = new NotifyIcon();
            a.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);
            //设置图标
            a.Text = "我的应用程序";//设置名称
            a.Visible = true;//设置可见性
            System.Windows.Forms.MenuItem menuItem = new System.Windows.Forms.MenuItem("quit");

            menuItem.Click += MenuItem_Click;

            System.Windows.Forms.MenuItem menuItem1 = new System.Windows.Forms.MenuItem("关于");
            menuItem1.Click += MenuItem1_Click;

            System.Windows.Forms.MenuItem[] menuItems = new System.Windows.Forms.MenuItem[] { menuItem, menuItem1 };

            a.ContextMenu = new System.Windows.Forms.ContextMenu(menuItems);
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            a.Dispose();
            this.Close();
        }

        private void MenuItem1_Click(object sender, EventArgs e)
        {
            Window2 login1 = new Window2();
            login1.Show();
            this.Close();
            System.Windows.MessageBox.Show("关于程序的信息");
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //StringBuilder hardwareInfo = new StringBuilder();
            //System.Windows.MessageBox.Show(hardwareInfo.ToString());
            //Window2 login1 = new Window2();
            myblocktime.Text = nowtime();

            // login1.Show();
            // this.Close();
        }

        private void tbx_mac_TextChanged()
        {

        }
        string nowtime()
        {
            DateTime currentTime = DateTime.Now;
            //string customFormat = "yyyy-MM-dd HH:mm:ss";
            string formattedTimeString = currentTime.ToString(customFormat);
            myblocktime.Text = formattedTimeString;
            getLocalIPAddressWithNetworkInterface(NetworkInterfaceType.Wireless80211);

            return formattedTimeString;
        }
        public static string GetCpuName()//获取CPU信息
        {

            var CPUName = "";
            var management = new ManagementObjectSearcher("Select * from Win32_Processor");
            foreach (var baseObject in management.Get())
            {
                var managementObject = (ManagementObject)baseObject;
                CPUName = managementObject["Name"].ToString();
            }
            return CPUName;
        }
        /// <summary>
        /// CPU制造厂商
        /// </summary>
        /// <returns></returns>
        public static string GetCpuManufacturer()//CPU制造厂商
        {
            var CPUMakerStr = "";
            var management = new ManagementObjectSearcher("Select * from Win32_Processor");
            foreach (var baseObject in management.Get())
            {
                var managementObject = (ManagementObject)baseObject;
                CPUMakerStr = managementObject["Manufacturer"].ToString();
            }
            return CPUMakerStr;
        }
        public static string p1()//获取进程数
        {
            int processCount = Process.GetProcesses().Length;

            return processCount.ToString();

        }

        void getLocalIPAddressWithNetworkInterface(NetworkInterfaceType _type)
        {
            //string output = "";
            //string outputMASK = "";
            //string outputDHCPTIME = "";
            //string outputMAC = "";
            //string outputcpu = "";
            //string outputcpumake = "";
            //string outputwinname = "";
            //string outputjincheng = "";

            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())//NetworkInterface 提供网络接口的配置 和统计信息 重点统计信息 
            {
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)//
                {
                    //UnicastIPAddressInformation （.Net 7
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)// UnicastIPAddressInformation提高网络接口的有关单播地址
                    {
                        //.Address 方法
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)//获取IP v4地址 或者 IP v6地址  InterNetwork （对于 IPv4，返回 InterNetwork） 
                                                                                   // 将迭代的地址 与 IPV4 匹配如果是本机的 IPv4  则计入
                        {
                            output += ip.Address.ToString();
                            outputMASK += ip.IPv4Mask.ToString();
                            outputDHCPTIME += ip.DhcpLeaseLifetime.ToString();
                            outputMAC += item.GetPhysicalAddress().ToString();
                            outputcpu += GetCpuName();
                            outputcpumake += GetCpuManufacturer();
                            outputwinname += Environment.MachineName;
                            p1();
                            //BLOCKip.Text = output;

                        }
                    }
                }
            }


            myblockIP.Text = output;
            myblockIP2.Text = outputMASK;
            myblockIP3.Text = outputDHCPTIME;
            myblockIP4.Text = outputMAC;
            myblockIP5.Text = outputcpu;
            myblockIP6.Text = outputcpumake;
            myblockIP7.Text = outputwinname;
        }


    }
}

