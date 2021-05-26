package Message.BodyE;

import Message.MessageBody;

public class BodyE9 extends MessageBody {
    String gameID;
    String roomID;
    String clientID;//刚刚抢地主的人
    boolean chase;//ture表示抢地主，false表示不抢地主
    int multiple;//当前倍数
    BodyE9(String gameID,String roomID,String clientID,boolean chase,int multiple){
        this.gameID = gameID;
        this.roomID = roomID;
        this.clientID = clientID;
        this.chase = chase;
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

    public boolean isChase() {
        return chase;
    }

    public int getMultiple() {
        return multiple;
    }
}
