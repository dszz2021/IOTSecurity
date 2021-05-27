package Message.ErrorInfo;

import Message.MessageBody;

public class Body45 extends MessageBody {
    String error;
    public Body45() {
        super(0x45);
        error = "房间非法（非正常退出）";
    }
}
