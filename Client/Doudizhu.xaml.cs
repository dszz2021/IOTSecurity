using System;
using System.Linq;
using System.Windows;
using WpfApp1.CardsClass;
using System.Collections;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media.Animation;

namespace WpfApp1
{
    public partial class Window4 : Window
    {
        public static Window4 DDZ = null;
        //游戏状态
        enum GameState { dealCard, grab, wait, redouble, playCard, settlement }
        //出牌类型
        enum CardsType { singleCard, doubleCards, threeCards, shunZi, lianDui, threeWithOne, threeWithTwo, planeOne, planeTwo, bomb, bombKing, outOfRule, noCard }

        //初始游戏状态为 wait
        static GameState gameState = GameState.wait;
        //初始出牌类型为 noCard
        static CardsType trueType = CardsType.noCard;
        static CardsType lastType = CardsType.noCard;
        //游戏轮次（三次为一轮）
        static int gameCycle = 0;
        //扑克比较数组
        static int[] arrayCompare;

        //扑克牌集合
        static List<CardBase> cardsList = new List<CardBase>();
        //左侧玩家扑克集合
        static List<CardBase> leftCards = new List<CardBase>();
        //中间玩家扑克集合
        static List<CardBase> midCards = new List<CardBase>();
        //右侧玩家扑克集合
        static List<CardBase> rightCards = new List<CardBase>();
        //底牌集合
        static List<CardBase> handCard = new List<CardBase>();
        //出牌集合
        static List<CardBase> outPutCard = new List<CardBase>();

        public Window4()
        {
            DDZ = this;
            //ddzMusic.Play();
            InitializeComponent();
        }

        //斗地主准备or取消准备
        private void Button_Ready_Click(object sender, RoutedEventArgs e)
        {
            if (_1_ready.Content.Equals("准备"))
            {
                _1_ready.Content = "取消准备";
                _1_return.IsEnabled = false;
                //向服务器发送 准备 信号

                gameState = GameState.dealCard;
                GetCardCollection();
                Cycle_Game_State(gameState);
            }
            else
            {
                _1_ready.Content = "准备";
                _1_return.IsEnabled = true;
                //向服务器发送 取消准备 信号

            }
        }

        //循环监听报文
        public void Receive_Server()
        {
            //监听服务器发送的报文

            //转换游戏状态
            gameState = GameState.dealCard;
            GetCardCollection();
            Cycle_Game_State(gameState);

            //抢地主
            gameState = GameState.grab;
            Cycle_Game_State(gameState);

            //地主确定 -> 展示底牌
            foreach (CardBase card in handCard)
            {
                card.SetCard();
            }
            Landlord_Cards();//地主发牌

            //加倍
            gameState = GameState.redouble;
            Cycle_Game_State(gameState);

            //出牌
            gameState = GameState.playCard;
            Cycle_Game_State(gameState);

            //结算
            gameState = GameState.settlement;
            Cycle_Game_State(gameState);
        }

