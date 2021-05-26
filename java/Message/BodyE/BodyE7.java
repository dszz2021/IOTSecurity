package Message.BodyE;

import Message.MessageBody;

public class BodyE7 extends MessageBody {
    String gameID;
    String roomID;
    String clientID;
    boolean ready;//ture表示准备，false表示不准备
    BodyE7(String gameID,String roomID,String clientID,boolean ready){
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
