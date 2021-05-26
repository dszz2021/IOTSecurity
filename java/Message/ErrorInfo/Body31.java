package Message.ErrorInfo;

import Message.MessageBody;

public class Body31 extends MessageBody {
    String error;
    public Body31() {
        super(0x31);
        error = "游戏列表返回失败";
    }
}
