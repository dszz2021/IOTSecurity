package DES;

import java.util.ArrayList;
import java.util.Iterator;

public class Key {
    class Pair{
        String leftKey;//28位
        String rightKey;//28位
    }
    String key;//存储56位的二进制编码
    final int[] switchTalbe = {1,1,2,2,2,2,2,2,1,2,2,2,2,2,2,1};//左移次数规定的表
    final int[] PCTable = {14,17,11,24,1,5,3,28,
                           15,6,21,10,23,19,12,4,
                            26,8,16,7,27,20,13,2,
                            41,52,31,37,47,55,30,40,
                            51,45,33,48,44,49,39,56,
                            34,53,46,42,50,36,29,32};//密钥用的置换PC表
    private ArrayList<String>keys;//用来保存每一层的密码
    public Key(String key) {
        this.key = stringToKey56(key);
        keys = new ArrayList<>();
    }


    private String stringToKey56(String keyString){
        char[] keyChar = keyString.toCharArray();
        StringBuilder key56 = new StringBuilder();
        int num=0;
        while (key56.length()<56){
            key56.append(Integer.toBinaryString(keyChar[num]));
            num++;
            if(num == keyChar.length){
                num = 0;
            }
        }
        return key56.substring(0,56);
    }
    /**
     *  用于循环移位的函数
     * @param keyPair   被移位的key(左和右)
     * @param round 轮次，确定移位的位数,该参数从0开始传入
     * @return      返回移位之后的左右两个key
     */
    private Pair switchFunc(Pair keyPair,int round){
        char temp1,temp2;
        int switchNum = switchTalbe[round];
        //先移左边
        char [] left = keyPair.leftKey.toCharArray();
        temp1 = left[0];
        temp2 = left[1];
        for(int i=0; i < 28-switchNum;i++){
            left[i] = left[i+switchNum];
        }
        if(switchNum == 1){
            left[27] = temp1;
        }else if (switchNum == 2){
            left[26] = temp1;
            left[27] = temp2;
        }
        keyPair.leftKey = String.copyValueOf(left);

        //再移动右边
        char [] right = keyPair.rightKey.toCharArray();
        temp1 = right[0];
        temp2 = right[1];
        for(int i=0; i < 28-switchNum;i++){
            right[i] = left[i+switchNum];
        }
        if(switchNum == 1){
            right[27] = temp1;
        }else if (switchNum == 2){
            right[26] = temp1;
            right[27] = temp2;
        }
        keyPair.rightKey = String.copyValueOf(right);
        return keyPair;
    }

    /**
     *
     * @param keyPair
     * @return 某一轮次的48位key
     */
    private String getKey(Pair keyPair){
        String key56 = keyPair.leftKey+keyPair.rightKey;
        char[] key48 = new char[48];
        for(int i =0 ;i<48;i++){
            key48[i] = key56.charAt(PCTable[i]-1);
        }
        return String.copyValueOf(key48);
    }

    public ArrayList<String> getKeys(){
        //首先初始化Pair
        Pair keyPair = new Pair();
        keyPair.leftKey = key.substring(0,28);
        keyPair.rightKey = key.substring(28,56);

        //循环得到key48
        for(int i = 0;i<16;i++){
            String temKey;
            keyPair = switchFunc(keyPair,i);
            temKey = getKey(keyPair);
//            keys.add(i,temKey);
            keys.add(temKey);
        }
        return keys;
    }

    /**
     * 测试使用的main
     */
    public static void main(String[] args) {
        Key key = new Key("1234567890abcd");
        ArrayList<String> keys = new ArrayList<>();
        keys = key.getKeys();
        Iterator iterator = keys.iterator();
        while(iterator.hasNext()){
            String a = (String) iterator.next();
            System.out.println(a.length()+"  "+a);
        }
    }
}
