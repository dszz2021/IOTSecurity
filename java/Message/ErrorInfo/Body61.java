package Message.ErrorInfo;

import Message.MessageBody;

public class Body61 extends MessageBody {
    String error;
    public Body61() {
        super(0x61);
        error = "玩家非正常退出";
    }
}
