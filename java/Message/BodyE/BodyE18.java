package Message.BodyE;

import Message.MessageBody;

public class BodyE18 extends MessageBody {
    String gameID;
    String roomID;
    String clientID;//退出者的ID
    public BodyE18(String gameID,String roomID,String clientID){
        super(0xe18);
        this.gameID = gameID;
        this.roomID = roomID;
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
}
