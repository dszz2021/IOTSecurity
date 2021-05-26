package Message.BodyE;

import Message.MessageBody;

public class BodyE12 extends MessageBody {
    String gameID;
    String roomID;
    String clientID;//出牌人的ID
    BodyE12(String gameID,String roomID,String clientID){
        super(0xe12);
        this.gameID = gameID;
        this.roomID = roomID;
        this.clientID = clientID;
    }

    public String getRoomID() {
        return roomID;
    }

    public String getGameID() {
        return gameID;
    }

    public String getClientID() {
        return clientID;
    }
}
