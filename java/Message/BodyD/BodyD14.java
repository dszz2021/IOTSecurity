package Message.BodyD;

import Message.MessageBody;

public class BodyD14 extends MessageBody {
    String idClient;
    String text;
    String numberInRoom;
    BodyD14(String idClient,String text,String numberInRoom){
        super(0xd14);
        this.idClient = idClient;
        this.text = text;
        this.numberInRoom = numberInRoom;
    }

    public String getText() {
        return text;
    }

    public String getIdClient() {
        return idClient;
    }

    public String getNumberInRoom() {
        return numberInRoom;
    }
}
