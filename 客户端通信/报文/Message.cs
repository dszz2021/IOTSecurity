using System;
using System.Collections;
using System.Collections.Generic;


namespace Message
{

    public enum CardType
    {
        singleCard, doubleCards, shunZi, lianDui, threeWithOne, threeWithTwo, planeOne, planeTwo, bomb, bombKing, outOfRule, noCard
    }

    public class Message
    {
        MessageHead head;
        string body;        //利用Json序列化之后的报文信息
        string sign;

        public Message(int thickType, int thinType, string body)
        {
            this.head = new MessageHead(thickType, thinType, body.Length);
            this.body = body;
            this.sign = getSign(body);
        }

        /**
         *              通过序列化的body字符串利用RSA私钥进行加密生成数字签名。
         * @param body
         * @return
         */
        private string getSign(string body)
        {
            /*
               这里调用RSA加密生成签名
             */
            return "";
        }

        public MessageHead getHead()
        {
            return head;
        }

        public string getBody()
        {
            return body;
        }

        public string getSign()
        {
            return sign;
        }
    }

    public class MessageHead
    {
        int thickType;//报文粗粒度分类，（A-E五类报文类和1-8八类错误码）（16进制）
        int thinType;//报文细致的分类
        int length;//报文MessageBody通过Json序列化之后的字符串长度
        public MessageHead(int thickType, int thinType, int length)
        {
            this.length = length;
            this.thickType = thickType;
            this.thinType = thinType;
        }

        public int getLength()
        {
            return length;
        }

        public int getThickType()
        {
            return thickType;
        }

        public int getThinType()
        {
            return thinType;
        }
    }

    public class MessageBody
    {
        int type;
        public MessageBody(int type)
        {
            this.type = type;
        }

        public int getType()
        {
            return type;
        }
    }
}

namespace Message.BodyA
{
    public class BodyA1 : MessageBody
    {
        string IDc;
        string IDtgs;
        string TS1;
        public BodyA1(String IDc, String IDtgs, String TS1) : base(0xa1)
        {
            this.IDc = IDc;
            this.IDtgs = IDtgs;
            this.TS1 = TS1;
        }

        public String getIDtgs()
        {
            return IDtgs;
        }

        public String getIDc()
        {
            return IDc;
        }

        public String getTS1()
        {
            return TS1;
        }
    }

    public class BodyA2 : MessageBody
    {
        string KeyCandTgs;
        string IDtgs;
        string TS2;
        string Lifetime2;
        string TicketTgs;// TicketTGS通过json序列化并加密后的字符串

        public class BodyA2Builder
        {
            public string KeyCandTgs;
            public string IDtgs;
            public string TS2;
            public string Lifetime2;
            public string TicketTgs;

            public BodyA2Builder setKeyCandTgs(string value)
            {
                this.KeyCandTgs = value;
                return this;
            }
            public BodyA2Builder setIDtgs(string value)
            {
                this.IDtgs = value;
                return this;
            }
            public BodyA2Builder setTS2(string value)
            {
                this.TS2 = value;
                return this;
            }
            public BodyA2Builder setLifetime2(string value)
            {
                this.Lifetime2 = value;
                return this;
            }
            public BodyA2Builder setTicketTgs(string value)
            {
                this.TicketTgs = value;
                return this;
            }

            public BodyA2 build()
            {
                return new BodyA2(this);
            }

        }
        private BodyA2(BodyA2Builder builder) : base(0xa2)
        {
            this.IDtgs = builder.IDtgs;
            this.Lifetime2 = builder.Lifetime2;
            this.TS2 = builder.TS2;
            this.KeyCandTgs = builder.KeyCandTgs;
            this.TicketTgs = builder.TicketTgs;
        }

        public string getIDtgs()
        {
            return IDtgs;
        }

        public string getTS2()
        {
            return TS2;
        }

        public string getLifetime2()
        {
            return Lifetime2;
        }

        public string getKeyCandTgs()
        {
            return KeyCandTgs;
        }

        public string getTicketTgs()
        {
            return TicketTgs;
        }
    }

    public class TicketTGS
    {
        private string KeyCAndTgs;
        private string IDc;
        private string ADc;
        private string IDtgs;
        private string TS2;
        private string Lifetime2;

