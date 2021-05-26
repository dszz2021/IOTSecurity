package Message.BodyE;

import Message.BodyA.BodyA2;
import Message.BodyA.TicketTGS;
import Message.CardType;
import Message.MessageBody;

import java.util.ArrayList;
import java.util.spi.CalendarDataProvider;

public class BodyE4 extends MessageBody {
    String gameID;
    String roomID;
    String clientID;
    CardType cardType;
    ArrayList<Integer> cardInformation;

    public static class BodyE4Builder{
        String gameID;
        String roomID;
        String clientID;
        CardType cardType;
        ArrayList<Integer> cardInformation;
        public BodyE4Builder gameID(String value){
            this.gameID = value;
            return this;
        }
        public BodyE4Builder roomID(String value){
            this.roomID = value;
            return this;
        }
        public BodyE4Builder clientID(String value){
            this.clientID = value;
            return this;
        }
        public BodyE4Builder cardType(CardType cardType){
            this.cardType = cardType;
            return this;
        }
        public BodyE4Builder cardInformation(ArrayList<Integer> cardInformation){
            this.cardInformation = cardInformation;
            return this;
        }
        public BodyE4 build(){
            return new BodyE4(this);
        }

    }

    BodyE4(BodyE4Builder builder){
        this.gameID = builder.gameID;
        this.roomID = builder.roomID;
        this.clientID = builder.clientID;
        this.cardType = builder.cardType;
        this.cardInformation = builder.cardInformation;
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

    public ArrayList<Integer> getCardInformation() {
        return cardInformation;
    }

    public CardType getCardType() {
        return cardType;
    }

    public static void main(String[] args) {

    }

}
