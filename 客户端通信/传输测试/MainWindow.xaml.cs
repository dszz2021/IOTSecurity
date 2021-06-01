using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Thread threadClient = null;
        Socket socketClient = null;

        private delegate void textBoxDelegate(string msg);

        private void setTextBox(string msg)
        {
            this.textBox.Dispatcher.Invoke(new textBoxDelegate(outputAction), msg);
        }

        private void outputAction(string msg)
        {
            this.textBox.AppendText(msg);
            this.textBox.AppendText("\n");
        }
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /* string json = "{\"a\":\"12\",\"ready\":true,\"stringList\":[\"aaa\",\"bbb\"],\"map\":{\"123\":123,\"456\":123456789,\"789\":789}}";
             Test1 test1 = new Test1();
             test1 = JsonConvert.DeserializeObject<Test1>(json);

             MessageBox.Show(Convert.ToString(test1.map["456"]),"123");
             if (test1.ready)
             {
                 Console.WriteLine(test1.map["123"]);
             }
             Console.WriteLine(test1);

            Test2 test2 = new Test2("1111");
            MessageBox.Show(test2.aaaa);
            MessageBox.Show(test2.getA());
           */
            ClientSendMsg(sendTextBox.Text);


        }
        void ClientSendMsg(string sendMsg)
        {
            //将输入的内容字符串转换为机器可以识别的字节数组     
            byte[] arrClientSendMsg = Encoding.UTF8.GetBytes(sendMsg+"\n");
            //调用客户端套接字发送字节数组     
            socketClient.Send(arrClientSendMsg);
            
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress address = IPAddress.Parse("127.0.0.1");
            IPEndPoint point = new IPEndPoint(address, 8080);

            try
            {
                socketClient.Connect(point);

            }
            catch (Exception)
            {
                Debug.WriteLine("连接失败\r\n");
                return;
            }
            threadClient = new Thread(recv);
            threadClient.IsBackground = true;
            threadClient.Start();
        }
        void recv()
        {
            int x = 0;
            //持续监听服务端发来的消息 
            while (true)
            {
                try
                {
                    //定义一个1M的内存缓冲区，用于临时性存储接收到的消息  
                    byte[] arrRecvmsg = new byte[1024 * 1024];

                    //将客户端套接字接收到的数据存入内存缓冲区，并获取长度  
                    int length = socketClient.Receive(arrRecvmsg);

                    //将套接字获取到的字符数组转换为人可以看懂的字符串  
                    string strRevMsg = Encoding.UTF8.GetString(arrRecvmsg, 0, length);
                    if (x == 1)
                    {
                        this.setTextBox("服务器:" + GetCurrentTime() + "\r\n" + strRevMsg + "\r\n\n");
                        Debug.WriteLine("服务器:" + GetCurrentTime() + "\r\n" + strRevMsg + "\r\n\n");
                    }
                    else
                    {
                        this.setTextBox(strRevMsg + "\r\n\n");
                        Debug.WriteLine(strRevMsg + "\r\n\n");
                        x = 1;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("远程服务器已经中断连接" + "\r\n\n");
                    Debug.WriteLine("远程服务器已经中断连接" + "\r\n");
                    break;
                }
            }
        }
        DateTime GetCurrentTime()
        {
            DateTime currentTime = new DateTime();
            currentTime = DateTime.Now;
            return currentTime;
        }

}


}
