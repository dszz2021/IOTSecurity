package Message.ErrorInfo;

import Message.MessageBody;

public class Body62 extends MessageBody {
    String error;
    public Body62() {
        super(0x62);
        error = "其他错误（未知）";
    }
}
