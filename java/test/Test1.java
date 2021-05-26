package test;

import com.google.gson.Gson;

import java.util.*;

public class Test1 {
    String a = "12";
    boolean ready = true;
    ArrayList<String> stringList;
    Map<String,Integer> map;

    public static void main(String[] args) {
//        Test3 test3 = new Test3();
//        Test1 test = new Test2(test3);
//        System.out.println(((Test2) test).getB());

        Gson gson = new Gson();
        Test1 test1 = new Test1();
        test1.stringList = new ArrayList<>();
        test1.stringList.add("aaa");
        test1.stringList.add("bbb");
        test1.map = new HashMap<>();
        test1.map.put("123",123);
        test1.map.put("456",456);
        test1.map.put("789",789);
        String a = gson.toJson(test1);
        System.out.println(a);
        Test3 test3=gson.fromJson(a,Test3.class);

        if(test3.ready) {
            System.out.println(test3.map.get("789"));
        }
    }
}
class Test2 extends Test1 {
    int a;
    public String b;
    Test2(Test1 test1){
        b = "asdf";
    }
    //@Override
    public String getB() {
        return b;
    }
}

class Test3  {
    String a;
    boolean ready;
    //ArrayList<String> stringList;
    Map<String,Integer> map;
}