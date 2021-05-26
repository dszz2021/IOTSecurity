package Message.BodyE;

import Message.CardType;
import Message.MessageBody;

import java.util.ArrayList;

public class BodyE13 extends MessageBody {
    String gameID;
    String roomID;
    String clientID;
    CardType cardType;
    ArrayList<Integer> cardInformation;//牌面信息
    int remainCardNumber;//出牌者剩余牌数
    int multiple;//当前倍数
    public static class BodyE13Builder{
        String gameID;
        String roomID;
        String clientID;
        CardType cardType;
        ArrayList<Integer> cardInformation;//牌面信息
        int remainCardNumber;//出牌者剩余牌数
        int multiple;//当前倍数

        public BodyE13Builder gameID(String value) {
            this.gameID = value;
            return this;
        }
        public BodyE13Builder roomID(String value) {
            this.roomID = value;
            return this;
        }
        public BodyE13Builder clientID(String value) {
            this.clientID = value;
            return this;
        }
        public BodyE13Builder cardInformation(ArrayList<Integer> cardInformation) {
            this.cardInformation = cardInformation;
            return this;
        }
        public BodyE13Builder remainCardNumber(int value) {
            this.remainCardNumber = value;
            return this;
        }
        public BodyE13Builder cardType(CardType value) {
            this.cardType = value;
            return this;
        }
        public BodyE13Builder multiple(int value) {
            this.multiple = value;
            return this;
        }

        public BodyE13 build(){
            return new BodyE13(this);
        }

    }
    BodyE13(BodyE13Builder builder){
        super(0xe13);
        this.cardInformation = builder.cardInformation;
        this.cardType = builder.cardType;
        this.clientID = builder.clientID;
        this.gameID = builder.gameID;
        this.multiple = builder.multiple;
        this.roomID = builder.roomID;
        this.remainCardNumber = builder.remainCardNumber;
    }

    public String getClientID() {
        return clientID;
    }

    public String getGameID() {
        return gameID;
    }

    public String getRoomID() {
        return roomID;
    }

    public int getMultiple() {
        return multiple;
    }

    public ArrayList<Integer> getCardInformation() {
        return cardInformation;
    }

    public CardType getCardType() {
        return cardType;
    }

    public int getRemainCardNumber() {
        return remainCardNumber;
    }
}
