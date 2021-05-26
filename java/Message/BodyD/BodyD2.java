package Message.BodyD;

import Message.MessageBody;

public class BodyD2 extends MessageBody {
    String IDc;
    String cName;//修改后的名字
    public BodyD2(String IDc,String name){
        this.IDc = IDc;
        this.cName = name;
    }

    public String getIDc() {
        return IDc;
    }

    public String getName() {
        return cName;
    }
}
