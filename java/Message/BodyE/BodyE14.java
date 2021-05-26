package Message.BodyE;

import Message.MessageBody;

import java.util.ArrayList;
import java.util.Dictionary;
import java.util.Map;

public class BodyE14 extends MessageBody {
    String gameID;
    String roomID;
    String clientID;//接收者的ID
    Map<String,Integer> cIDAndScore;// 房间的人和积分的变动对应表
    boolean spring;//是否是春天
    BodyE14(String gameID,String roomID,String clientID,Map<String,Integer> cIDAndScore,boolean spring){
        super(0xe14);
        this.gameID = gameID;
        this.roomID = roomID;
        this.clientID = clientID;
        this.cIDAndScore = cIDAndScore;
        this.spring = spring;
    }

    public String getRoomID() {
        return roomID;
    }

    public String getGameID() {
        return gameID;
    }

    public String getClientID() {
        return clientID;
    }

    public Map<String, Integer> getcIDAndScore() {
        return cIDAndScore;
    }

    public boolean isSpring() {
        return spring;
    }
}
