package Message.BodyD;

import Message.MessageBody;

public class BodyD9 extends MessageBody {
    boolean changeSuccessful;
    String reason;
    public BodyD9(boolean changeSuccessful){
        super(0xd9);
        this.changeSuccessful = changeSuccessful;
        reason = "successful";
    }

    public void setReason(String reason) {
        this.reason = reason;
    }

    public boolean isChangeSuccessful() {
        return changeSuccessful;
    }
}
