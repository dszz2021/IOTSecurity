using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // 0-允许登录；1-账号不存在；2-密码错误
        enum IsEnoll { success = 0, fail1 = 1, fail2 = 2 }

        // 登录按钮
        private void Button_enroll_Click(object sender, RoutedEventArgs e)
        {
            //获取窗口键入的账号and密码
            string ID = account.Text;
            string PW = password.Password;

            //向服务器发送报文
            bool isSend;
            do
            {
                //可判断次数≥5次终止发送信息
                isSend = Send_Server(ID, PW);
            } while (isSend);

            //接受服务器的信息判断是否登录
            IsEnoll choice = Receive_Server();
            switch (choice)
            {
                // choice==0 -> 登陆成功，进入游戏大厅
                case IsEnoll.success:
                    Window gameHall = new Window2();
                    Hide();
                    gameHall.ShowDialog();
                    Close();
                    break;
                // choice==1 -> 提示框：账号不存在！
                case IsEnoll.fail1:
                    break;
                // choice==2 -> 提示框：密码错误！
                case IsEnoll.fail2:
                    break;
            }

        }

        /*        
        //分析报文
        private bool Analysis_Message(Message message) 
        {
            return true;
        }
        */
        //DES解密
        private string DES_Decrypt(string ciphertext)
        {
            return null;
        }

        //向服务器发送报文
        private bool Send_Server(string account, string password)
        {
            return false;
        }
        //接收服务器的报文
        private IsEnoll Receive_Server()
        {
            return 0;
        }
    }
}