using System;
using System.Collections.Generic;
using System.Windows;

namespace WpfApp1
{
    public partial class Settlement : Window
    {
        Dictionary<String, int> idAndScore;
        public Settlement()
        {
            InitializeComponent();
        }
        public Settlement(Dictionary<String, int> IDAndScore)
        {
            idAndScore = IDAndScore;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Score_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (KeyValuePair<string, int> kv in idAndScore)
                scoreLV.Items.Add(new GameMessage(kv.Key, kv.Value.ToString()));
        }
    }
}
