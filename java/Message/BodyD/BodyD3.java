package Message.BodyD;

import Message.MessageBody;

public class BodyD3 extends MessageBody {
    String IDgame;
    BodyD3(String IDgame){
        this.IDgame = IDgame;
    }

    public String getIDgame() {
        return IDgame;
    }
}