        public class TicketTGSBuilder
        {
            public string KeyCAndTgs;
            public string IDc;
            public string ADc;
            public string IDtgs;
            public string TS2;
            public string Lifetime2;
            public TicketTGSBuilder setKeyCAndTgs(string val)
            {
                this.KeyCAndTgs = val;
                return this;
            }
            public TicketTGSBuilder setIDc(string val)
            {
                this.IDc = val;
                return this;
            }
            public TicketTGSBuilder setADc(string val)
            {
                this.ADc = val;
                return this;
            }
            public TicketTGSBuilder setIDtgs(string val)
            {
                this.IDtgs = val;
                return this;
            }
            public TicketTGSBuilder setTS2(string val)
            {
                this.TS2 = val;
                return this;
            }
            public TicketTGSBuilder setLifetime2(string val)
            {
                this.Lifetime2 = val;
                return this;
            }
            public TicketTGS build()
            {
                return new TicketTGS(this);
            }
        }

        private TicketTGS(TicketTGSBuilder builder)
        {
            this.ADc = builder.ADc;
            this.IDc = builder.IDc;
            this.KeyCAndTgs = builder.KeyCAndTgs;
            this.TS2 = builder.TS2;
            this.Lifetime2 = builder.Lifetime2;
            this.IDtgs = builder.IDtgs;
        }

        public string getADc()
        {
            return ADc;
        }

        public string getIDc()
        {
            return IDc;
        }

        public string getIDtgs()
        {
            return IDtgs;
        }

        public string getKeyCAndTgs()
        {
            return KeyCAndTgs;
        }

        public string getLifetime2()
        {
            return Lifetime2;
        }

        public string getTS2()
        {
            return TS2;
        }

        public void test()
        {
            //想得到这个类时，可以通过如下调用方法,example:
            TicketTGS ticketTGS = new TicketTGSBuilder().
                    setKeyCAndTgs("相应内容").
                    setIDc("相应内容").
                    setADc("相应内容").
                    setIDtgs("相应内容").
                    setTS2("相应内容").
                    setLifetime2("相应内容").build();
            Console.WriteLine(ticketTGS.getIDc());
        }
    }

    public class TicketV
    {
        string KeyCAndV;
        string IDc;
        string IDv;
        string ADc;
        string TS4;
        string Lifetime4;

        public class TicketVBuilder
        {
            public string KeyCAndV;
            public string IDc;
            public string IDv;
            public string ADc;
            public string TS4;
            public string Lifetime4;
            public TicketVBuilder setKeyCAndV(string val)
            {
                this.KeyCAndV = val;
                return this;
            }
            public TicketVBuilder setIDc(string val)
            {
                this.IDc = val;
                return this;
            }
            public TicketVBuilder setIDv(string val)
            {
                this.IDv = val;
                return this;
            }
            public TicketVBuilder setTS4(string val)
            {
                this.TS4 = val;
                return this;
            }
            public TicketVBuilder setLifetime4(string val)
            {
                this.Lifetime4 = val;
                return this;
            }
            public TicketVBuilder setadc(string val)
            {
                this.ADc = val;
                return this;
            }
            public TicketV build()
            {
                return new TicketV(this);
            }
        }

        private TicketV(TicketVBuilder builder)
        {
            this.IDc = builder.IDc;
            this.KeyCAndV = builder.KeyCAndV;
            this.TS4 = builder.TS4;
            this.Lifetime4 = builder.Lifetime4;
            this.IDv = builder.IDv;
            this.ADc = builder.ADc;
        }

        public string getIDc()
        {
            return IDc;
        }

        public string getTS4()
        {
            return TS4;
        }

        public string getKeyCAndV()
        {
            return KeyCAndV;
        }

        public string getIDv()
        {
            return IDv;
        }

        public string getADc()
        {
            return ADc;
        }

        public string getLifetime4()
        {
            return Lifetime4;
        }
    }
}

namespace Message.BodyB
{
    public class BodyB1 : MessageBody
    {
        string IDv;
        string TicketTgs;//序列化并且加密之后的ticket
        string Authenticator; //序列化并且加密之后的Authenticator

