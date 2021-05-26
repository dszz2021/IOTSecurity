package Message.BodyD;

import Message.MessageBody;

public class BodyD1 extends MessageBody {
    String IDc;
    public BodyD1(String IDc){
        super(0xd1);
        this.IDc = IDc;
    }

    public String getIDc() {
        return IDc;
    }
}
