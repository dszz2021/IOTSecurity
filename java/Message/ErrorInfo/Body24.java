package Message.ErrorInfo;

import Message.MessageBody;

public class Body24 extends MessageBody {
    String error;
    public Body24() {
        super(0x24);
        error = "该账户已经处于登录状态";
    }
}
