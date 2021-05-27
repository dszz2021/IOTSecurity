package Message.BodyD;

import Message.MessageBody;

public class BodyD8 extends MessageBody {
    boolean loadSuccessful;
    String cName;
    String score;
    public BodyD8(boolean loadSuccessful,String name,String score){
        super(0xd8);
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
