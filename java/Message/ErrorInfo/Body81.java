package Message.ErrorInfo;

import Message.MessageBody;

public class Body81 extends MessageBody {
    String error;
    public Body81() {
        super(0x81);
        error = "TGS认证时，IDv查找失败";
    }
}
