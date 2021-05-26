package Message.ErrorInfo;

import Message.MessageBody;

public class Body71 extends MessageBody {
    String error;
    public Body71() {
        super(0x71);
        error = "AS认证时，IDc查找失败";
    }
}
