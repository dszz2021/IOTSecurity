package Message.BodyD;

import Message.MessageBody;

public class BodyD16 extends MessageBody {
    String name;
    String text;
    BodyD16(String name,String text){
        super(0xd16);
        this.name = name;
        this.text = text;
    }
}
