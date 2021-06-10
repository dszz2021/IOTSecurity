using System.Windows;

namespace WpfApp1
{
    public partial class Window5 : Window
    {
        public Window5()
        {
            InitializeComponent();
        }
        //发送报文—密文
        public void SetSendCipher(string str)
        {
            send_cipher.Text = str;
        }
        //发送报文—明文
        public void SetSendPlain(string str)
        {
            send_plain.Text = str;
        }
        //接受报文—密文
        public void SetReceiveCipher(string str)
        {
            receive_cipher.Text = str;
        }
        //接受报文—明文
        public void SetReceivePlain(string str)
        {
            receive_plain.Text = str;
        }
    }
}