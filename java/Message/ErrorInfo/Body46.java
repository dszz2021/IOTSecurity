package Message.ErrorInfo;

import Message.MessageBody;

public class Body46 extends MessageBody {
    String error;
    public Body46() {
        super(0x46);
        error = "其他错误（未知）";
    }
}