        public BodyB1(string IDv, string TicketTgs, string Authenticator) : base(0xb1)
        {
            this.IDv = IDv;
            this.TicketTgs = TicketTgs;
            this.Authenticator = Authenticator;
        }
        public string getTicketTgs()
        {
            return TicketTgs;
        }

        public string getAuthenticator()
        {
            return Authenticator;
        }

        public string getIDv()
        {
            return IDv;
        }
    }

    public class BodyB2 : MessageBody
    {
        string KeyCAndV;
        string IDv;
        string TS4;
        string TicketV;// 序列化并且加密后的字符串

        public BodyB2(string keyCAndV, string IDv, string TS4, string ticketV) : base(0xb2)
        {
            this.KeyCAndV = keyCAndV;
            this.IDv = IDv;
            this.TS4 = TS4;
            this.TicketV = ticketV;
        }

        public string getIDv()
        {
            return IDv;
        }

        public string getKeyCAndV()
        {
            return KeyCAndV;
        }

        public string getTicketV()
        {
            return TicketV;
        }

        public string getTS4()
        {
            return TS4;
        }

    }

    public class Authenticator
    {
        string IDc;
        string ADc;
        string TS;
        public Authenticator(string IDc, string ADc, string TS)
        {
            this.ADc = ADc;
            this.IDc = IDc;
            this.TS = TS;
        }

        public string getIDc()
        {
            return IDc;
        }

        public string getADc()
        {
            return ADc;
        }

        public string getTS3()
        {
            return TS;
        }
    }
}

namespace Message.BodyC
{
    public class BodyC1 : MessageBody
    {
        string TicketV;
        string Authenticator;
        public BodyC1(string TicketV, string Authenticator) : base(0xC1)
        {
            this.TicketV = TicketV;
            this.Authenticator = Authenticator;
        }

        public string getTicketV()
        {
            return TicketV;
        }

        public string getAuthenticator()
        {
            return Authenticator;
        }
    }

    public class BodyC2 : MessageBody
    {
        string TS;
        public BodyC2(string TS5plus1) : base(0xC1)
        {
            this.TS = TS5plus1;
        }

        public string getTS()
        {
            return TS;
        }
    }
}

namespace Message.BodyD
{
    public class BodyD1 : MessageBody
    {
        string IDc;
        public BodyD1(string IDc) : base(0xd1)
        {

            this.IDc = IDc;
        }

        public String getIDc()
        {
            return IDc;
        }
    }

    public class BodyD2 : MessageBody
    {
        string IDc;
        string cName;//修改后的名字
        public BodyD2(string IDc, string name) : base(0xd2)
        {
            this.IDc = IDc;
            this.cName = name;
        }

        public string getIDc()
        {
            return IDc;
        }

        public string getName()
        {
            return cName;
        }
    }

    public class BodyD3 : MessageBody
    {
        string IDgame;
        public BodyD3(string IDgame) : base(0xd3)
        {

            this.IDgame = IDgame;
        }

        public string getIDgame()
        {
            return IDgame;
        }
    }

    public class BodyD4 : MessageBody
    {
        string IDgame;
        string IDc;
        string text;
        public BodyD4(string IDgame, string IDc, string text) : base(0xd4)
        {

            this.IDgame = IDgame;
            this.IDc = IDc;
            this.text = text;
        }

        public string getIDgame()
        {
            return IDgame;
        }

        public string getIDc()
        {
            return IDc;
        }

        public string getText()
        {
            return text;
        }

    }

    public class BodyD5 : MessageBody
    {
        string IDgame;
        string IDroom;
        string IDc;
        public BodyD5(string IDgame, string IDroom, string IDc) : base(0xd5)
        {

            this.IDgame = IDgame;
            this.IDroom = IDroom;
            this.IDc = IDc;
        }

        public string getIDc()
        {
            return IDc;
        }

        public string getIDgame()
        {
            return IDgame;
        }

        public string getIDroom()
        {
            return IDroom;
        }
    }

    public class BodyD6 : MessageBody
    {
        string IDc;
        string text;
        public BodyD6(string IDc, string text) : base(0xd6)
        {

            this.IDc = IDc;
            this.text = text;
        }

        public string getIDc()
        {
            return IDc;
        }

