package Message.BodyD;

import Message.MessageBody;

import java.util.ArrayList;

public class BodyD10 extends MessageBody {
    ArrayList<String> nameList;
    BodyD10(ArrayList<String> nameList){
        super(0xd10);
        this.nameList = nameList;
    }

    public ArrayList<String> getNameList() {
        return nameList;
    }
}
