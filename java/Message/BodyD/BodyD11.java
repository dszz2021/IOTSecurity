package Message.BodyD;

import Message.MessageBody;

public class BodyD11 extends MessageBody {
    String idGame;
    String cName;
    BodyD11(String idGame,String cName){
        this.cName = cName;
        this.idGame = idGame;
    }

    public String getcName() {
        return cName;
    }

    public String getIdGame() {
        return idGame;
    }
}
