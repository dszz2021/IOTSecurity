package Message.ErrorInfo;

import Message.MessageBody;

public class Body42 extends MessageBody {
    String error;
    public Body42() {
        super(0x42);
        error = "当前房间数超过上限";
    }
}
