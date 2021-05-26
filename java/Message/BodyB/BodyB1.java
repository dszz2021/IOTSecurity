package Message.BodyB;

import Message.MessageBody;

public class BodyB1 extends MessageBody {
    String IDv;
    String TicketTgs;//序列化并且加密之后的ticket
    String Authenticator; //序列化并且加密之后的Authenticator

    public BodyB1(String IDv,String TicketTgs,String Authenticator){
        super(0xb1);
        this.IDv = IDv;
        this.TicketTgs = TicketTgs;
        this.Authenticator = Authenticator;
    }
    public String getTicketTgs() {
        return TicketTgs;
    }

    public String getAuthenticator() {
        return Authenticator;
    }

    public String getIDv() {
        return IDv;
    }
}
