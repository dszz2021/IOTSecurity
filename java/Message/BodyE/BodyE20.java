package Message.BodyE;

import Message.MessageBody;

public class BodyE20 extends MessageBody {
    String start;
    BodyE20(){
        super(0xe20);
        start = "start";
    }

    public String getStart() {
        return start;
    }
}
