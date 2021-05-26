using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace WpfApp1
{
    /// <summary>
    /// Card.xaml 的交互逻辑
    /// </summary>
    public partial class Card : UserControl
    {
        public Card()
        {
            InitializeComponent();
        }

        // 用于判断该是否被用户选择了（鼠标点击了）。
        public bool IsSelected { get; set; }

        // 标记为是否已经是打出过的牌
        public bool IsOutPut { get; set; }

        // 用于判断牌是否可选
        private bool m_CanSelected = false;
        public bool CanSelected { get { return m_CanSelected; } }

        // 用于调用牌是否被选择或者没有选择的委托。
        /// <param name="isSelected">是否被选择</param>
        public delegate void CardSelectedDelegate(bool isSelected);
        public CardSelectedDelegate CardSelected { get; set; }

        // 设置纸牌是否可以选择
        /// <param name="canSelected">true为可以选择，false为不可以选择</param>
        public void SetCardSelected(bool canSelected)
        {
            m_CanSelected = canSelected;
            Storyboard onEnterStoryboard = (Storyboard)FindResource("OnPokerMouseEnter");
            Storyboard onLeaveStoryboard = (Storyboard)FindResource("OnPokerMouseLeave");
            //将动画设置为Leave时的状态，就相当于屏蔽了动画。
            OnPokerMouseEnter_BeginStoryboard.Storyboard = canSelected ? onEnterStoryboard : onLeaveStoryboard;
        }

        // 移动牌的方法
        /// <param name="offsetX">要移动的X偏移量</param>
        /// <param name="offsetY">要移动的Y偏移量</param>
        public void MoveCard(double offsetX, double offsetY)
        {
            //获取到前台动画对象
            Storyboard storyboard = FindResource("MoveCardStoryboard") as Storyboard;
            //X偏移量
            ((DoubleAnimation)storyboard.Children[0]).By = offsetX;
            //Y偏移量
            ((DoubleAnimation)storyboard.Children[1]).By = offsetY;
            storyboard.Begin(this, HandoffBehavior.Compose);//将新的动画追加到尾部。
        }

        // 选中一张牌
        public void SelectCard()
        {
            //确认当前没有被选中且牌可以被选中
            if (!IsSelected && CanSelected)
            {
                MoveCard(0, -20);
                IsSelected = true;
                CardSelected(true);
            }
        }

        // 取消选中一张牌
        public void UnSelectCard()
        {
            //确认当前已经被选中
            if (IsSelected)
            {
                MoveCard(0, 20);
                IsSelected = false;
                CardSelected(false);
            }
        }

        //选中则不选，不选则选中
        private void SelectBackground_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (CanSelected)
            {
                if (IsSelected)
                {
                    UnSelectCard();
                }
                else
                {
                    SelectCard();
                }
            }
        }
    }
}
