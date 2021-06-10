using System;
using Message1;
using System.Text;
using System.Windows;
using Message1.BodyD;
using Newtonsoft.Json;
using System.Collections;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace WpfApp1
{
    public partial class Window2 : Window
    {
        Socket socketClient = null;
        Window4 DDZ = null;
        static int NumOfPic { set; get; }
        public Window2()
        {
            InitializeComponent();
        }
        public Window2(MainWindow w, Socket socket)
        {
            socketClient = socket;
            //string myID = account.Text;
            //DDZ = new Window4(this, myID, socketClient);
            //w.SetDDZ(DDZ);
            InitializeComponent();
        }
        //获取游戏ID
        public string GetGameID()
        {
            if (gameLV.SelectedItem is GameMessage GM && GM is GameMessage)
                return GM.GameNum;
            return null;
        }
        //获取房间ID
        public string GetRoomID()
        {
            if (roomLV.SelectedItem is RoomMessage RM && RM is RoomMessage)
                return RM.RoomName;
            return null;
        }
        //发送报文 -> 服务器
        public void ClientSendMsg(string sendMsg)
        {
            //将输入的内容字符串转换为机器可以识别的字节数组     
            byte[] arrClientSendMsg = Encoding.UTF8.GetBytes(sendMsg + "\n");
            //调用客户端套接字发送字节数组     
            socketClient.Send(arrClientSendMsg);
        }
        //设置当前头像
        public void SetNumOfPic(int num)
        {
            NumOfPic = num;
        }
        //设置输出信息
        public void OutPutMessageBox(string str)
        {
            MessageBox.Show(str);
        }
        //创建房间按钮
        private void Button_SetUp_Click(object sender, RoutedEventArgs e)
        {
            //进入创建房间窗口
            Window setupRoom = new Window3();
            Hide();
            setupRoom.ShowDialog();
            Show();
        }
        //加入房间按钮
        private void Button_Join_Click(object sender, RoutedEventArgs e)
        {
            //获取游戏ID
            string gameID = "";
            if (gameLV.SelectedItem is GameMessage GM && GM is GameMessage)
                gameID = GM.GameNum;
            //获取房间ID
            string roomID = "";
            if (roomLV.SelectedItem is RoomMessage RM && RM is RoomMessage)
                roomID = RM.RoomName;
            //检验是否选中房间ListView其中一条
            if (roomLV.SelectedItem != null)
            {
                //封装报文
                BodyD5 bodyD5 = new BodyD5(gameID, roomID, account.Text);
                string bodyjson = JsonConvert.SerializeObject(bodyD5);
                Message message = new Message(0xD, 0x5, bodyjson);
                string messagJson = JsonConvert.SerializeObject(message);
                //向服务器发送报文
                ClientSendMsg(messagJson);
            }
        }
        //修改昵称按钮
        private void Button_Modify_Click(object sender, RoutedEventArgs e)
        {
            /*
             * 修改按钮点击后：
             * 个人昵称 TextBox -> isEnabled 属性 true ；修改按钮文本Text -> “确定”
             * 此时输入要修改的昵称，然后点击"确定"按钮即可修改昵称
             * 确定后修改按钮文本Text -> “修改”
             */
            if (modify.Content.Equals("修改"))
            {
                modify.Content = "确定";
                nickName.IsEnabled = true;
                //获取ID和昵称
                string ID = account.Text;
                string modifyStr = nickName.Text;
                //封装报文
                BodyD2 bodyD2 = new BodyD2(ID, modifyStr);
                string bodyjson = JsonConvert.SerializeObject(bodyD2);
                Message message = new Message(0xD, 0x2, bodyjson);
                string messagJson = JsonConvert.SerializeObject(message);
                //向服务器发送报文
                ClientSendMsg(messagJson);
            }
        }
        //发送信息按钮
        private void Button_Send_Click(object sender, RoutedEventArgs e)
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //获取ID和发送框中的内容
            string ID = account.Text;
            string str = "[" + time + "]" + nickName.Text + "：" + sendMes.Text + "\r\n";
            //封装报文
            BodyD6 bodyD6 = new BodyD6(ID, str);
            string bodyjson = JsonConvert.SerializeObject(bodyD6);
            Message message = new Message(0xD, 0x6, bodyjson);
            string messagJson = JsonConvert.SerializeObject(message);
            //向服务器发送报文
            if (!sendMes.Text.Equals(""))
                ClientSendMsg(messagJson);
            //滚动到光标处
            publicChannel.ScrollToEnd();
            sendMes.Text = null;
        }
        //公共发言信息刷新
        public void Public_Channel(string str)
        {
            publicChannel.Text += str;
        }
        //窗体加载事件 -> 加载头像
        private void GameHallLoad(object sender, RoutedEventArgs e)
        {
            //初始化个人头像
            Random ran = new Random();
            NumOfPic = ran.Next(1, 9); ;
            string path = "\\Resources\\Head\\" + NumOfPic + ".jpg";
            headPicture.Source = new BitmapImage(new Uri(path, UriKind.Relative));
            /*          
            account.Text = "1131684544";
            nickName.Text = "Gouzia-";
            happyBeen.Text = "9999999";
            */
        }
        //初始化大厅用户列表
        public void SetUserLV(ArrayList arrayList)
        {
            int num = 0;
            foreach (string str in arrayList)
            {
                num++;
                userLV.Items.Add(new { userName = str });
            }
            numOfPeople.Text = num.ToString();
        }
        //初始化游戏列表
        public void SetGameLV(Dictionary<string, string> dictionary)
        {
            foreach (KeyValuePair<string, string> kv in dictionary)
                gameLV.Items.Add(new GameMessage(kv.Key, kv.Value));
        }
        //房间信息加载（ListView选中行改变时加载）
        private void RoomLoad(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            roomDetail.Text = null;
            roomLV.Items.Clear();
            //获取游戏ID
            string gameID = "";
            if (gameLV.SelectedItem is GameMessage GM && GM is GameMessage)
                gameID = GM.GameNum;
            //封装报文
            BodyD3 bodyD3 = new BodyD3(gameID);
            string bodyjson = JsonConvert.SerializeObject(bodyD3);
            Message message = new Message(0xD, 0x3, bodyjson);
            string messagJson = JsonConvert.SerializeObject(message);
            //向服务器发送报文
            ClientSendMsg(messagJson);
        }
        //初始化房间列表
        public void SetRoomLV(ArrayList arrayList)
        {
            foreach (string str in arrayList)
                roomLV.Items.Add(new RoomMessage(str));
        }
        //详细房间信息加载（ListView选中行改变时加载）
        private void RoomDetail(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            roomDetail.Text = null;
            //获取游戏ID
            string gameID = "";
            if (gameLV.SelectedItem is GameMessage GM && GM is GameMessage)
                gameID = GM.GameNum;
            //获取房间ID
            string roomID = "";
            if (roomLV.SelectedItem is RoomMessage RM && RM is RoomMessage)
                roomID = RM.RoomName;
            //封装报文
            BodyD7 bodyD7 = new BodyD7(gameID, roomID);
            string bodyjson = JsonConvert.SerializeObject(bodyD7);
            Message message = new Message(0xD, 0x7, bodyjson);
            string messagJson = JsonConvert.SerializeObject(message);
            //向服务器发送报文
            ClientSendMsg(messagJson);
        }
        //回车键发送个人信息
        private void GameHall_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            object sender1 = new object();
            RoutedEventArgs e1 = new RoutedEventArgs();
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                if (modify.Content.Equals("确定"))
                    Button_Modify_Click(sender1, e1);
                else if (sendMes.Focus())
                    Button_Send_Click(sender1, e1);
            }
        }
        //头像更改事件
        private void ChangeHead(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Window CH = new HeadPicture(this);
            CH.ShowDialog();
            string path = "\\Resources\\Head\\" + NumOfPic + ".jpg";
            headPicture.Source = new BitmapImage(new Uri(path, UriKind.Relative));
        }
    }
    class RoomMessage
    {
        public string RoomName { set; get; }

        public RoomMessage(string Name)
        {
            RoomName = Name;
        }
    }
    class GameMessage
    {
        public string GameNum { set; get; }
        public string GameName { set; get; }

        public GameMessage(string Num, string Name)
        {
            GameNum = Num;
            GameName = Name;
        }
    }
}