package Message.ErrorInfo;

import Message.MessageBody;

public class Body51 extends MessageBody {
    String error;
    public Body51() {
        super(0x51);
        error = "消息转发失败";
    }
}
