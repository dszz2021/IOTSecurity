using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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

using Message1;
using Message1.BodyD;
using Message1.BodyA;
using static Message1.BodyA.BodyA2;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Thread threadClient = null;
        Socket socketClient = null;

        public MainWindow()
        {
            InitializeComponent();
        }
        private delegate void textBoxDelegate(string msg);

        private void setTextBox(string msg)
        {
            this.textBox.Dispatcher.Invoke(new textBoxDelegate(outputAction), msg);
        }

        private void outputAction(string msg)
        {
            this.textBox.AppendText(msg);
            this.textBox.AppendText("\n\n");
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BodyD5 bodyD5 = new BodyD5("1","11","123");
            string bodyjson = JsonConvert.SerializeObject(bodyD5);         
            //BodyD1 t = JsonConvert.DeserializeObject<BodyD1>(bodyjson);
            Message message = new Message(0xd, 0x5, bodyjson);
            string messagJson = JsonConvert.SerializeObject(message);       
            ClientSendMsg(messagJson);
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
            IPEndPoint point = new IPEndPoint(address, 8899);

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
                    string[] striparr = strRevMsg.Split(new string[] { "\n" }, StringSplitOptions.None);
                    striparr = striparr.Where(s => !string.IsNullOrEmpty(s)).ToArray();

                    //this.setTextBox("服务器:" + GetCurrentTime() + "  " + strRevMsg );

                    foreach (string mes in striparr)
                    {
                        string mess = mes.TrimEnd('\r', '\n');
                        Message message = JsonConvert.DeserializeObject<Message>(mess);
                        //MessageBox.Show(message.head.thickType+" "+message.head.thinType);



                        switch (message.head.thickType)
                        {
                            case 0xa:
                                {
                                    switch (message.head.thinType)
                                    {

                                    }
                                    break;
                                }
                            case 0xb:
                                {
                                    switch (message.head.thinType)
                                    {

                                    }

                                    break;
                                }
                            case 0xc:
                                {
                                    switch (message.head.thinType)
                                    {

                                    }

                                    break;
                                }
                            case 0xd:
                                {
                                    switch (message.head.thinType)
                                    {
                                        case 0x8:
                                            {
                                                String strbody = message.body;

                                                //解密

                                                BodyD8 body = JsonConvert.DeserializeObject<BodyD8>(strbody);
                                                setTextBox("type:" + body.type % 16 + " " + body.cName + " " + body.score + " " + body.loadSuccessful);
                                                break;
                                            }
                                        case 0x9:
                                            {
                                                String strbody = message.body;

                                                //解密

                                                BodyD9 body = JsonConvert.DeserializeObject<BodyD9>(strbody);
                                                setTextBox("type:" + body.type % 16 + "  " + body.reason + " " + body.changeSuccessful);
                                                break;
                                            }
                                        case 0x10:
                                            {
                                                String strbody = message.body;

                                                //解密

                                                BodyD10 body = JsonConvert.DeserializeObject<BodyD10>(strbody);
                                                setTextBox("type:" + body.type % 16 + "  " + body.nameList);
                                                break;
                                            }
                                        case 0x11:
                                            {
                                                String strbody = message.body;

                                                //解密

                                                BodyD11 body = JsonConvert.DeserializeObject<BodyD11>(strbody);
                                                setTextBox("type:" + body.type % 16 + "  " + body.gameIdAndName);
                                                break;
                                            }
                                        case 0x12:
                                            {
                                                String strbody = message.body;

                                                //解密

                                                BodyD12 body = JsonConvert.DeserializeObject<BodyD12>(strbody);
                                                setTextBox("type:" + body.type % 16 + "  " + body.idGame + "  " + body.idRoom);
                                                break;
                                            }
                                        case 0x13:
                                            {
                                                String strbody = message.body;

                                                //解密

                                                BodyD13 body = JsonConvert.DeserializeObject<BodyD13>(strbody);

                                                setTextBox("type:" + body.type % 16 + "  " + body.idGame + "  " + body.idRoom + "  " + body.createRoomSuccessful);
                                                break;
                                            }
                                        case 0x14:
                                            {
                                                String strbody = message.body;

                                                //解密

                                                BodyD14 body = JsonConvert.DeserializeObject<BodyD14>(strbody);
                                                setTextBox("type:" + body.type % 16 + "  " + body.idClient + "  " + body.numberInRoom + "  " + body.text);
                                                break;
                                            }
                                        case 0x15:
                                            {
                                                String strbody = message.body;

                                                //解密

                                                BodyD15 body = JsonConvert.DeserializeObject<BodyD15>(strbody);
                                                setTextBox("type:" + body.type % 16 + "  " + body.joinSuccessful);
                                                break;
                                            }
                                        case 0x16:
                                            {
                                                String strbody = message.body;

                                                //解密

                                                BodyD16 body = JsonConvert.DeserializeObject<BodyD16>(strbody);
                                                setTextBox("type:" + body.type % 16 + "  " + body.name + "  " + body.text);
                                                break;
                                            }

                                    }

                                    break;
                                }
                            case 0xe:
                                {
                                    switch (message.head.thinType)
                                    {
                                        case 0x7:
                                            {
                                                String strbody = message.body;

                                                //解密

                                                BodyD1 body = JsonConvert.DeserializeObject<BodyD1>(strbody);
                                                setTextBox("type:" + body.type);
                                                break;
                                            }
                                        case 0x8:
                                            {
                                                String strbody = message.body;

                                                //解密

                                                BodyD1 body = JsonConvert.DeserializeObject<BodyD1>(strbody);
                                                setTextBox("type:" + body.type);
                                                break;
                                            }
                                        case 0x9:
                                            {
                                                String strbody = message.body;

                                                //解密

                                                BodyD1 body = JsonConvert.DeserializeObject<BodyD1>(strbody);
                                                setTextBox("type:" + body.type);
                                                break;
                                            }
                                        case 0x10:
                                            {
                                                String strbody = message.body;

                                                //解密

                                                BodyD1 body = JsonConvert.DeserializeObject<BodyD1>(strbody);
                                                setTextBox("type:" + body.type);
                                                break;
                                            }
                                        case 0x11:
                                            {
                                                String strbody = message.body;

                                                //解密

                                                BodyD1 body = JsonConvert.DeserializeObject<BodyD1>(strbody);
                                                setTextBox("type:" + body.type);
                                                break;
                                            }
                                        case 0x12:
                                            {
                                                String strbody = message.body;

                                                //解密

                                                BodyD1 body = JsonConvert.DeserializeObject<BodyD1>(strbody);
                                                setTextBox("type:" + body.type);
                                                break;
                                            }
                                        case 0x13:
                                            {
                                                String strbody = message.body;

                                                //解密

                                                BodyD1 body = JsonConvert.DeserializeObject<BodyD1>(strbody);
                                                setTextBox("type:" + body.type);
                                                break;
                                            }
                                        case 0x14:
                                            {
                                                String strbody = message.body;

                                                //解密

                                                BodyD1 body = JsonConvert.DeserializeObject<BodyD1>(strbody);
                                                setTextBox("type:" + body.type);
                                                break;
                                            }
                                        case 0x15:
                                            {
                                                String strbody = message.body;

                                                //解密

                                                BodyD1 body = JsonConvert.DeserializeObject<BodyD1>(strbody);
                                                setTextBox("type:" + body.type);
                                                break;
                                            }
                                        case 0x16:
                                            {
                                                String strbody = message.body;

                                                //解密

                                                BodyD1 body = JsonConvert.DeserializeObject<BodyD1>(strbody);
                                                setTextBox("type:" + body.type);
                                                break;
                                            }
                                        case 0x17:
                                            {
                                                String strbody = message.body;

                                                //解密

                                                BodyD1 body = JsonConvert.DeserializeObject<BodyD1>(strbody);
                                                setTextBox("type:" + body.type);
                                                break;
                                            }
                                        case 0x19:
                                            {
                                                String strbody = message.body;

                                                //解密

                                                BodyD1 body = JsonConvert.DeserializeObject<BodyD1>(strbody);
                                                setTextBox("type:" + body.type);
                                                break;
                                            }
                                        case 0x20:
                                            {
                                                String strbody = message.body;

                                                //解密

                                                BodyD1 body = JsonConvert.DeserializeObject<BodyD1>(strbody);
                                                setTextBox("type:" + body.type);
                                                break;
                                            }
                                    }
                                    break;
                                }
                            default:
                                {
                                    MessageBox.Show("defult");
                                    break;
                                }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("远程服务器已经中断连接" + "\r\n");
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
