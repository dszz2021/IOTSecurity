package Message.ErrorInfo;

import Message.MessageBody;

public class Body44 extends MessageBody {
    String error;
    public Body44() {
        super(0x44);
        error = "房间已满";
    }
}
