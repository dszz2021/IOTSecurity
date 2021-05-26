package Message.BodyE;

import Message.MessageBody;

public class BodyE3 extends MessageBody {
    String gameID;
    String roomID;
    String clientID;
    boolean doubleness;//ture表示加倍，false表示不不加倍
    BodyE3(String gameID,String roomID,String clientID,boolean doubleness){
        super(0xe3);
        this.gameID = gameID;
        this.roomID = roomID;
        this.clientID = clientID;
        this.doubleness = doubleness;
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

    public boolean isDoubleness() {
        return doubleness;
    }
}
