package Message.BodyE;

import Message.MessageBody;

public class BodyE16 extends MessageBody {
    String clientID;
    String question;
    BodyE16(String clientID){
        this.clientID = clientID;
        question = "are you grab?";
    }

    public String getClientID() {
        return clientID;
    }

    public String getQuestion() {
        return question;
    }
}
