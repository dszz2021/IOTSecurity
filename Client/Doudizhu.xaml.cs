using System;
using Message1;
using System.Linq;
using System.Windows;
using System.Threading;
using System.Net.Sockets;
using WpfApp1.CardsClass;
using System.Collections;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media.Animation;
using System.Text.RegularExpressions;
using Message1.BodyE;
using Newtonsoft.Json;
using System.Text;

namespace WpfApp1
{
    public partial class Window4 : Window
    {
        Socket socketClient = null;
        public static Window2 gameHall = null;
        public static Window4 DDZ = null;
        //游戏状态
        public enum GameState { ready, dealCard, grab, wait, redouble, playCard, settlement }
        //出牌类型
        public enum CardsType
        {
            singleCard, doubleCards, threeCards, shunZi, lianDui, threeWithOne, threeWithTwo, planeOne, planeTwo, bomb, bombKing, outOfRule, noCard
        }

        //初始游戏状态为 ready
        static GameState gameState = GameState.ready;
        //初始出牌类型为 noCard
        static CardsType trueType = CardsType.noCard;
        //扑克比较数组
        static int[] arrayCompare;
        //玩家ID集合
        string[] player = new string[3];
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
        //结算清单
        Dictionary<String, int> settleMent;
        //游戏ID
        readonly string gameID;
        //房间ID
        readonly string roomID;

        public Window4()
        {
            DDZ = this;
            InitializeComponent();
        }
        public Window4(Window2 w, string myID, Socket socket)
        {
            gameHall = w;
            gameID = w.GetGameID();
            roomID = w.GetRoomID();
            DDZ = this;
            socketClient = socket;
            player[1] = myID;
            InitializeComponent();
        }
        //发送报文 -> 服务器
        void ClientSendMsg(string sendMsg)
        {
            //将输入的内容字符串转换为机器可以识别的字节数组     
            byte[] arrClientSendMsg = Encoding.UTF8.GetBytes(sendMsg + "\n");
            //调用客户端套接字发送字节数组     
            socketClient.Send(arrClientSendMsg);
        }
        //设置当前倍数
        public void SetMultiple(int mul)
        {
            multiple.Content = mul;
        }
        //设置上家出牌类型
        internal void SetLastType(CardType cardType)
        {
            trueType = (CardsType)cardType;
            throw new NotImplementedException();
        }
        //设置玩家显示信息
        public void SetOutMessage(string id, bool ready, string one, string two)
        {
            int location = GetPlayer(id);
            if (location != -1)
            {
                if (ready)
                {
                    //显示出玩家准备
                    if (location == 0)
                    {
                        left.Visibility = Visibility.Visible;
                        left.Content = one;
                    }
                    else if (location == 1)
                    {
                        middle.Visibility = Visibility.Visible;
                        middle.Content = one;
                    }
                    else
                    {
                        right.Visibility = Visibility.Visible;
                        right.Content = one;
                    }
                }
                else
                {
                    //显示出玩家未准备
                    if (location == 0)
                    {
                        left.Visibility = Visibility.Visible;
                        left.Content = two;
                    }
                    else if (location == 1)
                    {
                        middle.Visibility = Visibility.Visible;
                        middle.Content = two;
                    }
                    else
                    {
                        right.Visibility = Visibility.Visible;
                        right.Content = two;
                    }
                }
            }
        }
        //设置出牌信息
        public void SetOutPutCards(ArrayList arrayList, string id)
        {
            //清除桌面上已打出的扑克
            RemoveAllOutPutCards();
            //打出领桌的扑克
            int num = GetPlayer(id);
            if (num == 0)
                RenewCards(arrayList, leftCards);
            else if (num == 2)
                RenewCards(arrayList, rightCards);
            //出牌动画
            OutputAnimation();
        }
        //更新出牌集合
        public void RenewCards(ArrayList arrayList, List<CardBase> sideCards)
        {
            for (int i = 0; i < arrayList.Count; i++)
            {
                CardBase card = new CardBase();
                //将ArrayList转换为string便于切分
                string str = arrayList[i].ToString();
                //将string切分为花色和点数
                string[] str1 = Regex.Split(str, "_0x_", RegexOptions.IgnoreCase);
                string[] str2 = Regex.Split(str1[0], "0x", RegexOptions.IgnoreCase);
                //设置扑克点数
                card.CardNumber = Convert.ToInt32(str2[0], 16) + 2;
                //设置扑克花色
                if (card.CardNumber > 15)
                    card.CardColor = "Joker";
                else
                {
                    if (str1[1].Equals(1))
                        card.CardColor = "Club";
                    else if (str1[1].Equals(2))
                        card.CardColor = "Diamond";
                    else if (str1[1].Equals(3))
                        card.CardColor = "Spader";
                    else if (str[1].Equals(4))
                        card.CardColor = "Heart";
                }
                outPutCard.Add(card);
                sideCards.RemoveAt(i);
            }
        }
        //设置结算清单
        public void SetSettleMent(Dictionary<String, int> dic)
        {
            settleMent = dic;
        }
        //获取玩家ID
        public int GetPlayer(string id)
        {
            for (int i = 0; i < player.Length; i++)
            {
                if (player[i].Equals(id))
                    return i;
            }
            return -1;
        }
        //底牌 -> 地主
        public void Landlord_Cards(string id)
        {
            //获取地主位置
            int location = GetPlayer(id);
            //展示底牌
            foreach (CardBase card in handCard)
                card.SetCard();
            //睡眠1000ms
            Thread.Sleep(2000);
            //底牌动画
            Storyboard story = new Storyboard();
            for (int i = 0; i < 3; i++)
            {
                Canvas.SetLeft(handCard[i].Card, 450);
                Canvas.SetTop(handCard[i].Card, 200);
            }
            //分发底牌
            if (location == 0)
            {
                for (int i = 51; i < 54; i++)
                {
                    leftCards.Add(handCard[i - 51]);
                    Point LeftLocation = new Point(-370, -150 + 7 * i);
                    CardAnimation animation = new CardAnimation(this, handCard[i - 51].Card);
                    animation.MoveCard(LeftLocation.X, LeftLocation.Y, TimeSpan.FromSeconds(0.05 * i));
                    leftPlayer.Visibility = Visibility.Visible;
                }
            }
            else if (location == 1)
            {
                midCards.Add(handCard[1]);
                midCards.Add(handCard[2]);
                midCards.Add(handCard[3]);
                midPlayer.Visibility = Visibility.Visible;
                midCards = SortCards(midCards, DDZ.CanvasTable, story, 0.1);
            }
            else if (location == 2)
            {
                for (int i = 51; i < 54; i++)
                {
                    rightCards.Add(handCard[i - 51]);
                    Point RightLocation = new Point(390, -150 + 7 * i);
                    CardAnimation animation = new CardAnimation(this, handCard[i - 51].Card);
                    animation.MoveCard(RightLocation.X, RightLocation.Y, TimeSpan.FromSeconds(0.05 * i));
                    rightPlayer.Visibility = Visibility.Visible;
                }
            }
        }

