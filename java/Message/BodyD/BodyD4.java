package Message.BodyD;

import Message.MessageBody;

public class BodyD4 extends MessageBody {
    String IDgame;
    String IDc;
    String text;
    public BodyD4(String IDgame,String IDc,String text){
        super(0xd4);
        this.IDgame = IDgame;
        this.IDc = IDc;
        this.text = text;
    }

    public String getIDgame() {
        return IDgame;
    }

    public String getIDc() {
        return IDc;
    }

    public String getText() {
        return text;
    }

}
