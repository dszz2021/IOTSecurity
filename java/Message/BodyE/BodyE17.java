package Message.BodyE;

import Message.MessageBody;

public class BodyE17 extends MessageBody {
    String clientID;//发送者的ID
    int voiceID;
    public BodyE17(String clientID,int voiceID){
        super(0xe17);
        this.clientID = clientID;
        this.voiceID = voiceID;
    }

    public String getClientID() {
        return clientID;
    }

    public int getVoiceID() {
        return voiceID;
    }
}
