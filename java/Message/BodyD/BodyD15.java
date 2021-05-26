package Message.BodyD;

import Message.MessageBody;

public class BodyD15 extends MessageBody {
    boolean joinSuccessful;
    BodyD15(boolean joinSuccessful){
        super(0xd15);
        this.joinSuccessful = joinSuccessful;
    }

    public boolean isJoinSuccessful() {
        return joinSuccessful;
    }
}