        //游戏状态循环
        private void Cycle_Game_State(GameState state)
        {
            switch (state)
            {
                case GameState.dealCard:
                    //隐藏“准备”按钮
                    _1_ready.Visibility = Visibility.Hidden;
                    _1_return.Visibility = Visibility.Hidden;
                    //发牌动画
                    DealCards();
                    //扑克排序
                    Storyboard story = new Storyboard();
                    midCards = SortCards(midCards, DDZ.CanvasTable, story, 0.15);
                    story.Begin();
                    break;
                case GameState.grab:
                    //显示“地主”按钮
                    _2_grab.Visibility = Visibility.Visible;
                    _2_noGrab.Visibility = Visibility.Visible;
                    break;
                case GameState.wait:
                    break;
                case GameState.redouble:
                    //设置本家扑克可选,并将牌是否可以选择注册到委托
                    foreach (CardBase card in midCards)
                    {
                        card.Card.SetCardSelected(true);
                        card.Card.CardSelected = new Card.CardSelectedDelegate(CardSelectedEvent);
                    }
                    //显示“加倍”按钮
                    _3_double.Visibility = Visibility.Visible;
                    _3_noDouble.Visibility = Visibility.Visible;
                    break;
                case GameState.playCard:
                    //显示“出牌”按钮
                    _4_output.Visibility = Visibility.Visible;
                    _4_noOutput.Visibility = Visibility.Visible;
                    break;
                case GameState.settlement:
                    Settlement settlement = new Settlement();
                    settlement.ShowDialog();
                    Close();
                    break;
            }
        }
        //获取一副扑克牌
        public static void GetCardCollection()
        {
            for (int i = 0; i < 51; i++)
            {
                CardBase card = new CardBase();
                //获得本家的一幅扑克
                if (i % 3 == 1)
                {
                    card.CardColor = "Club";
                    Random random = new Random(i);
                    card.CardNumber = random.Next(3, 15);
                    midCards.Add(card);
                }
                else
                {
                    card.CardColor = "FaceDown";
                    card.CardNumber = 0;
                }
                card.SetCard();
                cardsList.Add(card);
            }
            //设置底牌
            for (int i = 0; i < 3; i++)
            {
                Random random = new Random(i);
                CardBase card = new CardBase
                {

                    CardColor = "Club",
                    CardNumber = random.Next(3, 15)
                };
                handCard.Add(card);
            }
        }
        //用于鼠标点击牌是否被选择
        public static void CardSelectedEvent(bool isSelected)
        {
            //存储选中扑克的点数
            ArrayList cardSelect = new ArrayList();
            foreach (CardBase card in midCards)
            {
                if (card.Card.IsSelected)
                {
                    cardSelect.Add(card.CardNumber);
                }
            }
            int[] cardInt = new int[cardSelect.Count];
            for (int i = 0; i < cardSelect.Count; i++)
            {
                cardInt[i] = (int)cardSelect[i];
            }
            //判断选中的扑克是否符合牌型
            CardsType cardsType = JudgeCardType(cardInt);
            if (trueType == CardsType.noCard)
            {
                if (cardsType != CardsType.outOfRule)
                    DDZ._4_output.IsEnabled = true;
                else
                    DDZ._4_output.IsEnabled = false;
            }
            else
            {
                if (CompareCard(cardsType, cardInt, arrayCompare))
                    DDZ._4_output.IsEnabled = true;
                else
                    DDZ._4_output.IsEnabled = false;
            }
        }
        //判断扑克牌型
        private static CardsType JudgeCardType(int[] cards)
        {
            //冒泡排序
            for (int i = 0; i < cards.Length - 1; i++)
            {
                for (int j = 0; j < cards.Length - i - 1; j++)
                {
                    if (cards[j] > cards[j + 1])
                    {
                        int temp = cards[j];
                        cards[j] = cards[j + 1];
                        cards[j + 1] = temp;
                    }
                }
            }
            //单牌
            if (cards.Length == 1)
                return CardsType.singleCard;
            else if (cards.Length == 2)
            {
                //对子
                if (cards[0] == cards[1])
                    return CardsType.doubleCards;
                //王炸
                else if ((cards[0] == 16 && cards[1] == 17) || (cards[0] == 17 && cards[1] == 16))
                    return CardsType.bombKing;
            }
            else if (cards.Length == 3)
            {
                if (cards[0] == cards[1] && cards[1] == cards[2])
                    return CardsType.threeCards;
            }
            else if (cards.Length == 4)
            {
                //炸弹
                if ((cards[0] == cards[1]) && (cards[1] == cards[2]) && (cards[2] == cards[3]))
                    return CardsType.bomb;
                //三带一
                else if ((cards[0] == cards[1]) && (cards[1] == cards[2]) || (cards[1] == cards[2]) && (cards[2] == cards[3]))
                    return CardsType.threeWithOne;
            }
            else if (cards.Length >= 5)
            {
                //三带二
                if (cards.Length == 5 && (((cards[0] == cards[1]) && (cards[2] == cards[3]) && (cards[3] == cards[4])) || ((cards[0] == cards[1]) && (cards[1] == cards[2]) && (cards[3] == cards[4]))))
                    return CardsType.threeWithTwo;
                //顺子
                bool isShunZi = false;
                for (int i = 0; i < cards.Length - 1; i++)
                {
                    if ((cards[i] + 1 == cards[i + 1]) && cards[i + 1] < 15)
                        isShunZi = true;
                    else
                    {
                        isShunZi = false;
                        break;
                    }
                }
                if (isShunZi)
                    return CardsType.shunZi;

                if (cards.Length % 2 == 0)
                {
                    //连对
                    bool isLianDui = false;
                    for (int i = 0; i < cards.Length - 2; i += 2)
                    {
                        if ((cards[i] == cards[i + 1]) && (cards[i] + 1 == cards[i + 2]) && cards[i + 2] < 15)
                            isLianDui = true;
                        else
                        {
                            isLianDui = false;
                            break;
                        }
                    }
                    if (isLianDui)
                        return CardsType.lianDui;

                    //飞机（三带一）
                    if (cards.Length % 4 == 0)
                    {
                        bool isPlane1 = false;
                        int numPlane1 = cards.Length / 4;
                        int trueNumPlane1 = 0;
                        for (int i = 0; i < cards.Length - 2; i++)
                        {
                            if ((cards[i] == cards[i + 1]) && (cards[i + 1] == cards[i + 2]) && cards[i] < 15)
                                trueNumPlane1++;
                            if (numPlane1 == trueNumPlane1)
                                isPlane1 = true;
                            else
                                isPlane1 = false;
                        }
                        if (isPlane1)
                            return CardsType.planeOne;
                    }
                }
                //飞机（三带二）
                if (cards.Length % 5 == 0)
                {
                    bool isPlane2 = false;

                    int numPlane2 = cards.Length / 5;
                    int trueNumPlane21 = 0;
                    int trueNumPlane22 = 0;
                    for (int i = 0; i < cards.Length - 2; i++)
                    {
                        if (cards[i] == cards[i + 1] && cards[i] != cards[i + 2])
                            trueNumPlane21++;
                        if ((cards[i] == cards[i + 1]) && (cards[i + 1] == cards[i + 2]) && cards[i] < 15)
                            trueNumPlane22++;
                        if (numPlane2 == trueNumPlane21 && numPlane2 == trueNumPlane22)
                            isPlane2 = true;
                        else
                            isPlane2 = false;
                    }
                    if (isPlane2)
                        return CardsType.planeTwo;
                }
            }
            return CardsType.outOfRule;
        }

