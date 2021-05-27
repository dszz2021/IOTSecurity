package Message.ErrorInfo;

import Message.MessageBody;

public class Body82 extends MessageBody {
    String error;
    public Body82() {
        super(0x82);
        error = "TGS认证时，Ticket过期";
    }
}
