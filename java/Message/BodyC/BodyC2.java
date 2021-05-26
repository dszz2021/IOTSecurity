package Message.BodyC;

import Message.MessageBody;

public class BodyC2 extends MessageBody {
    String TS;
    public BodyC2(String TS5plus1){
        super(0xC1);
        this.TS = TS5plus1;
    }

    public String getTS() {
        return TS;
    }
}
