package Message.BodyD;

import Message.MessageBody;

import java.util.ArrayList;

public class BodyD12 extends MessageBody {
    String idGame;
    ArrayList<String> idRoom;
    BodyD12(String idGame,ArrayList<String> idRoom){
        this.idGame = idGame;
        this.idRoom = idRoom;
    }

    public String getIdGame() {
        return idGame;
    }

    public ArrayList<String> getIdRoom() {
        return idRoom;
    }
}
