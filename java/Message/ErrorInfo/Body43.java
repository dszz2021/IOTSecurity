package Message.ErrorInfo;

import Message.MessageBody;

public class Body43 extends MessageBody {
    String error;
    public Body43() {
        super(0x43);
        error = "房间不存在（加入房间）";
    }
}
