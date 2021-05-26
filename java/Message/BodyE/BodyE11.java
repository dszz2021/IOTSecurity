package Message.BodyE;

import Message.MessageBody;

public class BodyE11 extends MessageBody {
    String gameID;
    String roomID;
    String clientID;//刚刚加倍的人
    boolean doubleness;//ture表示加倍，false表示不不加倍
    int multiple;
    BodyE11(String gameID,String roomID,String clientID,boolean doubleness,int multiple){
        super(0xe11);
        this.gameID = gameID;
        this.roomID = roomID;
        this.clientID = clientID;
        this.doubleness = doubleness;
        this.multiple = multiple;
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

    public int getMultiple() {
        return multiple;
    }
}
