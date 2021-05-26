package Message.BodyD;

import Message.MessageBody;

public class BodyD15 extends MessageBody {
    boolean joinSuccessful;
    BodyD15(boolean joinSuccessful){
        this.joinSuccessful = joinSuccessful;
    }

    public boolean isJoinSuccessful() {
        return joinSuccessful;
    }
}
