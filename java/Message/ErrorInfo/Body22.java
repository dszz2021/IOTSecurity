package Message.ErrorInfo;

import Message.MessageBody;

public class Body22 extends MessageBody {
    String error;
    public Body22() {
        super(0x22);
        error = "密码错误";
    }
}
