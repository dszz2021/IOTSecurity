package DES;

import java.util.ArrayList;
import java.util.Iterator;

public class StringByte {
    /**
     *  该函数将明文转化为二进制字符串，并且64位为一组不足64位的高位补零
     *  模块输入为一串明文字符串text
     *  输出为该字符串对应的64位一组的多组二进制字符串
     */
    public ArrayList<String> StringToBinary(String text){
        ArrayList<String> binaryCode=new ArrayList<>();
        char[] chars = text.toCharArray();
        String code="";
        int i=0;
        for(char a:chars){
            i++;//记录循环次数，每四次循环能凑够一个64位二进制存入arraylist中；
            //将字符转化为二进制字符串
            String charCode = Integer.toBinaryString(a);
            //若二进制字符串不满16位则在高位用0补齐
            if(charCode.length()!=16){
                int num = 16-charCode.length();
                for(int j=0;j<num;j++){
                    charCode="0"+charCode;
                }
            }
            code +=charCode;
            if (i==4){
                //将i置零
                i=0;
                binaryCode.add(code);
                code="";
            }
        }
        //结束循环后，若最后一轮不足64位则高位补零
        if(code!=""){
            int num = 64-code.length();
            for(int j=0;j<num;j++){
                code="0"+code;
            }
            binaryCode.add(code);
        }

        return binaryCode;
    }

    /**
     *
     * @param binaryCode 解码之后得到的二进制字符串
     * @return 返回值是该组二进制字符串所代表的明文
     */
    public String BinaryToString(ArrayList<String> binaryCode){
        String meanLessText="0000000000000000";
        String text="";
        Iterator<String> iterator=binaryCode.iterator();
        while (iterator.hasNext()){
            String code=iterator.next();//code中位64位二进制数
            //切成16位的二进制转化为char并将其赋给text；
            while (code.length()>=16){
                String temp = code.substring(0,16);
                if(!temp.equals(meanLessText)){
                    int intCode = Integer.parseInt(temp,2);
                    text+=(char)intCode;
                }
                code=code.substring(16);
            }
        }

        return text;
    }

    /**
     *这个主函数仅用于测试使用
     */
    public static void main(String[] args) {
        StringByte stringByte = new StringByte();
        System.out.println(stringByte.BinaryToString(stringByte.StringToBinary("阿巴阿巴阿巴")));
    }
}