        //准备or取消准备
        private void Button_Ready_Click(object sender, RoutedEventArgs e)
        {
            if (_1_ready.Content.Equals("准备"))
            {
                _1_ready.Content = "取消准备";
                _1_return.IsEnabled = false;
                //封装报文
                BodyE1 bodyE1 = new BodyE1(gameID, roomID, player[1], true);
                string bodyjson = JsonConvert.SerializeObject(bodyE1);
                Message message = new Message(0xD, 0x7, bodyjson);
                string messagJson = JsonConvert.SerializeObject(message);
                //向服务器发送报文
                ClientSendMsg(messagJson);
            }
            else
            {
                _1_ready.Content = "准备";
                _1_return.IsEnabled = true;
                //封装报文
                BodyE1 bodyE1 = new BodyE1(gameID, roomID, player[1], false);
                string bodyjson = JsonConvert.SerializeObject(bodyE1);
                Message message = new Message(0xD, 0x7, bodyjson);
                string messagJson = JsonConvert.SerializeObject(message);
                //向服务器发送报文
                ClientSendMsg(messagJson);
            }
        }
        //游戏状态循环
        public void Cycle_Game_State(GameState state)
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
                    Settlement settlement = new Settlement(settleMent);
                    settlement.ShowDialog();
                    Close();
                    break;
            }
        }
        //获取一副扑克牌
        public void GetCardCollection(ArrayList arrayList)
        {
            for (int i = 0; i < arrayList.Count; i++)
            {
                CardBase card = new CardBase();
                //将ArrayList转换为string便于切分
                string str = arrayList[i].ToString();
                //将string切分为花色和点数
                string[] str1 = Regex.Split(str, "_0x_", RegexOptions.IgnoreCase);
                string[] str2 = Regex.Split(str1[0], "0x", RegexOptions.IgnoreCase);
                //设置扑克点数
                card.CardNumber = Convert.ToInt32(str2[0], 16) + 2;
                //设置扑克花色
                if (card.CardNumber > 15)
                    card.CardColor = "Joker";
                else
                {
                    if (str1[1].Equals(1))
                        card.CardColor = "Club";
                    else if (str1[1].Equals(2))
                        card.CardColor = "Diamond";
                    else if (str1[1].Equals(3))
                        card.CardColor = "Spader";
                    else if (str[1].Equals(4))
                        card.CardColor = "Heart";
                }
                //本家扑克
                if (i < 17)
                {
                    midCards.Add(card);
                    card.SetCard();
                    cardsList.Add(card);
                }
                //设置底牌
                else
                    handCard.Add(card);
            }
            //设置左右两家的扑克
            for (int i = 0; i < 17; i++)
            {
                CardBase card = new CardBase
                {
                    CardColor = "FaceDown",
                    CardNumber = 0
                };
                leftCards.Add(card);
                cardsList.Add(card);
                rightCards.Add(card);
                cardsList.Add(card);
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
                        int num = 0;
                        bool isPlane1 = false;
                        int[] numPlane1 = new int[cards.Length / 4];
                        for (int i = 0; i < cards.Length - 2; i++)
                        {
                            //判断是否为 threeCards
                            if ((cards[i] == cards[i + 1]) && (cards[i + 1] == cards[i + 2]) && cards[i] < 15)
                            {
                                numPlane1[num] = cards[i];
                                num++;
                            }
                            //判断是否为 plane
                            for (int j = 0; i < numPlane1.Length - 1; i++)
                            {
                                if (numPlane1[j] + 1 == numPlane1[j + 1])
                                    isPlane1 = true;
                                else
                                    isPlane1 = false;
                            }
                        }
                        if (isPlane1)
                            return CardsType.planeOne;
                    }
                }
                //飞机（三带二）
                if (cards.Length % 5 == 0)
                {
                    bool isPlane2 = false;
                    int[] numPlaneArr = new int[cards.Length / 5];
                    int trueNumPlane3 = 0;
                    int trueNumPlane2 = 0;
                    for (int i = 1; i < cards.Length - 2; i++)
                    {
                        //判断是否为 threeCards
                        if (cards[i - 1] == cards[i + 1] && cards[i + 1] < 15)
                        {
                            numPlaneArr[trueNumPlane3] = cards[i];
                            trueNumPlane3++;
                        }
                        //判断是否为 doubleCards
                        if ((cards[i] == cards[i + 1] && cards[i] != cards[i - 1]) || (cards[i - 1] == cards[i] && cards[i] != cards[i + 1]))
                            trueNumPlane2++;
                    }
                    //判断是否为 plane
                    if (trueNumPlane3 == trueNumPlane2 && trueNumPlane3 == cards.Length / 5)
                    {
                        for (int i = 0; i < numPlaneArr.Length - 1; i++)
                        {
                            if (numPlaneArr[i] + 1 == numPlaneArr[i + 1])
                                isPlane2 = true;
                            else
                                isPlane2 = false;
                        }
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
        //发牌
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
                    Point LeftLocation = new Point(-370, -150 + 7 * i);
                    CardAnimation animation = new CardAnimation(this, leftCards[i / 3].Card);
                    animation.MoveCard(LeftLocation.X, LeftLocation.Y, TimeSpan.FromSeconds(0.05 * i));
                }
                //中间玩家
                else if (i % 3 == 1)
                {
                    Point MiddleLocation = new Point(-240 + 10 * i, 330);
                    CardAnimation animation = new CardAnimation(this, midCards[i / 3].Card);
                    animation.MoveCard(MiddleLocation.X, MiddleLocation.Y, TimeSpan.FromSeconds(0.05 * i));
                }
                //右侧玩家
                else if (i % 3 == 2)
                {
                    Point RightLocation = new Point(390, -150 + 7 * i);
                    CardAnimation animation = new CardAnimation(this, rightCards[i / 3].Card);
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
        //出牌
        private void Output_Click(object sender, RoutedEventArgs e)
        {
            //获取已选择的扑克
            var query = from c in midCards where c.Card.IsSelected == true orderby c.CardNumber select c;
            List<CardBase> selectCard = query.ToList();
            //获取发送报文ArrayList
            ArrayList arrayList = new ArrayList(selectCard.Count);
            for (int i = 0; i < selectCard.Count; i++)
                arrayList[i] = selectCard[i];
            //获取数字集合
            int[] cardArray = new int[selectCard.Count];
            for (int j = 0; j < selectCard.Count; j++)
                cardArray[j] = selectCard[j].CardNumber;
            //设置"真实"出牌类型
            trueType = JudgeCardType(cardArray);
            arrayCompare = cardArray;
            //出牌以及动画
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
            OutputAnimation();
            //封装报文
            BodyE4 bodyE4 = new BodyE4.BodyE4Builder().
                setgameID(gameID).
                setroomID(roomID).
                setclientID(player[1]).
                setcardInformation(arrayList).
                setcardType((CardType)trueType).build();
            string bodyjson = JsonConvert.SerializeObject(bodyE4);
            Message message = new Message(0xD, 0x4, bodyjson);
            string messagJson = JsonConvert.SerializeObject(message);
            //向服务器发送报文
            ClientSendMsg(messagJson);
            //出牌按钮隐藏
            _4_output.Visibility = Visibility.Hidden;
            _4_output.IsEnabled = false;
            _4_noOutput.Visibility = Visibility.Hidden;
        }
        private void NoOutput_Click(object sender, RoutedEventArgs e)
        {
            ArrayList arrayList = null;
            //封装报文
            BodyE4 bodyE4 = new BodyE4.BodyE4Builder().
                setgameID(gameID).
                setroomID(roomID).
                setclientID(player[1]).
                setcardInformation(arrayList).
                setcardType((CardType)trueType).build();
            string bodyjson = JsonConvert.SerializeObject(bodyE4);
            Message message = new Message(0xD, 0x4, bodyjson);
            string messagJson = JsonConvert.SerializeObject(message);
            //向服务器发送报文
            ClientSendMsg(messagJson);
            //出牌按钮隐藏
            _4_output.Visibility = Visibility.Hidden;
            _4_noOutput.Visibility = Visibility.Hidden;
        }
        //出牌动画
        public void OutputAnimation()
        {
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
        }
        //清除桌上打出的牌
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

        //抢地主
        private void Grab_Click(object sender, RoutedEventArgs e)
        {
            //封装报文
            BodyE2 bodyE2 = new BodyE2(gameID, roomID, player[1], true);
            string bodyjson = JsonConvert.SerializeObject(bodyE2);
            Message message = new Message(0xD, 0x7, bodyjson);
            string messagJson = JsonConvert.SerializeObject(message);
            //向服务器发送报文
            ClientSendMsg(messagJson);
            _2_grab.Visibility = Visibility.Hidden;
            _2_noGrab.Visibility = Visibility.Hidden;
        }
        private void NoGrab_Click(object sender, RoutedEventArgs e)
        {
            //封装报文
            BodyE2 bodyE2 = new BodyE2(gameID, roomID, player[1], false);
            string bodyjson = JsonConvert.SerializeObject(bodyE2);
            Message message = new Message(0xD, 0x7, bodyjson);
            string messagJson = JsonConvert.SerializeObject(message);
            //向服务器发送报文
            ClientSendMsg(messagJson);

            _2_grab.Visibility = Visibility.Hidden;
            _2_noGrab.Visibility = Visibility.Hidden;
        }
        //加倍
        private void Double_Click(object sender, RoutedEventArgs e)
        {
            //封装报文
            BodyE3 bodyE3 = new BodyE3(gameID, roomID, player[1], true);
            string bodyjson = JsonConvert.SerializeObject(bodyE3);
            Message message = new Message(0xD, 0x7, bodyjson);
            string messagJson = JsonConvert.SerializeObject(message);
            //向服务器发送报文
            ClientSendMsg(messagJson);
            _3_double.Visibility = Visibility.Hidden;
            _3_noDouble.Visibility = Visibility.Hidden;
        }
        private void Nodouble_Click(object sender, RoutedEventArgs e)
        {
            //封装报文
            BodyE3 bodyE3 = new BodyE3(gameID, roomID, player[1], true);
            string bodyjson = JsonConvert.SerializeObject(bodyE3);
            Message message = new Message(0xD, 0x7, bodyjson);
            string messagJson = JsonConvert.SerializeObject(message);
            //向服务器发送报文
            ClientSendMsg(messagJson);
            _3_double.Visibility = Visibility.Hidden;
            _3_noDouble.Visibility = Visibility.Hidden;
        }

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
        //返回按钮 -> 游戏大厅
        private void Button_Return_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
        //玩家意外退出
        public void UnexpectedExit(string id)
        {
            MessageBox.Show("玩家" + id + "意外退出，游戏结束！");
            Thread.Sleep(1000);
            Close();
        }
    }
}