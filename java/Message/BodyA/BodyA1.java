package Message.BodyA;

import Message.MessageBody;

public class BodyA1 extends MessageBody {
    String IDc;
    String IDtgs;
    String TS1;
    public BodyA1(String IDc,String IDtgs,String TS1){
        this.IDc = IDc;
        this.IDtgs = IDtgs;
        this.TS1 = TS1;
    }

    public String getIDtgs() {
        return IDtgs;
    }

    public String getIDc() {
        return IDc;
    }

    public String getTS1() {
        return TS1;
    }
}