        public string getText()
        {
            return text;
        }
    }

    public class BodyD7 : MessageBody
    {
        string IDgame;
        string IDroom;
        public BodyD7(string idGame, string idRoom) : base(0xd7)
        {

            this.IDgame = idGame;
            this.IDroom = idRoom;
        }

        public string getIDroom()
        {
            return IDroom;
        }

        public string getIDgame()
        {
            return IDgame;
        }
    }

    public class BodyD8 : MessageBody
    {
        bool loadSuccessful;
        string cName;
        string score;
        public BodyD8(bool loadSuccessful, string name, string score) : base(0xd8)
        {
            this.loadSuccessful = loadSuccessful;
            this.cName = name;
            this.score = score;
        }

        public string getName()
        {
            return cName;
        }

        public string getScore()
        {
            return score;
        }
        public bool isLoadSuccessful()
        {
            return loadSuccessful;
        }
    }

    public class BodyD9 : MessageBody
    {
        bool changeSuccessful;
        string reason;
        public BodyD9(bool changeSuccessful) : base(0xd9)
        {
            this.changeSuccessful = changeSuccessful;
            reason = "successful";
        }

        public void setReason(string reason)
        {
            this.reason = reason;
        }

        public bool isChangeSuccessful()
        {
            return changeSuccessful;
        }
    }

    public class BodyD10 : MessageBody
    {
        ArrayList nameList;
        public BodyD10(ArrayList nameList) : base(0xd10)
        {
            this.nameList = nameList;
        }

        public ArrayList getNameList()
        {
            return nameList;
        }
    }

    public class BodyD11 : MessageBody
    {
        Dictionary<string, string> gameIdAndName;
        public BodyD11(Dictionary<string, string> gameIdAndName) : base(0xd11)
        {
            this.gameIdAndName = gameIdAndName;
        }

        public Dictionary<string, string> getGameIdAndName()
        {
            return gameIdAndName;
        }
    }

    public class BodyD12 : MessageBody
    {
        string idGame;
        ArrayList idRoom;
        public BodyD12(string idGame, ArrayList idRoom) : base(0xd12)
        {
            this.idGame = idGame;
            this.idRoom = idRoom;
        }

        public string getIdGame()
        {
            return idGame;
        }

        public ArrayList getIdRoom()
        {
            return idRoom;
        }
    }

    public class BodyD13 : MessageBody
    {
        bool createRoomSuccessful;
        string idGame;
        string idRoom;
        public BodyD13(bool createRoomSuccessful, string idGame, string idRoom) : base(0xd13)
        {
            this.createRoomSuccessful = createRoomSuccessful;
            this.idGame = idGame;
            this.idRoom = idRoom;
        }

        public string getIdRoom()
        {
            return idRoom;
        }

        public string getIdGame()
        {
            return idGame;
        }

        public bool isCreateRoomSuccessful()
        {
            return createRoomSuccessful;
        }
    }

    public class BodyD14 : MessageBody
    {
        string idClient;
        string text;
        string numberInRoom;
        public BodyD14(string idClient, string text, string numberInRoom) : base(0xd14)
        {
            this.idClient = idClient;
            this.text = text;
            this.numberInRoom = numberInRoom;
        }

        public string getText()
        {
            return text;
        }

        public string getIdClient()
        {
            return idClient;
        }

        public string getNumberInRoom()
        {
            return numberInRoom;
        }
    }

    public class BodyD15 : MessageBody
    {
        bool joinSuccessful;
        public BodyD15(bool joinSuccessful) : base(0xd15)
        {
            this.joinSuccessful = joinSuccessful;
        }

        public bool isJoinSuccessful()
        {
            return joinSuccessful;
        }
    }

    public class BodyD16 : MessageBody
    {
        string name;
        string text;
        public BodyD16(string name, string text) : base(0xd16)
        {
            this.name = name;
            this.text = text;
        }
    }
}

namespace Message.BodyE
{
    public class BodyE1 : MessageBody
    {
        String gameID;
        String roomID;
        String clientID;
        bool ready;//ture表示准备，false表示不准备
        public BodyE1(String gameID, String roomID, String clientID, bool ready) : base(0xe1)
        {
            this.gameID = gameID;
            this.roomID = roomID;
            this.clientID = clientID;
            this.ready = ready;
        }

