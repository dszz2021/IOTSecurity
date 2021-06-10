using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;

using Message1;
using Message1.BodyA;
using Message1.BodyD;
using Message1.BodyE;
using Message1.BodyB;
using Message1.BodyC;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        string IDtgs = "1";
        string IDv = "1";
        string TICKETtgs;
        string TICKETv;
        string KEYc_tgs;
        string KEYc_v;
        //游戏大厅
        static Window2 gameHall;
        //斗地主
        static Window4 douDiZhu;
        Thread threadClient = null;
        Socket socketClient = null;
        public MainWindow()
        {
            Window Show_Message = new Window5();
            Show_Message.Show();
            InitializeComponent();
            account.Focus();
            gameHall = new Window2(this, socketClient);


        }
        //设置斗地主对象
        public void SetDDZ(Window4 DDZ)
        {
            douDiZhu = DDZ;
        }
        public string GetID()
        {
            return account.Text;
        }
        // 登录按钮
        private void Button_enroll_Click(object sender, RoutedEventArgs e)
        {
            //获取窗口键入的账号and密码
            string ID = account.Text;
            string PW = password.Password;
            //如果AS服务器完成认证则进行TGS服务器认证
            if (AS_Socket(ID, PW))
            {
                //如果TGS服务器完成认证则进行服务器认证
                if (TGS_Socket())
                {
                    //建立服务器socket连接
                    socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPAddress address = IPAddress.Parse("192.168.43.16");
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
                    //服务器认证报文
                    BodyC1 bodyC1 = new BodyC1(TICKETv, new Authenticator(account.Text, "", GetCurrentTime().ToString()).ToString());
                    string bodyjson1 = JsonConvert.SerializeObject(bodyC1);
                    Message message1 = new Message(0xC, 0x1, bodyjson1);
                    string messagJson1 = JsonConvert.SerializeObject(message1);
                    //向服务器发送报文
                    byte[] arrClientSendMsg1 = Encoding.UTF8.GetBytes(messagJson1 + "\n");
                    socketClient.Send(arrClientSendMsg1);
                    //建立线程持续监听
                    threadClient = new Thread(Recv)
                    {
                        IsBackground = true
                    };
                    threadClient.Start();
                }
            }
        }
        //验证AS服务器
        public bool AS_Socket(string id, string pw)
        {
            //建立AS服务器socket连接
            Socket socketAS = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress address = IPAddress.Parse("192.168.43.16");
            IPEndPoint point = new IPEndPoint(address, 8080);
            try
            {
                socketAS.Connect(point);
            }
            catch (Exception)
            {
                Debug.WriteLine("连接失败！\r\n");
                return false;
            }
            //封装报文
            BodyA1 bodyA1 = new BodyA1(id, IDtgs, GetCurrentTime().ToString());
            string bodyjson = JsonConvert.SerializeObject(bodyA1);
            Message message = new Message(0xA, 0x1, bodyjson);
            string messagJson = JsonConvert.SerializeObject(message);

            byte[] arrClientSendMsg = Encoding.UTF8.GetBytes(messagJson + "\n");
            socketAS.Send(arrClientSendMsg);

            byte[] arrRecvmsg = new byte[1024 * 1024];
            int length = socketAS.Receive(arrRecvmsg);
            string strRevMsg = Encoding.UTF8.GetString(arrRecvmsg, 0, length);

            string[] striparr = strRevMsg.Split(new string[] { "\n" }, StringSplitOptions.None);
            striparr = striparr.Where(s => !string.IsNullOrEmpty(s)).ToArray();
            foreach (string mes in striparr)
            {
                string mess = mes.TrimEnd('\r', '\n');
                Message message0 = JsonConvert.DeserializeObject<Message>(mes);
                switch (message0.head.thickType)
                {
                    case 0xa:
                        {
                            String strbody = message0.body;
                            //DES解密
                            DES des = new DES();
                            string plain = des.deCipher(strbody, pw);
                            BodyA2 body = JsonConvert.DeserializeObject<BodyA2>(plain);
                            if (body.IDtgs == IDtgs)
                            {
                                KEYc_tgs = body.KeyCandTgs;
                                TICKETtgs = body.TicketTgs;
                                MessageBox.Show("AS认证成功！");
                                return true;
                            }
                            else
                                MessageBox.Show("AS认证失败，请稍后重试！");
                            break;
                        }
                }
            }
            return false;
        }
        //验证TGS服务器
        public bool TGS_Socket()
        {
            //建立TGS服务器socket连接
            Socket socketTGS = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress address = IPAddress.Parse("192.168.43.16");
            IPEndPoint point = new IPEndPoint(address, 8181);
            try
            {
                socketTGS.Connect(point);
            }
            catch (Exception)
            {
                Debug.WriteLine("连接失败\r\n");
                return false;
            }
            //TGS认证报文
            BodyB1 bodyB1 = new BodyB1(IDv, TICKETtgs, new Authenticator(account.Text, " ", GetCurrentTime().ToString()).ToString());
            string bodyjson1 = JsonConvert.SerializeObject(bodyB1);
            Message message1 = new Message(0xB, 0x1, bodyjson1);
            string messagJson1 = JsonConvert.SerializeObject(message1);
            //向TGS服务器发送报文
            byte[] arrClientSendMsg1 = Encoding.UTF8.GetBytes(messagJson1 + "\n");
            socketTGS.Send(arrClientSendMsg1);

            byte[] arrRecvmsg = new byte[1024 * 1024];
            int length = socketTGS.Receive(arrRecvmsg);
            string strRevMsg = Encoding.UTF8.GetString(arrRecvmsg, 0, length);

            string[] striparr = strRevMsg.Split(new string[] { "\n" }, StringSplitOptions.None);
            striparr = striparr.Where(s => !string.IsNullOrEmpty(s)).ToArray();
            foreach (string mes in striparr)
            {
                string mess = mes.TrimEnd('\r', '\n');
                Message message0 = JsonConvert.DeserializeObject<Message>(mes);
                switch (message0.head.thickType)
                {
                    case 0xb:
                        {
                            String strbody = message0.body;
                            //DES解密
                            DES des = new DES();
                            string plain = des.deCipher(strbody, KEYc_tgs);
                            BodyB2 body = JsonConvert.DeserializeObject<BodyB2>(plain);
                            if (body.IDv == IDv)
                            {
                                KEYc_v = body.KeyCAndV;
                                TICKETv = body.TicketV;
                                MessageBox.Show("TGS认证成功！");
                            }
                            else
                                MessageBox.Show("TGS认证失败，请稍后重试！");
                            break;
                        }
                }
            }
            return false;
        }

        //发送报文 -> 服务器
        void ClientSendMsg(string sendMsg)
        {
            //将输入的内容字符串转换为机器可以识别的字节数组     
            byte[] arrClientSendMsg = Encoding.UTF8.GetBytes(sendMsg + "\n");
            //调用客户端套接字发送字节数组     
            socketClient.Send(arrClientSendMsg);
        }
        //接收报文 <- 服务器
        void Recv()
        {
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
                    foreach (string mes in striparr)
                    {
                        string mess = mes.TrimEnd('\r', '\n');
                        Message message = JsonConvert.DeserializeObject<Message>(mess);
                        switch (message.head.thickType)
                        {
                            case 0xc:
                                {
                                    String strbody = message.body;
                                    //DES解密
                                    DES des = new DES();
                                    string plain = des.deCipher(strbody, KEYc_v);
                                    BodyC2 body = JsonConvert.DeserializeObject<BodyC2>(plain);
                                    if (true)
                                        MessageBox.Show("服务器认证成功！");
                                    break;
                                }
                            case 0xd:
                                {
                                    switch (message.head.thinType)
                                    {
                                        case 0x8:
                                            {
                                                String strbody = message.body;
                                                //DES解密

                                                //如果无法解密，则说明当前密码错误
                                                if (true)
                                                    MessageBox.Show("密码错误！");
                                                BodyD8 body = JsonConvert.DeserializeObject<BodyD8>(strbody);
                                                if (body.loadSuccessful)
                                                {
                                                    //游戏大厅界面
                                                    gameHall.Show();
                                                    gameHall.account.Text = account.Text;
                                                    gameHall.nickName.Text = body.cName;
                                                    gameHall.happyBeen.Text = body.score;
                                                    Hide();
                                                }
                                                else
                                                    MessageBox.Show("该账号不存在！");
                                                break;
                                            }
                                        case 0x9:
                                            {
                                                String strbody = message.body;
                                                //DES解密

                                                BodyD9 body = JsonConvert.DeserializeObject<BodyD9>(strbody);
                                                if (body.changeSuccessful)
                                                {
                                                    gameHall.modify.Content = "修改";
                                                    gameHall.nickName.IsEnabled = false;
                                                    gameHall.OutPutMessageBox("信息修改成功！");
                                                }
                                                else
                                                    gameHall.OutPutMessageBox("信息修改失败，请稍后重试！");
                                                break;
                                            }
                                        case 0x10:
                                            {
                                                String strbody = message.body;
                                                //DES解密

                                                BodyD10 body = JsonConvert.DeserializeObject<BodyD10>(strbody);
                                                gameHall.SetUserLV(body.nameList);
                                                break;
                                            }
                                        case 0x11:
                                            {
                                                String strbody = message.body;
                                                //DES解密

                                                BodyD11 body = JsonConvert.DeserializeObject<BodyD11>(strbody);
                                                gameHall.SetGameLV(body.gameIdAndName);
                                                break;
                                            }
                                        case 0x12:
                                            {
                                                String strbody = message.body;
                                                //DES解密

                                                BodyD12 body = JsonConvert.DeserializeObject<BodyD12>(strbody);
                                                gameHall.SetRoomLV(body.idRoom);
                                                break;
                                            }
                                        case 0x13:
                                            {
                                                String strbody = message.body;
                                                //DES解密

                                                BodyD13 body = JsonConvert.DeserializeObject<BodyD13>(strbody);
                                                if (body.createRoomSuccessful)
                                                {
                                                    douDiZhu.Show();
                                                    gameHall.Hide();
                                                }
                                                else
                                                    gameHall.OutPutMessageBox("创建房间失败，请稍后重试！");
                                                break;
                                            }
                                        case 0x14:
                                            {
                                                String strbody = message.body;
                                                //DES解密

                                                BodyD14 body = JsonConvert.DeserializeObject<BodyD14>(strbody);
                                                gameHall.roomDetail.Text = null;
                                                gameHall.roomDetail.Text = "房主账号：" + body.idClient + "\r\n"
                                                    + "房间备注：" + body.text + "\r\n"
                                                    + "当前人数：" + body.numberInRoom + "\r\n";
                                                break;
                                            }
                                        case 0x15:
                                            {
                                                String strbody = message.body;
                                                //DES解密

                                                BodyD15 body = JsonConvert.DeserializeObject<BodyD15>(strbody);
                                                if (body.joinSuccessful)
                                                {
                                                    gameHall.Hide();
                                                    douDiZhu.ShowDialog();
                                                    gameHall.Show();
                                                }
                                                else
                                                    gameHall.OutPutMessageBox("加入房间失败，请刷新列表重试！");
                                                break;
                                            }
                                        case 0x16:
                                            {
                                                String strbody = message.body;
                                                //DES解密
                                                BodyD16 body = JsonConvert.DeserializeObject<BodyD16>(strbody);
                                                gameHall.Public_Channel(body.text);
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
                                                //DES解密

                                                BodyE7 body = JsonConvert.DeserializeObject<BodyE7>(strbody);
                                                douDiZhu.SetOutMessage(body.clientID, body.ready, "已准备", "未准备");
                                                break;
                                            }
                                        case 0x8:
                                            {
                                                String strbody = message.body;
                                                //DES解密

                                                BodyE8 body = JsonConvert.DeserializeObject<BodyE8>(strbody);
                                                douDiZhu.GetCardCollection(body.cardInformation);
                                                //游戏循环-->发牌阶段
                                                douDiZhu.Cycle_Game_State(Window4.GameState.dealCard);
                                                break;
                                            }
                                        case 0x9:
                                            {
                                                String strbody = message.body;
                                                //DES解密

                                                BodyE9 body = JsonConvert.DeserializeObject<BodyE9>(strbody);
                                                douDiZhu.SetOutMessage(body.clientID, body.chase, "抢地主", "不抢");
                                                //设置当前倍数
                                                douDiZhu.SetMultiple(body.multiple);
                                                break;
                                            }
                                        case 0x10:
                                            {
                                                String strbody = message.body;
                                                //DES解密

                                                BodyE10 body = JsonConvert.DeserializeObject<BodyE10>(strbody);
                                                //底牌交给地主并设置地主头像
                                                douDiZhu.Landlord_Cards(body.clientID);
                                                //设置当前倍数
                                                douDiZhu.SetMultiple(body.multiple);
                                                //游戏循环-->加倍阶段
                                                douDiZhu.Cycle_Game_State(Window4.GameState.redouble);
                                                break;
                                            }
                                        case 0x11:
                                            {
                                                String strbody = message.body;
                                                //DES解密

                                                BodyE11 body = JsonConvert.DeserializeObject<BodyE11>(strbody);
                                                douDiZhu.SetOutMessage(body.clientID, body.doubleness, "加倍", "不加倍");
                                                //设置当前倍数
                                                douDiZhu.SetMultiple(body.multiple);
                                                break;
                                            }
                                        case 0x12:
                                            {
                                                String strbody = message.body;
                                                //DES解密

                                                BodyE12 body = JsonConvert.DeserializeObject<BodyE12>(strbody);
                                                //如果出牌玩家是自己
                                                int num = douDiZhu.GetPlayer(body.clientID);
                                                //游戏循环-->出牌阶段
                                                if (num == 1)
                                                    douDiZhu.Cycle_Game_State(Window4.GameState.playCard);
                                                break;
                                            }
                                        case 0x13:
                                            {
                                                String strbody = message.body;
                                                //DES解密

                                                BodyE13 body = JsonConvert.DeserializeObject<BodyE13>(strbody);
                                                int location = douDiZhu.GetPlayer(body.clientID);
                                                if (location != -1 && location != 1)
                                                {
                                                    douDiZhu.SetLastType(body.cardType);
                                                    douDiZhu.SetOutPutCards(body.cardInformation, body.clientID);
                                                }
                                                //设置当前倍数
                                                douDiZhu.SetMultiple(body.multiple);
                                                break;
                                            }
                                        case 0x14:
                                            {
                                                String strbody = message.body;
                                                //DES解密

                                                BodyE14 body = JsonConvert.DeserializeObject<BodyE14>(strbody);
                                                //游戏循环-->结算阶段
                                                douDiZhu.SetSettleMent(body.cIDAndScore);
                                                douDiZhu.Cycle_Game_State(Window4.GameState.settlement);
                                                break;
                                            }
                                        case 0x15:
                                            {
                                                String strbody = message.body;
                                                //DES解密

                                                BodyE15 body = JsonConvert.DeserializeObject<BodyE15>(strbody);
                                                douDiZhu.UnexpectedExit(body.clientID);
                                                break;
                                            }
                                        case 0x16:
                                            {
                                                String strbody = message.body;
                                                //DES解密

                                                BodyE16 body = JsonConvert.DeserializeObject<BodyE16>(strbody);
                                                int i = douDiZhu.GetPlayer(body.clientID);
                                                if (i == 1)
                                                {
                                                    //游戏循环-->抢地主阶段
                                                    douDiZhu.Cycle_Game_State(Window4.GameState.grab);
                                                }
                                                break;
                                            }
                                        case 0x17:
                                            {
                                                String strbody = message.body;
                                                //DES解密

                                                BodyE17 body = JsonConvert.DeserializeObject<BodyE17>(strbody);
                                                break;
                                            }
                                        case 0x19:
                                            {
                                                String strbody = message.body;
                                                //DES解密

                                                BodyE19 body = JsonConvert.DeserializeObject<BodyE19>(strbody);
                                                break;
                                            }
                                        case 0x20:
                                            {
                                                String strbody = message.body;
                                                //DES解密

                                                BodyE20 body = JsonConvert.DeserializeObject<BodyE20>(strbody);
                                                douDiZhu.multiple.Visibility = Visibility.Visible;
                                                douDiZhu.SetMultiple(1);
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
                    Debug.WriteLine(ex + "远程服务器已经中断连接" + "\r\n");
                    break;
                }
            }
        }

        DateTime GetCurrentTime()
        {
            _ = new DateTime();
            DateTime currentTime = DateTime.Now;
            return currentTime;
        }

        //回车键实现登录
        private void Enroll_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            object sender1 = new object();
            RoutedEventArgs e1 = new RoutedEventArgs();
            if (e.Key == System.Windows.Input.Key.Enter)
                Button_enroll_Click(sender1, e1);
        }
    }
}