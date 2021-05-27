package Message.BodyD;

import Message.MessageBody;

public class BodyD3 extends MessageBody {
    String IDgame;
    public BodyD3(String IDgame){
        super(0xd3);
        this.IDgame = IDgame;
    }

    public String getIDgame() {
        return IDgame;
    }
}
