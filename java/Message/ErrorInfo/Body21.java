package Message.ErrorInfo;

import Message.MessageBody;

public class Body21 extends MessageBody {
    String error;
    public Body21() {
        super(0x21);
        error = "账号不存在";
    }
}