        //判断能否出牌
        private static bool CompareCard(CardsType myType, int[] myCard, int[] otherCard)
        {
            if (myType == trueType)
            {
                if (myType == CardsType.bomb || myType == CardsType.singleCard || myType == CardsType.doubleCards || myType == CardsType.threeCards)
                {
                    if (myCard[0] > otherCard[0])
                        return true;
                }
                else if (myType == CardsType.shunZi || myType == CardsType.lianDui)
                {
                    if (myCard.Length == otherCard.Length && myCard[0] > otherCard[0])
                        return true;
                }
                else if (myType == CardsType.threeWithOne || myType == CardsType.threeWithTwo)
                {
                    int myNum = 0, otherNum = 0;
                    for (int i = 0; i < myCard.Length - 2; i++)
                    {
                        if (myCard[i] == myCard[i + 1] && myCard[i] == myCard[i + 2])
                            myNum = myCard[i];
                        if (otherCard[i] == otherCard[i + 1] && otherCard[i] == otherCard[i + 2])
                            otherNum = otherCard[i];
                    }
                    if (myNum > otherNum)
                        return true;
                }
                else if (myType == CardsType.planeOne || myType == CardsType.planeTwo)
                {
                    int myNum = 0, otherNum = 0;
                    if (myCard.Length == otherCard.Length)
                    {
                        for (int i = 0; i < myCard.Length - 2; i++)
                        {
                            if (myCard[i] == myCard[i + 1] && myCard[i] == myCard[i + 2])
                                myNum = myCard[i];
                            if (otherCard[i] == otherCard[i + 1] && otherCard[i] == otherCard[i + 2])
                                otherNum = otherCard[i];
                        }
                    }
                    if (myNum > otherNum)
                        return true;
                }
            }
            else if (myType == CardsType.bombKing || (myType == CardsType.bomb && trueType != CardsType.bomb))
                return true;
            return false;
        }

