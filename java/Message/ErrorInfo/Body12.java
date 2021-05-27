package Message.ErrorInfo;

import Message.MessageBody;

public class Body12 extends MessageBody {
    String error;
    public Body12() {
        super(0x12);
        error = "Client提供的Ticket过期";
    }
}