        public String getClientID()
        {
            return clientID;
        }

        public String getGameID()
        {
            return gameID;
        }

        public String getRoomID()
        {
            return roomID;
        }

        public bool isReady()
        {
            return ready;
        }
    }

    public class BodyE2 : MessageBody
    {
        String gameID;
        String roomID;
        String clientID;
        bool chase;//ture表示抢地主，false表示不抢地主
        public BodyE2(String gameID, String roomID, String clientID, bool chase) : base(0xe2)
        {
            this.gameID = gameID;
            this.roomID = roomID;
            this.clientID = clientID;
            this.chase = chase;
        }

        public String getClientID()
        {
            return clientID;
        }

        public String getGameID()
        {
            return gameID;
        }

        public String getRoomID()
        {
            return roomID;
        }

        public bool isChase()
        {
            return chase;
        }
    }

    public class BodyE3 : MessageBody
    {
        String gameID;
        String roomID;
        String clientID;
        bool doubleness;//ture表示加倍，false表示不不加倍
        public BodyE3(String gameID, String roomID, String clientID, bool doubleness):base(0xe3)
        {
            this.gameID = gameID;
            this.roomID = roomID;
            this.clientID = clientID;
            this.doubleness = doubleness;
        }

        public String getClientID()
        {
            return clientID;
        }

        public String getGameID()
        {
            return gameID;
        }

        public String getRoomID()
        {
            return roomID;
        }

        public bool isDoubleness()
        {
            return doubleness;
        }
    }

    public class BodyE4 : MessageBody
    {
        String gameID;
        String roomID;
        String clientID;
        CardType cardType;
        ArrayList cardInformation;

        public class BodyE4Builder
        {
            public String gameID;
            public String roomID;
            public String clientID;
            public CardType cardType;
            public ArrayList cardInformation;
            public BodyE4Builder setgameID(String value)
            {
                this.gameID = value;
                return this;
            }
            public BodyE4Builder setroomID(String value)
            {
                this.roomID = value;
                return this;
            }
            public BodyE4Builder setclientID(String value)
            {
                this.clientID = value;
                return this;
            }
            public BodyE4Builder setcardType(CardType cardType)
            {
                this.cardType = cardType;
                return this;
            }
            public BodyE4Builder setcardInformation(ArrayList cardInformation)
            {
                this.cardInformation = cardInformation;
                return this;
            }
            public BodyE4 build()
            {
                return new BodyE4(this);
            }

        }

        BodyE4(BodyE4Builder builder) : base(0xe4)
        { 
            this.gameID = builder.gameID;
            this.roomID = builder.roomID;
            this.clientID = builder.clientID;
            this.cardType = builder.cardType;
            this.cardInformation = builder.cardInformation;
        }

        public String getClientID()
        {
            return clientID;
        }

        public String getGameID()
        {
            return gameID;
        }

        public String getRoomID()
        {
            return roomID;
        }

        public ArrayList getCardInformation()
        {
            return cardInformation;
        }

        public CardType getCardType()
        {
            return cardType;
        }


    }

    public class BodyE5 : MessageBody
    {
        String gameID;
        String roomID;
        String clientID;
        int voiceID;
        public BodyE5(String gameID, String roomID, String clientID, int voiceID) : base(0xe5)
        {
            this.gameID = gameID;
            this.roomID = roomID;
            this.clientID = clientID;
            this.voiceID = voiceID;
        }

        public String getRoomID()
        {
            return roomID;
        }

        public String getGameID()
        {
            return gameID;
        }

        public String getClientID()
        {
            return clientID;
        }

        public int getVoiceID()
        {
            return voiceID;
        }
    }

    public class BodyE6 : MessageBody
    {
        String clientID;
        bool quit;
        public BodyE6(String clientID, bool quit) : base(0xe6)
        {
            this.clientID = clientID;
            this.quit = quit;
        }

        public String getClientID()
        {
            return clientID;
        }

        public bool isQuit()
        {
            return quit;
        }
    }

