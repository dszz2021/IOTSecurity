package Message.BodyD;

import Message.MessageBody;

public class BodyD8 extends MessageBody {
    boolean loadSuccessful;
    String cName;
    String score;
    BodyD8(boolean loadSuccessful,String name,String score){
        this.loadSuccessful = loadSuccessful;
        this.cName = name;
        this.score = score;
    }

    public String getName() {
        return cName;
    }

    public String getScore() {
        return score;
    }
    public  boolean isLoadSuccessful(){
        return loadSuccessful;
    }
}
