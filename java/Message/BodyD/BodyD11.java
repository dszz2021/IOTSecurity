package Message.BodyD;

import Message.MessageBody;

import java.util.ArrayList;
import java.util.Map;

public class BodyD11 extends MessageBody {
    Map<String,String> gameIdAndName;
    public BodyD11( Map<String,String> gameIdAndName){
        super(0xd11);
        this.gameIdAndName = gameIdAndName;
    }

    public Map<String, String> getGameIdAndName() {
        return gameIdAndName;
    }
}
