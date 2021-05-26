package test;

import com.google.gson.Gson;

import java.util.ArrayList;

//Json Test
public class Tested {
    String a;
    int b;
    ArrayList<String> c;
    Test test;
    public static class Builder{
        String a;
        int b;
        ArrayList<String> c;
        Test test;
        public Builder a(String val){
            this.a = val;
            return this;
        }
        public Builder b(int val){
            this.b = val;
            return this;
        }
        public Builder c(ArrayList<String> val){
            this.c = val;
            return this;
        }
        public Builder test(){
            this.test = new Test(1,3);
            return this;
        }
        public Tested build(){
            return new Tested(this);
        }
    }
    private Tested(Builder builder){
        this.a = builder.a;
        this.b = builder.b;
        this.c = builder.c;
        this.test = builder.test;
    }

    public static void main(String[] args) {
        ArrayList<String> c = new ArrayList<>();
        c.add("adsa");
        c.add("qeqrqer");
        Tested tested1 = new Tested.Builder().a("aa").b(12).c(c).test().build();
        System.out.println(tested1.a);
        Gson gson = new Gson();
        String json = gson.toJson(tested1);
        System.out.println(json);
        Tested tested2 = gson.fromJson(json,Tested.class);
        System.out.println(tested2.test.aaa);
    }
}

class Test{
    int aaa;
    int bbb;
    Test(int a,int b){
        aaa = a;
        bbb = b;
    }
}