    public class BodyE7 : MessageBody
    {
        String gameID;
        String roomID;
        String clientID;
        bool ready;//ture表示准备，false表示不准备
        public BodyE7(String gameID, String roomID, String clientID, bool ready) : base(0xe7)
        { 
            this.gameID = gameID;
            this.roomID = roomID;
            this.clientID = clientID;
            this.ready = ready;
        }

        public String getClientID()
        {
            return clientID;
        }

        public String getGameID()
        {
            return gameID;
        }

        public String getRoomID()
        {
            return roomID;
        }

        public bool isReady()
        {
            return ready;
        }

    }

    public class BodyE8 : MessageBody
    {
        String gameID;
        String roomID;
        String clientID;
        ArrayList cardInformation;

        public BodyE8(String gameID, String roomID, String clientID, ArrayList cardInformation) : base(0xe8)
        {
            this.gameID = gameID;
            this.roomID = roomID;
            this.cardInformation = cardInformation;
            this.clientID = clientID;
        }

        public String getClientID()
        {
            return clientID;
        }

        public String getGameID()
        {
            return gameID;
        }

        public String getRoomID()
        {
            return roomID;
        }

        public ArrayList getCardInformation()
        {
            return cardInformation;
        }
    }

    public class BodyE9 : MessageBody
    {
        String gameID;
        String roomID;
        String clientID;//刚刚抢地主的人
        bool chase;//ture表示抢地主，false表示不抢地主
        int multiple;//当前倍数
        public BodyE9(String gameID, String roomID, String clientID, bool chase, int multiple) : base(0xe9)
        {
            this.gameID = gameID;
            this.roomID = roomID;
            this.clientID = clientID;
            this.chase = chase;
            this.multiple = multiple;
        }

        public String getClientID()
        {
            return clientID;
        }

        public String getGameID()
        {
            return gameID;
        }

        public String getRoomID()
        {
            return roomID;
        }

        public bool isChase()
        {
            return chase;
        }

        public int getMultiple()
        {
            return multiple;
        }
    }

    public class BodyE10 : MessageBody
    {
        String gameID;
        String roomID;
        String clientID;//地主的ID
        int multiple;//当前倍数
        public BodyE10(String gameID, String roomID, String clientID, int multiple) : base(0xe10)
        {
            this.gameID = gameID;
            this.roomID = roomID;
            this.clientID = clientID;
            this.multiple = multiple;
        }

        public String getClientID()
        {
            return clientID;
        }

        public String getGameID()
        {
            return gameID;
        }

        public String getRoomID()
        {
            return roomID;
        }

        public int getMultiple()
        {
            return multiple;
        }
    }

    public class BodyE11 : MessageBody
    {
        String gameID;
        String roomID;
        String clientID;//刚刚加倍的人
        bool doubleness;//ture表示加倍，false表示不不加倍
        int multiple;
        public BodyE11(String gameID, String roomID, String clientID, bool doubleness, int multiple):base(0xe11)
        {
            this.gameID = gameID;
            this.roomID = roomID;
            this.clientID = clientID;
            this.doubleness = doubleness;
            this.multiple = multiple;
        }

        public String getClientID()
        {
            return clientID;
        }

        public String getGameID()
        {
            return gameID;
        }

        public String getRoomID()
        {
            return roomID;
        }

        public bool isDoubleness()
        {
            return doubleness;
        }

        public int getMultiple()
        {
            return multiple;
        }
    }

    public class BodyE12 : MessageBody
    {
        String gameID;
        String roomID;
        String clientID;//出牌人的ID
        public BodyE12(String gameID, String roomID, String clientID):base(0xe12)
    {
        this.gameID = gameID;
        this.roomID = roomID;
        this.clientID = clientID;
    }

    public String getRoomID()
    {
        return roomID;
    }

    public String getGameID()
    {
        return gameID;
    }

    public String getClientID()
    {
        return clientID;
    }
}

    public class BodyE13 : MessageBody
    {
        String gameID;
        String roomID;
        String clientID;
        CardType cardType;
        ArrayList cardInformation;//牌面信息
        int remainCardNumber;//出牌者剩余牌数
        int multiple;//当前倍数
        public class BodyE13Builder
        {
            public String gameID;
            public String roomID;
            public String clientID;
            public CardType cardType;
            public ArrayList cardInformation;//牌面信息
            public int remainCardNumber;//出牌者剩余牌数
            public int multiple;//当前倍数

