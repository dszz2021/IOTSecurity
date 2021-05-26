package Message.BodyD;

import Message.MessageBody;

public class BodyD7 extends MessageBody {
    String IDgame;
    String IDroom;
    BodyD7(String idGame,String idRoom){
        super(0xd7);
        this.IDgame = idGame;
        this.IDroom = idRoom;
    }

    public String getIDroom() {
        return IDroom;
    }

    public String getIDgame() {
        return IDgame;
    }
}
