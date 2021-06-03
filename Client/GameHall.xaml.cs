using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WpfApp1
{
    /// <summary>
    /// Window2.xaml 的交互逻辑
    /// </summary>
    public partial class Window2 : Window
    {
        static int NumOfPic { set; get; }
        public Window2()
        {
            InitializeComponent();
        }
        public void SetNumOfPic(int num)
        {
            NumOfPic = num;
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
            //检验是否选中 房间ListView 其中一条

            //选中进行后续操作（没选中没有操作)
            Window douDiZhu = new Window4();
            Hide();
            douDiZhu.ShowDialog();
            Show();
        }

        //修改按钮
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
            }
            else
            {
                modify.Content = "修改";
                nickName.IsEnabled = false;
            }
        }

        //发送信息按钮
        private void Button_Send_Click(object sender, RoutedEventArgs e)
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //获取发送框中的内容
            string str = "[" + time + "]" + nickName.Text + "：" + sendMes.Text + "\r\n";
            if (!sendMes.Text.Equals(""))
            {
                //将个人发言发送到服务器
                Public_Channel(str);
            }
            //滚动到光标处
            publicChannel.ScrollToEnd();
            sendMes.Text = null;
        }

        /*
         * 功能：公共频道信息刷新
         * 参数：str-当前新加的聊天信息
         */
        private void Public_Channel(string str)
        {
            publicChannel.Text += str;
        }

        //窗体加载事件（窗口生成时加载）
        private void GameHallLoad(object sender, RoutedEventArgs e)
        {
            //初始化个人信息
            NumOfPic = 5;
            string path = "\\Resources\\Head\\" + NumOfPic + ".jpg";
            headPicture.Source = new BitmapImage(new Uri(path, UriKind.Relative));
            account.Text = "1131684544";
            nickName.Text = "Gouzia-";
            happyBeen.Text = "9999999";

            //初始化游戏列表
            gameLV.Items.Add(new { gameName = "DSZZ斗地主" });
            gameLV.Items.Add(new { gameName = "DSZZ跑得快" });

            //初始化大厅用户列表
            numOfPeople.Text = 2.ToString();
            userLV.Items.Add(new { userName = "Gouzia-" });
            userLV.Items.Add(new { userName = "J" });
        }
        //房间信息加载（ListView选中行改变时加载）
        private void RoomLoad(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            roomDetail.Text = null;
            roomLV.Items.Clear();
            if (gameLV.SelectedItem.ToString().Equals("{ gameName = DSZZ斗地主 }"))
            {
                roomLV.Items.Add(new RoomMessage("二缺一！快来一起战斗！", 2, "准备"));
                roomLV.Items.Add(new RoomMessage("战斗即刻开始！", 3, "开始"));
            }

        }
        //详细房间信息加载（ListView选中行改变时加载）
        private void RoomDetail(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            roomDetail.Text = null;
            if (roomLV.SelectedItem is RoomMessage RM && RM is RoomMessage)
            {
                string roomName = RM.RoomName;
                int roomNum = RM.RoomNum;
                string roomState = RM.RoomState;
                roomDetail.Text +=
                    "房间名称：" + roomName + "\r\n"
                    + "当前人数：" + roomNum + "\r\n"
                    + "当前房间状态：" + roomState + "\r\n";
            }
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
        public int RoomNum { set; get; }
        public string RoomState { set; get; }

        public RoomMessage(string Name, int Num, string State)
        {
            RoomName = Name;
            RoomNum = Num;
            RoomState = State;
        }
    }
}
