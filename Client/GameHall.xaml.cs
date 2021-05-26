using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Window2.xaml 的交互逻辑
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
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
            if(modify.Content.Equals("修改"))
            {
                modify.Content = "确定";
                nickname.IsEnabled = true;
            }
            else
            {
                modify.Content = "修改";
                nickname.IsEnabled = false;
            }
        }

        //发送信息按钮
        private void Button_Send_Click(object sender, RoutedEventArgs e)
        {
            //获取发送框中的内容
            string str = "";
            //将个人发言发送到服务器
            Send_Server(str);
        }
        //发送个人发言信息到服务器
        private bool Send_Server(string str)
        {
            return true;
        }

        //接收服务器发来的信息
        private void Receive_Server()
        {
            //如果 接收到公共频道信息
            string str = "";
            Public_Channel(str);
            // 接收到其它信息...

        }

        /*
         * 功能：公共频道信息刷新
         * 参数：str-当前新加的聊天信息
         */
        private void Public_Channel(string str)
        {
            //公共频道当前信息 += str;
        }

        //窗体加载事件（窗口生成时加载）
        private void GameHallLoad(object sender, RoutedEventArgs e)
        {
            //连接数据库
            /*
             * 加载个人ID和昵称:TextBox
             * 加载游戏列表:ListView
             * 加载大厅在线用户:ListView
             */
        }
        //房间信息加载（ListView选中行改变时加载）
        private void RoomLoad(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //连接数据库
            //获取选中行的文本Text（游戏ID）
            //数据库中查找
            //加载房间ListView

        }
        //详细房间信息加载（ListView选中行改变时加载）
        private void RoomDetail(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //连接数据库
            //获取选中行的文本Text（房间ID）
            //数据库中查找
            //加载详细信息ListView
        }
    }
}
