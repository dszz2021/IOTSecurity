package Message.BodyE;

import Message.MessageBody;

public class BodyE15 extends MessageBody {
    String clientID;
    String warn;
    public BodyE15(String clientID){
        super(0xe15);
        this.clientID = clientID;
        this.warn = "FBI Warning ! Somebody quit illegal";
    }

    public String getClientID() {
        return clientID;
    }

    public String getWarn() {
        return warn;
    }
}