            public BodyE13Builder setgameID(String value)
            {
                this.gameID = value;
                return this;
            }
            public BodyE13Builder setroomID(String value)
            {
                this.roomID = value;
                return this;
            }
            public BodyE13Builder setclientID(String value)
            {
                this.clientID = value;
                return this;
            }
            public BodyE13Builder setcardInformation(ArrayList cardInformation)
            {
                this.cardInformation = cardInformation;
                return this;
            }
            public BodyE13Builder setremainCardNumber(int value)
            {
                this.remainCardNumber = value;
                return this;
            }
            public BodyE13Builder setcardType(CardType value)
            {
                this.cardType = value;
                return this;
            }
            public BodyE13Builder setmultiple(int value)
            {
                this.multiple = value;
                return this;
            }

            public BodyE13 build()
            {
                return new BodyE13(this);
            }

        }
        BodyE13(BodyE13Builder builder) : base(0xe13)
        {
         
            this.cardInformation = builder.cardInformation;
            this.cardType = builder.cardType;
            this.clientID = builder.clientID;
            this.gameID = builder.gameID;
            this.multiple = builder.multiple;
            this.roomID = builder.roomID;
            this.remainCardNumber = builder.remainCardNumber;
        }

        public String getClientID()
        {
            return clientID;
        }

        public String getGameID()
        {
            return gameID;
        }

        public String getRoomID()
        {
            return roomID;
        }

        public int getMultiple()
        {
            return multiple;
        }

        public ArrayList getCardInformation()
        {
            return cardInformation;
        }

        public CardType getCardType()
        {
            return cardType;
        }

        public int getRemainCardNumber()
        {
            return remainCardNumber;
        }
    }

    public class BodyE14 : MessageBody
    {
        String gameID;
        String roomID;
        String clientID;//接收者的ID
        Dictionary<String, int> cIDAndScore;// 房间的人和积分的变动对应表
        bool spring;//是否是春天
        public BodyE14(String gameID, String roomID, String clientID, Dictionary<String, int> cIDAndScore, bool spring) : base(0xe14)
        {
            this.gameID = gameID;
            this.roomID = roomID;
            this.clientID = clientID;
            this.cIDAndScore = cIDAndScore;
            this.spring = spring;
        }

        public String getRoomID()
        {
            return roomID;
        }

        public String getGameID()
        {
            return gameID;
        }

        public String getClientID()
        {
            return clientID;
        }

        public Dictionary<String, int> getcIDAndScore()
        {
            return cIDAndScore;
        }

        public bool isSpring()
        {
            return spring;
        }
    }

    public class BodyE15 : MessageBody
    {
        String clientID;
        String warn;
        public BodyE15(String clientID) : base(0xe15)
        {
            this.clientID = clientID;
            this.warn = "FBI Warning ! Somebody quit illegal";
        }

        public String getClientID()
        {
            return clientID;
        }

        public String getWarn()
        {
            return warn;
        }
    }

    public class BodyE16 : MessageBody
    {
        String clientID;
        String question;
        public BodyE16(String clientID) : base(0xe16)
        {
            this.clientID = clientID;
            question = "are you grab?";
        }

        public String getClientID()
        {
            return clientID;
        }

        public String getQuestion()
        {
            return question;
        }
    }

    public class BodyE17 : MessageBody
    {
        String clientID;//发送者的ID
        int voiceID;
        public BodyE17(String clientID, int voiceID) : base(0xe17)
        {

            this.clientID = clientID;
            this.voiceID = voiceID;
        }

        public String getClientID()
        {
            return clientID;
        }

        public int getVoiceID()
        {
            return voiceID;
        }
    }

    public class BodyE18 : MessageBody
    {
        String gameID;
        String roomID;
        String clientID;//退出者的ID
        public BodyE18(String gameID, String roomID, String clientID) : base(0xe18)
        {
          
            this.gameID = gameID;
            this.roomID = roomID;
            this.clientID = clientID;
        }

        public String getClientID()
        {
            return clientID;
        }

