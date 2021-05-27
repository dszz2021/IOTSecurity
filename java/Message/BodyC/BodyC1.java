package Message.BodyC;
import Message.MessageBody;
public class BodyC1 extends MessageBody {
    String TicketV;
    String Authenticator;
    public BodyC1(String TicketV,String Authenticator){
        super(0xC1);
        this.TicketV = TicketV;
        this.Authenticator = Authenticator;
    }

    public String getTicketV() {
        return TicketV;
    }

    public String getAuthenticator() {
        return Authenticator;
    }
}
