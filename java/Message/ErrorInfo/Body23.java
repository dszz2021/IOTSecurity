package Message.ErrorInfo;

import Message.MessageBody;

public class Body23 extends MessageBody {
    String error;
    public Body23() {
        super(0x23);
        error = "未知错误";
    }
}
