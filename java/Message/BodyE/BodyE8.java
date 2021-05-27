package Message.BodyE;

import Message.CardType;
import Message.MessageBody;

import java.util.ArrayList;

public class BodyE8 extends MessageBody {
    String gameID;
    String roomID;
    String clientID;
    ArrayList<Integer> cardInformation;

    public BodyE8(String gameID,String roomID,String clientID,ArrayList<Integer> cardInformation){
        super(0xe8);
        this.gameID = gameID;
        this.roomID = roomID;
        this.cardInformation = cardInformation;
        this.clientID = clientID;
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
}
