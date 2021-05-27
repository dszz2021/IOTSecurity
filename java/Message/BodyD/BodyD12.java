package Message.BodyD;

import Message.MessageBody;

import java.util.ArrayList;

public class BodyD12 extends MessageBody {
    String idGame;
    ArrayList<String> idRoom;
    public BodyD12(String idGame,ArrayList<String> idRoom){
        super(0xd12);
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
