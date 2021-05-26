package Message.ErrorInfo;

import Message.MessageBody;

public class Body33 extends MessageBody {
    String error;
    public Body33() {
        super(0x33);
        error = "房间不存在（查看房间）";
    }
}
