package Message.BodyB;

public class Authenticator {
    String IDc;
    String ADc;
    String TS;
    public Authenticator(String IDc,String ADc,String TS){
        this.ADc = ADc;
        this.IDc = IDc;
        this.TS = TS;
    }

    public String getIDc() {
        return IDc;
    }

    public String getADc() {
        return ADc;
    }

    public String getTS3() {
        return TS;
    }
}
