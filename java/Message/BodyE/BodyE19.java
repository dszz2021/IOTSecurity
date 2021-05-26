package Message.BodyE;

import Message.MessageBody;

public class BodyE19 extends MessageBody {
    String clientID;
    BodyE19(String clientID){
        super(0xe19);
        this.clientID = clientID;
    }

    public String getClientID() {
        return clientID;
    }
}
