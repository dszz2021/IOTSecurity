package Message.BodyD;

import Message.MessageBody;

public class BodyD9 extends MessageBody {
    boolean changeSuccessful;
    String reason;
    BodyD9(boolean changeSuccessful){
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
