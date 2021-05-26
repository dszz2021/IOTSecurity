package Message.ErrorInfo;

import Message.MessageBody;

public class Body32 extends MessageBody {
    String error;
    public Body32() {
        super(0x32);
        error = "房间列表返回失败";
    }
}