        //发牌状态
        private void DealCards()
        {
            //居中显示所有牌
            foreach (CardBase card in cardsList)
            {
                CanvasTable.Children.Insert(0, card.Card);
                Canvas.SetLeft(card.Card, 450);
                Canvas.SetTop(card.Card, 200);
            }
            //发牌动画
            for (int i = 0; i < 51; i++)
            {
                //左侧玩家
                if (i % 3 == 0)
                {
                    Point LeftLocation = new Point(-370, -150 + 10 * i);
                    CardAnimation animation = new CardAnimation(this, cardsList[i].Card);
                    animation.MoveCard(LeftLocation.X, LeftLocation.Y, TimeSpan.FromSeconds(0.05 * i));
                }
                //中间玩家
                else if (i % 3 == 1)
                {
                    Point MiddleLocation = new Point(-240 + 10 * i, 330);
                    CardAnimation animation = new CardAnimation(this, cardsList[i].Card);
                    animation.MoveCard(MiddleLocation.X, MiddleLocation.Y, TimeSpan.FromSeconds(0.05 * i));
                }
                //右侧玩家
                else if (i % 3 == 2)
                {
                    Point RightLocation = new Point(390, -150 + 10 * i);
                    CardAnimation animation = new CardAnimation(this, cardsList[i].Card);
                    animation.MoveCard(RightLocation.X, RightLocation.Y, TimeSpan.FromSeconds(0.05 * i));
                }
            }
            //居上显示底牌
            for (int i = 0; i < 3; i++)
            {
                if (i == 0)
                {
                    CanvasTable.Children.Insert(0, handCard[i].Card);
                    Canvas.SetLeft(handCard[i].Card, 350);
                    Canvas.SetTop(handCard[i].Card, 10);
                }
                else if (i == 1)
                {
                    CanvasTable.Children.Insert(0, handCard[i].Card);
                    Canvas.SetLeft(handCard[i].Card, 450);
                    Canvas.SetTop(handCard[i].Card, 10);
                }
                else
                {
                    CanvasTable.Children.Insert(0, handCard[i].Card);
                    Canvas.SetLeft(handCard[i].Card, 550);
                    Canvas.SetTop(handCard[i].Card, 10);
                }
            }

        }
        //扑克牌排序
        public static List<CardBase> SortCards(List<CardBase> cardList, Canvas canvasTable, Storyboard story, double sortSpeed)
        {
            //冒泡排序
            for (int i = 0; i < cardList.Count - 1; i++)
            {
                for (int j = 0; j < cardList.Count - i - 1; j++)
                {
                    if (cardList[j].CardNumber < cardList[j + 1].CardNumber)
                    {
                        CardBase temp = cardList[j];
                        cardList[j] = cardList[j + 1];
                        cardList[j + 1] = temp;
                    }
                }
            }
            List<CardBase> cards = new List<CardBase>();
            for (int i = 0; i < cardList.Count; i++)
            {
                Point point = new Point((-(cardList.Count - 1) / 2) * 25, 330);
                CardAnimation animation = new CardAnimation(cardList[i].Card);
                animation.MoveCard(point.X + 25 * i, point.Y, TimeSpan.FromSeconds(sortSpeed * i), story);
                //将其上下位置重新排列
                canvasTable.Children.Remove(cardList[i].Card);
                canvasTable.Children.Add(cardList[i].Card);
                cards.Add(cardList[cardList.Count - i - 1]);
            }
            return cards;
        }

        //出牌按钮点击事件
        private void Output_Click(object sender, RoutedEventArgs e)
        {
            //获取已选择的扑克
            var query = from c in midCards where c.Card.IsSelected == true orderby c.CardNumber select c;
            List<CardBase> selectCard = query.ToList();

            int[] cardArray = new int[selectCard.Count];
            for (int j = 0; j < selectCard.Count; j++)
            {
                cardArray[j] = selectCard[j].CardNumber;
            }
            //设置"真实"出牌类型
            trueType = JudgeCardType(cardArray);
            arrayCompare = cardArray;
            //出牌以及动画
            //List<CardBase> outPutCard = new List<CardBase>();
            foreach (int n in cardArray)
            {
                var q = from c in midCards where c.CardNumber == n && c.Card.IsSelected == true select c;
                outPutCard.Add(q.First());
                q.First().Card.IsOutPut = true;
                midCards.Remove(q.First());
            }

            //重新排序
            Storyboard story = new Storyboard();
            SortCards(midCards, CanvasTable, story, 0.01);
            story.Begin();

            //出牌动画
            Storyboard storyOutPut = new Storyboard();
            for (int i = 0; i < outPutCard.Count; i++)
            {
                CardAnimation animation = new CardAnimation(this, outPutCard[i].Card);
                CanvasTable.Children.Remove(outPutCard[i].Card);
                CanvasTable.Children.Add(outPutCard[i].Card);//换顺序
                outPutCard[i].SetCard();
                outPutCard[i].Card.SetCardSelected(false);
                Point point = new Point(-(outPutCard.Count / 2) * 20, 330 - outPutCard[i].Card.Height - 25);
                animation.MoveCard(point.X + i * 25, point.Y, TimeSpan.FromSeconds(i * 0.01), storyOutPut);
            }
            storyOutPut.Begin();
            //发送“出牌”报文

            _4_output.Visibility = Visibility.Hidden;
            _4_output.IsEnabled = false;
            _4_noOutput.Visibility = Visibility.Hidden;
        }
        private void NoOutput_Click(object sender, RoutedEventArgs e)
        {
            //发送“不出牌”报文
            RemoveAllOutPutCards();
            _4_output.Visibility = Visibility.Hidden;
            _4_noOutput.Visibility = Visibility.Hidden;
        }

