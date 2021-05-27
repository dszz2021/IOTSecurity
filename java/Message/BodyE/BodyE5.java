package Message.BodyE;

import Message.MessageBody;

public class BodyE5 extends MessageBody {
    String gameID;
    String roomID;
    String clientID;
    int voiceID;
    public BodyE5(String gameID,String roomID,String clientID,int voiceID){
        super(0xe5);
        this.gameID = gameID;
        this.roomID = roomID;
        this.clientID = clientID;
        this.voiceID = voiceID;
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

    public int getVoiceID() {
        return voiceID;
    }
}
