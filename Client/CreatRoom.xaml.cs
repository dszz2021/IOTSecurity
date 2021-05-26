using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Window3.xaml 的交互逻辑
    /// </summary>
    public partial class Window3 : Window
    {
        public Window3()
        {
            InitializeComponent();
        }

        //确认创建按钮
        private void Button_SetUp_Click_(object sender, RoutedEventArgs e)
        {
            //获取窗口键入的房间信息
            string gameName = game.Text;
            string roomName = room.Text;

            bool isSend;
            do
            {
                //次数
                isSend = Send_Server(gameName, roomName);
            } while (isSend);

            Receive_Server();
            //提示框：创建成功！

            //进入斗地主游戏窗口
            Window douDiZhu = new Window4();
            Hide();
            douDiZhu.ShowDialog();
            Close();
        }
        private bool Send_Server(string gameName,string roomName)
        {
            return false;
        }
        private void Receive_Server()
        {

        }

        //返回按钮 -> 游戏大厅
        private void Button_Return_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
