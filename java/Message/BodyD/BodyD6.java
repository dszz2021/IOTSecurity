package Message.BodyD;

import Message.MessageBody;

public class BodyD6 extends MessageBody {
    String IDc;
    String text;
    public BodyD6(String IDc,String text){
        super(0xd6);
        this.IDc = IDc;
        this.text = text;
    }

    public String getIDc() {
        return IDc;
    }

    public String getText() {
        return text;
    }
}
