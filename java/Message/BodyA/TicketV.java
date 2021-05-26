package Message.BodyA;

//构造这个类的方法与
public class TicketV {
    String KeyCAndV;
    String IDc;
    String IDv;
    String ADc;
    String TS4;
    String Lifetime4;

    public static class TicketVBuilder{
        private String KeyCAndV;
        private String IDc;
        private String IDv;
        String ADc;
        private String TS4;
        private String Lifetime4;
        public TicketVBuilder KeyCAndV(String val){
            this.KeyCAndV = val;
            return this;
        }
        public TicketVBuilder IDc(String val){
            this.IDc = val;
            return this;
        }
        public TicketVBuilder IDv(String val){
            this.IDv = val;
            return this;
        }
        public TicketVBuilder TS4(String val){
            this.TS4 = val;
            return this;
        }
        public TicketVBuilder Lifetime4(String val){
            this.Lifetime4 = val;
            return this;
        }
        public TicketVBuilder adc(String val){
            this.ADc = val;
            return this;
        }
        public TicketV build(){
            return new TicketV(this);
        }
    }

    private TicketV(TicketVBuilder builder){
        this.IDc = builder.IDc;
        this.KeyCAndV = builder.KeyCAndV;
        this.TS4 = builder.TS4;
        this.Lifetime4 = builder.Lifetime4;
        this.IDv = builder.IDv;
        this.ADc = builder.ADc;
    }

    public String getIDc() {
        return IDc;
    }

    public String getTS4() {
        return TS4;
    }

    public String getKeyCAndV() {
        return KeyCAndV;
    }

    public String getIDv() {
        return IDv;
    }

    public String getADc() {
        return ADc;
    }

    public String getLifetime4() {
        return Lifetime4;
    }
}
