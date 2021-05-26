package Message.BodyA;

public class TicketTGS {
    private String KeyCAndTgs;
    private String IDc;
    private String ADc;
    private String IDtgs;
    private String TS2;
    private String Lifetime2;

    public static class TicketTGSBuilder{
        private String KeyCAndTgs;
        private String IDc;
        private String ADc;
        private String IDtgs;
        private String TS2;
        private String Lifetime2;
        public TicketTGSBuilder KeyCAndTgs(String val){
            this.KeyCAndTgs = val;
            return this;
        }
        public TicketTGSBuilder IDc(String val){
            this.IDc = val;
            return this;
        }
        public TicketTGSBuilder ADc(String val){
            this.ADc = val;
            return this;
        }
        public TicketTGSBuilder IDtgs(String val){
            this.IDtgs = val;
            return this;
        }
        public TicketTGSBuilder TS2(String val){
            this.TS2 = val;
            return this;
        }
        public TicketTGSBuilder Lifetime2(String val){
            this.Lifetime2 = val;
            return this;
        }
        public TicketTGS build(){
            return new TicketTGS(this);
        }
    }

    private TicketTGS(TicketTGSBuilder builder){
        this.ADc = builder.ADc;
        this.IDc = builder.IDc;
        this.KeyCAndTgs = builder.KeyCAndTgs;
        this.TS2 = builder.TS2;
        this.Lifetime2 = builder.Lifetime2;
        this.IDtgs = builder.IDtgs;
    }

    public String getADc() {
        return ADc;
    }

    public String getIDc() {
        return IDc;
    }

    public String getIDtgs() {
        return IDtgs;
    }

    public String getKeyCAndTgs() {
        return KeyCAndTgs;
    }

    public String getLifetime2() {
        return Lifetime2;
    }

    public String getTS2() {
        return TS2;
    }

    public static void main(String[] args) {
        //想得到这个类时，可以通过如下调用方法,example:
        TicketTGS ticketTGS = new TicketTGSBuilder().
                KeyCAndTgs("相应内容").
                IDc("相应内容").
                ADc("相应内容").
                IDtgs("相应内容").
                TS2("相应内容").
                Lifetime2("相应内容").build();
        System.out.println(ticketTGS.getIDtgs());
    }
}
