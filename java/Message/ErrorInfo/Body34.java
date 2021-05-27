package Message.ErrorInfo;

import Message.MessageBody;

public class Body34 extends MessageBody {
    String error;
    public Body34() {
        super(0x34);
        error = "其他错误（未知）";
    }
}
