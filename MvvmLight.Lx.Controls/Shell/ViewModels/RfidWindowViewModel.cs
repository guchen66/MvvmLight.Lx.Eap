using Microsoft.Win32;
using MvvmLight.Lx.Controls.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unity;
using ValueConverters;
using MvvmLight.Lx.Devices.RFID.Models;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace MvvmLight.Lx.Controls.Shell.ViewModels
{
    public class RfidWindowViewModel : BaseViewModel
    {
        public RfidWindowViewModel(IUnityContainer container) : base(container)
        {
            ConnectServerCommand = new RelayCommand(ExecuteConnect);
            DisconnectServerCommand = new RelayCommand(CloseClientFun);
            SendMessage = new RelayCommand(SendMessageFun);
        }
        private RfidModel  _tcpModel;

        public RfidModel TcpModel
        {
            get => _tcpModel;
            set => SetProperty(ref _tcpModel, value);
        }

        private string _logText;
        public string LogText
        {
            get { return _logText; }
            set { SetProperty(ref _logText, value); }
        }


        private string _sendText;
        public string SendText
        {
            get { return _sendText; }
            set { SetProperty(ref _sendText, value); }
        }


        public bool CloseClientThread;   // 用于关闭客户端接收消息线程
        public TcpClient DefaultClient;  //创建连接的TcpClient ,也就是服务器的Ip+端口
        private Thread CLientThreadReceive;  // 创建客户端接收服务器的线程变量
        public ICommand ConnectServerCommand { get; private set; }
        public ICommand DisconnectServerCommand { get; private set; }
        public ICommand SendMessage { get; private set; }


        NetworkStream _networkStream = null;          //建立连接服务端的数据流
        ///<summary>客户端连接到服务器</summary>>
        private void ExecuteConnect()
        {
            CloseClientThread = true;
            if (TcpModel.IP != null && TcpModel.Port != null)
            {
                try
                {
                    DefaultClient = new TcpClient();

                    IPAddress ip = IPAddress.Parse(TcpModel.IP.Trim());
                    DefaultClient.Connect(ip, Convert.ToInt32(TcpModel.Port.Trim()));
                    LogText += "连接成功......" + "\r\n";
                    _networkStream = DefaultClient.GetStream();
                    // 开启循环接收服务器消息的线程
                    CLientThreadReceive = new Thread(new ThreadStart(Receive));
                    CLientThreadReceive.IsBackground = true;
                    CLientThreadReceive.Start();

                }
                catch (Exception ex)
                {
                    LogText += "连接到服务器失败：" + ex.ToString() + "\r\n";
                }

            }
            else
            {
                LogText += "请输入IP/Port：" + "\r\n";
            }


        }


        private void Receive()
        {
            try
            {
                while (CloseClientThread)
                {
                    byte[] buffer = new byte[2048];
                    //实际接收到的字节数
                    int r = _networkStream.Read(buffer,0,buffer.Length);
                    if (r == 0)
                    {
                        LogText += "服务端断开连接........." + "\r\n";
                        break;
                    }
                    else
                    {
                        //判断发送的数据的类型
                        if (buffer[0] == 0)  //表示发送的是文字消息
                        {
                            string str = Encoding.Default.GetString(buffer, 1, r - 1);
                            LogText += "接收远程服务器:" + "发送的消息:" + str + "\r\n";
                        }
                        //表示发送的是文件
                        if (buffer[0] == 1)         // 这里后期改进为WPF中专用的
                        {
                            SaveFileDialog sfd = new SaveFileDialog();
                            sfd.InitialDirectory = @"";
                            sfd.Title = "请选择要保存的文件";
                            sfd.Filter = "所有文件|*.*";
                            sfd.ShowDialog();

                            string strPath = sfd.FileName;
                            using (FileStream fsWrite = new FileStream(strPath, FileMode.OpenOrCreate, FileAccess.Write))
                            {
                                fsWrite.Write(buffer, 1, r - 1);
                            }
                            LogText += "保存文件成功" + "\r\n";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                LogText += $"接收服务端发送的消息出错/与服务器断开连接:{ex.Message}" + "\r\n";
            }
        }

        private void SendMessageFun()
        {
            if (DefaultClient != null)
            {
                if (SendText != null)
                    try
                    {
                        string strMsg = SendText.Trim();
                        byte[] buffer = new byte[2048];
                        buffer = Encoding.Default.GetBytes(strMsg);
                        _networkStream.Write(buffer, 0, buffer.Length);
                        // int receive = _networkStream.BeginWrite(buffer,0,buffer.Length,new AsyncCallback(SendCallback),"");

                    }
                    catch (Exception ex)
                    {

                        LogText += "发送消息失败:" + ex.Message + "\r\n";

                    }
                else
                {
                    LogText += "请先输入要发送的信息:" + "\r\n";
                }

            }
            else
            {
                LogText += "请先连接到服务端:" + "\r\n";
            }

        }


        private void SendMessageDisconnect()
        {

            try
            {
                string strMsg = "disconnectNetwork";
                byte[] buffer = new byte[2048];
                buffer = Encoding.Default.GetBytes(strMsg);
                // int receive = DefaultClient.Send(buffer);

            }
            catch (Exception ex)
            {

                LogText += "发送断开连接消息出错:" + ex.Message + "\r\n";

            }

        }


        private void CloseClientFun()
        {

            // 发送消息告诉服务端，我将断开连接；如果客户端主动断开与服务端的连接，
            // 最好实现让服务端接收消息的线程函数退出循环阻塞状态，不然会引发服务端异常：

            // System.Net.Sockets.SocketException: 您的主机中的软件中止了一个已建立的连接。   
            //An established connection was aborted by the software in your host machine

            SendMessageDisconnect();

            if (DefaultClient != null)
            {

                //终止线程
                CloseClientThread = false;

                //关闭socket
                DefaultClient.Close();
            }
            else
            {
                LogText += "客户端未开启" + "\r\n";
            }
        }
    }
}
