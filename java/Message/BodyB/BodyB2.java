package Message.BodyB;

import Message.MessageBody;

public class BodyB2 extends MessageBody {
    String KeyCAndV;
    String IDv;
    String TS4;
    String TicketV;// 序列化并且加密后的字符串

    public BodyB2(String keyCAndV,String IDv,String TS4,String ticketV){
        super(0xb2);
        this.KeyCAndV = keyCAndV;
        this.IDv = IDv;
        this.TS4 = TS4;
        this.TicketV = ticketV;
    }

    public String getIDv() {
        return IDv;
    }

    public String getKeyCAndV() {
        return KeyCAndV;
    }

    public String getTicketV() {
        return TicketV;
    }

    public String getTS4() {
        return TS4;
    }

}
