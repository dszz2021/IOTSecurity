package Message.BodyA;

import Message.MessageBody;

public class BodyA2 extends MessageBody {
    String KeyCandTgs;
    String IDtgs;
    String TS2;
    String Lifetime2;
    String TicketTgs;// TicketTGS通过json序列化并加密后的字符串

    public static class BodyA2Builder{
        String KeyCandTgs;
        String IDtgs;
        String TS2;
        String Lifetime2;
        String TicketTgs;

        public BodyA2Builder KeyCandTgs(String value){
            this.KeyCandTgs = value;
            return this;
        }
        public BodyA2Builder IDtgs(String value){
            this.IDtgs = value;
            return this;
        }
        public BodyA2Builder TS2(String value){
            this.TS2 = value;
            return this;
        }
        public BodyA2Builder Lifetime2(String value){
            this.Lifetime2 = value;
            return this;
        }
        public BodyA2Builder TicketTgs(String value){
            this.TicketTgs = value;
            return this;
        }

        public BodyA2 build(){
            return new BodyA2(this);
        }

    }
    private BodyA2(BodyA2Builder builder){
        super(0xa2);
        this.IDtgs = builder.IDtgs;
        this.Lifetime2 = builder.Lifetime2;
        this.TS2 = builder.TS2;
        this.KeyCandTgs = builder.KeyCandTgs;
        this.TicketTgs = builder.TicketTgs;
    }

    public String getIDtgs() {
        return IDtgs;
    }

    public String getTS2() {
        return TS2;
    }

    public String getLifetime2() {
        return Lifetime2;
    }

    public String getKeyCandTgs() {
        return KeyCandTgs;
    }

    public String getTicketTgs() {
        return TicketTgs;
    }
}
