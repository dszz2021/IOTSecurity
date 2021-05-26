package Message.ErrorInfo;

import Message.MessageBody;

public class Body72 extends MessageBody {
    String error;
    public Body72() {
        super(0x72);
        error = "AS认证时，IDtgs查找失败";
    }
}
