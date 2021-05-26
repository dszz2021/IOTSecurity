package Message.BodyE;

import Message.MessageBody;

public class BodyE2 extends MessageBody {
    String gameID;
    String roomID;
    String clientID;
    boolean chase;//ture表示抢地主，false表示不抢地主
    BodyE2(String gameID,String roomID,String clientID,boolean chase){
        this.gameID = gameID;
        this.roomID = roomID;
        this.clientID = clientID;
        this.chase = chase;
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

    public boolean isChase() {
        return chase;
    }
}
