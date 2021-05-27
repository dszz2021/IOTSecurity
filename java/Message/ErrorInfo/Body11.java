package Message.ErrorInfo;

import Message.MessageBody;

public class Body11 extends MessageBody {
    String error;
    public Body11() {
        super(0x11);
        error = "服务器解密Ticket失败";
    }
}
