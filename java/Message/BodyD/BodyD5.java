package Message.BodyD;

import Message.MessageBody;

public class BodyD5 extends MessageBody {
    String IDgame;
    String IDroom;
    String IDc;
    BodyD5(String IDgame,String IDroom,String IDc){
        this.IDgame = IDgame;
        this.IDroom = IDroom;
        this.IDc = IDc;
    }

    public String getIDc() {
        return IDc;
    }

    public String getIDgame() {
        return IDgame;
    }

    public String getIDroom() {
        return IDroom;
    }
}
