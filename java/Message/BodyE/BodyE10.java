package Message.BodyE;

import Message.MessageBody;

public class BodyE10 extends MessageBody {
    String gameID;
    String roomID;
    String clientID;//地主的ID
    int multiple;//当前倍数
    public BodyE10(String gameID,String roomID,String clientID,int multiple){
        super(0xe10);
        this.gameID = gameID;
        this.roomID = roomID;
        this.clientID = clientID;
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

    public int getMultiple() {
        return multiple;
    }
}
