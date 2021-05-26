package Message.BodyE;

import Message.MessageBody;

public class BodyE6 extends MessageBody {
    String clientID;
    boolean quit;
    BodyE6(String clientID,boolean quit){
        this.clientID = clientID;
        this.quit = quit;
    }

    public String getClientID() {
        return clientID;
    }

    public boolean isQuit() {
        return quit;
    }
}
