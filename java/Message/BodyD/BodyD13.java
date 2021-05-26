package Message.BodyD;

import Message.MessageBody;

public class BodyD13 extends MessageBody {
    boolean createRoomSuccessful;
    String idGame;
    String idRoom;
    BodyD13(boolean createRoomSuccessful,String idGame,String idRoom){
        super(0xd13);
        this.createRoomSuccessful = createRoomSuccessful;
        this.idGame = idGame;
        this.idRoom = idRoom;
    }

    public String getIdRoom() {
        return idRoom;
    }

    public String getIdGame() {
        return idGame;
    }

    public boolean isCreateRoomSuccessful() {
        return createRoomSuccessful;
    }
}
