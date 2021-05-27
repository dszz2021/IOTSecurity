package Message.ErrorInfo;

import Message.MessageBody;

public class Body41 extends MessageBody {
    String error;
    public Body41() {
        super(0x41);
        error = "房主已在其他房间中";
    }
}
