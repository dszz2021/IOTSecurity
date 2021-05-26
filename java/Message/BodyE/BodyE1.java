package Message.BodyE;

import Message.MessageBody;

public class BodyE1 extends MessageBody {
    String gameID;
    String roomID;
    String clientID;
    boolean ready;//ture表示准备，false表示不准备
    BodyE1(String gameID,String roomID,String clientID,boolean ready){
        super(0xe1);
        this.gameID = gameID;
        this.roomID = roomID;
        this.clientID = clientID;
        this.ready = ready;
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

    public boolean isReady() {
        return ready;
    }
}