        //清除桌上打出的所有的牌
        private void RemoveAllOutPutCards()
        {
            var q = from c in outPutCard where c.Card.IsOutPut == true select c;
            foreach (CardBase card in q.ToList())
            {
                if (CanvasTable.Children.Contains(card.Card))
                {
                    CanvasTable.Children.Remove(card.Card);
                    outPutCard.Remove(card);
                }
            }
        }

        #region 抢地主/加倍
        private void Grab_Click(object sender, RoutedEventArgs e)
        {
            //发送“抢”地主报文

            _2_grab.Visibility = Visibility.Hidden;
            _2_noGrab.Visibility = Visibility.Hidden;
        }
        private void NoGrab_Click(object sender, RoutedEventArgs e)
        {
            //发送“不抢”地主报文

            _2_grab.Visibility = Visibility.Hidden;
            _2_noGrab.Visibility = Visibility.Hidden;
        }

        //底牌 -> 地主
        private void Landlord_Cards()
        {
            Storyboard story = new Storyboard();
            for (int i = 0; i < 3; i++)
            {
                midCards.Add(handCard[i]);
                Canvas.SetLeft(handCard[i].Card, 450);
                Canvas.SetTop(handCard[i].Card, 200);
                /*                
                Point MiddleLocation = new Point(-240, 520);
                CardAnimation animation = new CardAnimation(this, handCard[i].Card);
                animation.MoveCard(MiddleLocation.X, MiddleLocation.Y, TimeSpan.FromSeconds(0), story);
                */
            }
            midCards = SortCards(midCards, DDZ.CanvasTable, story, 0.001);
        }

        private void Double_Click(object sender, RoutedEventArgs e)
        {
            //发送“加倍”报文

            _3_double.Visibility = Visibility.Hidden;
            _3_noDouble.Visibility = Visibility.Hidden;
        }
        private void Nodouble_Click(object sender, RoutedEventArgs e)
        {
            //发送“不加倍”报文

            _3_double.Visibility = Visibility.Hidden;
            _3_noDouble.Visibility = Visibility.Hidden;
        }
        #endregion

        #region 按钮事件
        //菜单按钮 -> 报文展示
        private void Menu_Message_Click(object sender, RoutedEventArgs e)
        {
            Window Show_Message = new Window5();
            Show_Message.Show();
        }
        //菜单按钮 -> 游戏设置
        private void Menu_Setting_Click(object sender, RoutedEventArgs e)
        {

        }
        //窗口关闭事件
        private void Test(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //父窗口关闭时，子窗口同时关闭
            foreach (Window window in App.Current.Windows)
            {
                if (window.Title.Equals("报文展示"))
                    window.Close();
            }
            //游戏扑克及状态清零

            //游戏状态为 wait
            gameState = GameState.wait;
            //出牌类型为 noCard
            trueType = CardsType.noCard;

            //扑克牌集合
            cardsList.Clear(); ;
            leftCards.Clear();
            midCards.Clear();
            rightCards.Clear();
            handCard.Clear();
            outPutCard.Clear();
        }
        //返回按钮 -> 游戏大厅
        private void Button_Return_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //抢地主
            gameState = GameState.grab;
            Cycle_Game_State(gameState);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //地主确定 -> 展示底牌
            foreach (CardBase card in handCard)
            {
                card.SetCard();
            }
            Landlord_Cards();//地主发牌

            //加倍
            gameState = GameState.redouble;
            Cycle_Game_State(gameState);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //出牌
            gameState = GameState.playCard;
            Cycle_Game_State(gameState);
        }
    }
    #endregion 按钮事件
}