        public String getGameID()
        {
            return gameID;
        }

        public String getRoomID()
        {
            return roomID;
        }
    }

    public class BodyE19 : MessageBody
    {
        String clientID;
        public BodyE19(String clientID) : base(0xe19)
        {
            this.clientID = clientID;
        }

        public String getClientID()
        {
            return clientID;
        }
    }

    public class BodyE20 : MessageBody
    {
        String start;
        public BodyE20() : base(0xe20)
        {
            start = "start";
        }

        public String getStart()
        {
            return start;
        }
    }

}

namespace Message.Error
{
    public class Body11 : MessageBody
    {
        String error;
        public Body11() : base(0x11)
        {
            error = "服务器解密Ticket失败";
        }
    }

    public class Body12 : MessageBody
    {
        String error;
        public Body12() : base(0x12)
        {
            error = "Client提供的Ticket过期";
        }
    }

    public class Body21 : MessageBody
    {
        String error;
        public Body21() : base(0x21)
        {
            error = "账号不存在";
        }
    }

    public class Body22 : MessageBody
    {
        String error;
        public Body22() : base(0x22)
        {
            error = "密码错误";
        }
    }

    public class Body23 : MessageBody
    {
        String error;
        public Body23() : base(0x23)
        {

            error = "未知错误";
        }
    }

    public class Body24 : MessageBody
    {
        String error;
        public Body24() : base(0x24)
        {
            error = "该账户已经处于登录状态";
        }
    }

    public class Body31 : MessageBody
    {
        String error;
        public Body31() : base(0x31)
        {
            error = "游戏列表返回失败";
        }
    }

    public class Body32 : MessageBody
    {
        String error;
        public Body32() : base(0x32)
        {
            error = "房间列表返回失败";
        }
    }

    public class Body33 : MessageBody
    {
        String error;
        public Body33() : base(0x33)
        {
            error = "房间不存在（查看房间）";
        }
    }

    public class Body34 : MessageBody
    {
        String error;
        public Body34() : base(0x34)
        {
            error = "其他错误（未知）";
        }
    }

    public class Body41 : MessageBody
    {
        String error;
        public Body41() : base(0x41)
        {
            error = "房主已在其他房间中";
        }
    }

    public class Body42 : MessageBody
    {
        String error;
        public Body42() : base(0x42)
        {
            error = "当前房间数超过上限";
        }
    }

    public class Body43 : MessageBody
    {
        String error;
        public Body43() : base(0x43)
        {
            error = "房间不存在（加入房间）";
        }
    }

    public class Body44 : MessageBody
    {
        String error;
        public Body44() : base(0x44)
        {
            error = "房间已满";
        }
    }

    public class Body45 : MessageBody
    {
        String error;
        public Body45() : base(0x45)
        {
            error = "房间非法（非正常退出）";
        }
    }

    public class Body46 : MessageBody
    {
        String error;
        public Body46() : base(0x46)
        {
            error = "其他错误（未知）";
        }
    }

    public class Body51 : MessageBody
    {
        String error;
        public Body51() : base(0x51)
        {
            error = "消息转发失败";
        }
    }

    public class Body52 : MessageBody
    {
        String error;
        public Body52() : base(0x52)
        {
            error = "其他错误（未知）";
        }
    }

    public class Body61 : MessageBody
    {
        String error;
        public Body61() : base(0x61)
        {
            error = "玩家非正常退出";
        }
    }

    public class Body62 : MessageBody
    {
        String error;
        public Body62() : base(0x62)
        {
            error = "其他错误（未知）";
        }
    }

    public class Body71 : MessageBody
    {
        String error;
        public Body71() : base(0x71)
        {
            error = "AS认证时，IDc查找失败";
        }
    }

    public class Body72 : MessageBody
    {
        String error;
        public Body72() : base(0x72)
        {

            error = "AS认证时，IDtgs查找失败";
        }
    }

    public class Body81 : MessageBody
    {
        String error;
        public Body81() : base(0x81)
        {
            error = "TGS认证时，IDv查找失败";
        }
    }

    public class Body82 : MessageBody
    {
        String error;
        public Body82() : base(0x82)
        {
            error = "TGS认证时，Ticket过期";
        }
    }

}
