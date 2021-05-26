package Message.ErrorInfo;

import Message.MessageBody;

public class Body52 extends MessageBody {
    String error;
    public Body52() {
        super(0x52);
        error = "其他错误（未知）";
    }
}
