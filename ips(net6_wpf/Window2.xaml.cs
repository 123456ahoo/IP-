using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;//获取某些东西
using System.Management;
using System;
using System.Diagnostics;
using System.Threading;



namespace WPF设置任务栏闪烁桌面图标
{
    /// <summary>
    /// Window2.xaml 的交互逻辑
    /// </summary>
    /// string output = "";
    public partial class Window2 : Window
    {

        public Window2()
        {
            InitializeComponent();

            myblocktime.Text = nowtime();
            getLocalIPAddressWithNetworkInterface(NetworkInterfaceType.Wireless80211);
            while (true)
            {
                //Thread.Sleep(1000);
                myblocktime.Text = nowtime();
            }

        }
        string customFormat = "yyyy-MM-dd HH:mm:ss";
        string nowtime()//获取时间
        {
            DateTime currentTime = DateTime.Now;
;
            string formattedTimeString = currentTime.ToString(customFormat);
            //myblocktime.Text = formattedTimeString;
            //getLocalIPAddressWithNetworkInterface(NetworkInterfaceType.Wireless80211);

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
            string output         = "无线局域网 IP:- - - - - - - - - - - - - - -";
            string outputMASK     = "无线局域网 Mask:- - - - - - - - - - - - - -";
            string outputDHCPTIME = "无线局域网 租约:- - - - - - - - - - - - - -";
            string outputMAC      = "无线局域网 Mac：- - - - - - - - - - - - - -";
            string outputcpu      = "CPU:- - - - - - - - - - - - - - - - - - - -";
            string outputcpumake  = "CPU制造厂:- - - - - - - - - - - - - - - - -";
            string outputwinname  = "windosnam:- - - - - - - - - - - - - - - - -";
            string outputjincheng = "windosnam:- - - - - - - - - - - - - - - - -";

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
                            outputMAC +=item.GetPhysicalAddress().ToString();
                            outputcpu += GetCpuName();
                            outputcpumake += GetCpuManufacturer();
                            outputwinname += Environment.MachineName;
                            p1();
                            
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
