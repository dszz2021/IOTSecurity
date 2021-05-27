package DES;

import java.util.*;
import java.util.regex.Pattern;

public class DES {
    final int EpTable[] = {
            32,1,2,3,4,5,
            4,5,6,7,8,9,
            8,9,10,11,12,13,
            12,13,14,15,16,17,
            16,17,18,19,20,21,
            20,21,22,23,24,25,
            24,25,26,27,28,29,
            28,29,30,31,32,1
    };
    final int IpTable[] = {
            58,50,42,34,26,18,10,2,
            60,52,44,36,28,20,12,4,
            62,54,46,38,30,22,14,6,
            64,56,48,40,32,24,16,8,
            57,49,41,33,25,17,9,1,
            59,51,43,35,27,19,11,3,
            61,53,45,37,29,21,13,5,
            63,55,47,39,31,23,15,7};
    final int deIPTable[] = {
            40,8,48,16,56,24,64,32,
            39,7,47,15,55,23,63,31,
            38,6,46,14,54,22,62,30,
            37,5,45,13,53,21,61,29,
            36,4,44,12,52,20,60,28,
            35,3,43,11,51,19,59,27,
            34,2,42,10,50,18,58,26,
            33,1,41,9,49,17,57,25
    };

    final int [][] S1Box = {
            {14,4,13,1,2,15,11,8,3,10,6,12,5,9,0,7},
            {0,15,7,4,14,2,13,1,10,6,12,11,9,5,3,8},
            {4,1,14,8,13,6,2,11,15,12,9,7,3,10,5,0},
            {15,12,8,2,4,9,1,7,5,11,3,14,10,0,6,13}
    };
    final int[][] S2Box = {
            {15,1,8,14,6,11,3,4,9,7,2,13,12,0,5,10},
            {3,13,4,7,15,2,8,14,12,0,1,10,6,9,11,5},
            {0,14,7,11,10,4,13,1,5,8,12,6,9,3,2,15},
            {13,8,10,1,3,15,4,2,11,6,7,12,0,5,14,9}
    };
    final int[][] S3Box = {
            {10,0,9,14,6,3,15,5,1,13,12,7,11,4,2,8},
            {13,7,0,9,3,4,6,10,2,8,5,14,12,11,15,1},
            {13,6,4,9,8,15,3,0,11,1,2,12,5,10,14,7},
            {1,10,13,0,6,9,8,7,4,15,14,3,11,5,2,12},
    };
    final int[][] S4Box = {
            {7,13,14,3,0,6,9,10,1,2,8,5,11,12,4,15},
            {13,8,11,5,6,15,0,3,4,7,2,12,1,10,14,19},
            {10,6,9,0,12,11,7,13,15,1,3,14,5,2,8,4},
            {3,15,0,6,10,1,13,8,9,4,5,11,12,7,2,14}
    };
    final int[][] S5Box = {
            {2,12,4,1,7,10,11,6,5,8,3,15,13,0,14,9},
            {14,11,2,12,4,7,13,1,5,0,15,13,3,9,8,6},
            {4,2,1,11,10,13,7,8,15,9,12,5,6,3,0,14},
            {11,8,12,7,1,14,2,13,6,15,0,9,10,4,5,3}
    };
    final int[][] S6Box = {
            {12,1,10,15,9,2,6,8,0,13,3,4,14,7,5,11},
            {10,15,4,2,7,12,9,5,6,1,13,14,0,11,3,8},
            {9,14,15,5,2,8,12,3,7,0,4,10,1,13,11,6},
            {4,3,2,12,9,5,15,10,11,14,1,7,6,0,8,13}
    };
    final int[][] S7Box = {
            {4,11,2,14,15,0,8,13,3,12,9,7,5,10,6,1},
            {13,0,11,7,4,9,1,10,14,3,5,12,2,15,8,6},
            {1,4,11,13,12,3,7,14,10,15,6,8,0,5,9,2},
            {6,11,13,8,1,4,10,7,9,5,0,15,14,2,3,12}
    };
    final int[][] S8Box = {
            {13,2,8,4,6,15,11,1,10,9,3,14,5,0,12,7},
            {1,15,13,8,10,3,7,4,12,5,6,11,0,14,9,2},
            {7,11,4,1,9,12,14,2,0,6,10,13,15,3,5,8},
            {2,1,14,7,4,10,8,13,15,12,9,0,3,5,6,11}
    };
    final int[] PBox = {
            16,7,20,21,29,12,28,17,
            1,15,23,26,5,18,31,10,
            2,8,24,14,32,27,3,9,
            19,13,30,6,22,11,4,25
    };

    class Pair{
        String left;
        String right;
    }
    /**
     * 明文初始进行的IP置换
     * @param text 64位
     * @return     64位
     */
    private String IPDisplace(String text){
        char[] temp=new char[64];//置换后返回的二进制数
        for(int i = 0; i < 64 ; i++){
            temp[i] = text.charAt(IpTable[i]-1);
        }
        return String.copyValueOf(temp);
    }
    /**
     * 对密文进行Ip逆置换
     * @param text 64位
     * @return 64位
     */
    private String deIpDisplace(String text){
        char[] temp=new char[64];//置换后返回的二进制数
        for(int i = 0; i < 64 ; i++){
            temp[i] = text.charAt(deIPTable[i]-1);
        }
        return String.copyValueOf(temp);
    }
    /**
     * 初始化Pair（将明文转化为左右两侧）
     * @param text 64位的text
     * @return     Pair
     */
    private Pair initPair(String text){
        Pair pair = new Pair();
        pair.left = text.substring(0,32);
        pair.right = text.substring(32,64);
        return pair;
    }
    /**
     *
     * @param left  左侧的
     * @param right 右侧的
     * @param len   两个与或的数的长度(48或32)
     * @return      返回与或的结果
     */
    private String XOR(String left,String right,int len){
        char[] leftChar = left.toCharArray();
        char[] rightChar = right.toCharArray();
        char[] result = new char[len];
        for(int i=0;i<len;i++){
            if(leftChar[i] == rightChar[i]){
                result[i] = '0';
            }else {
                result[i] ='1';
            }
        }
        return String.copyValueOf(result);
    }
    /**
     * Ep扩展的函数
     * @param text32 传入右半部分32位二进制
     * @return      返回48位
     */
    private String EpExpand(String text32){
        char[] temp = new char[48];
        //System.out.println("111");
        for(int i = 0; i < 48 ; i++){
            temp[i] = text32.charAt(EpTable[i]-1);
        }
        //System.out.println(temp);
        return String.copyValueOf(temp);
    }
    /**
     * S盒子代替
     * @param text48 48位
     * @return 32位的code
     */
    private String sBoxHandler(String text48){
        String text32="";
        for(int i = 0; i < 8 ; i++){
            String temp = text48.substring(i*6,i*6+6);
            String lineStr = ""+temp.charAt(0)+temp.charAt(5);
            String rowStr  = ""+temp.substring(1,5);
            int line = Integer.parseInt(lineStr,2);
            int row = Integer.parseInt(rowStr,2);
            switch (i){
                case 0:
                {
                    int sNum = S1Box[line][row];
                    String sNumCode = Integer.toBinaryString(sNum);
                    while(sNumCode.length()<4){
                        sNumCode = "0" + sNumCode;
                    }
                    text32 = text32 + sNumCode;
                }
                case 1:
                {
                    int sNum = S2Box[line][row];
                    String sNumCode = Integer.toBinaryString(sNum);
                    while(sNumCode.length()<4){
                        sNumCode = "0" + sNumCode;
                    }
                    text32 = text32 + sNumCode;
                }
                case 2:
                {
                    int sNum = S3Box[line][row];
                    String sNumCode = Integer.toBinaryString(sNum);
                    while(sNumCode.length()<4){
                        sNumCode = "0" + sNumCode;
                    }
                    text32 = text32 + sNumCode;
                }
                case 3:
                {
                    int sNum = S4Box[line][row];
                    String sNumCode = Integer.toBinaryString(sNum);
                    while(sNumCode.length()<4){
                        sNumCode = "0" + sNumCode;
                    }
                    text32 = text32 + sNumCode;
                }
                case 4:
                {
                    int sNum = S5Box[line][row];
                    String sNumCode = Integer.toBinaryString(sNum);
                    while(sNumCode.length()<4){
                        sNumCode = "0" + sNumCode;
                    }
                    text32 = text32 + sNumCode;
                }
                case 5:
                {
                    int sNum = S6Box[line][row];
                    String sNumCode = Integer.toBinaryString(sNum);
                    while(sNumCode.length()<4){
                        sNumCode = "0" + sNumCode;
                    }
                    text32 = text32 + sNumCode;
                }
                case 6:
                {
                    int sNum = S7Box[line][row];
                    String sNumCode = Integer.toBinaryString(sNum);
                    while(sNumCode.length()<4){
                        sNumCode = "0" + sNumCode;
                    }
                    text32 = text32 + sNumCode;
                }
                case 7:
                {
                    int sNum = S8Box[line][row];
                    String sNumCode = Integer.toBinaryString(sNum);
                    while(sNumCode.length()<4){
                        sNumCode = "0" + sNumCode;
                    }
                    text32 = text32 + sNumCode;
                }
            }
        }
        return text32;
    }

    private String pBoxHandler(String text32){
        char[] temp32 = new char[32];
        for(int i = 0 ; i < 32 ; i++){
            temp32[i] = text32.charAt(PBox[i]-1);
        }
        return String.copyValueOf(temp32);
    }

    /**
     * 加密用的方法
     * 参数keys作为密钥
     * 参数plainText是需要加密的明文（64位的字节）
     */
    public String DESCipher(String keys,String plainText){
        String cipherText="";
        cipherText = IPDisplace(plainText);
        Key key = new Key(keys);
        ArrayList<String> keysList = key.getKeys();
        Iterator keyIterator = keysList.iterator();
        Pair pair = initPair(cipherText);
        for (int i =0 ;i<16;i++){
            String temp = "";
            temp = EpExpand(pair.right);
            temp = XOR(temp,(String) keyIterator.next(),48);
            temp = sBoxHandler(temp);
            temp = pBoxHandler(temp);
            temp = XOR(pair.left,temp,32);
            pair.left = pair.right;
            pair.right = temp;
        }
        cipherText = deIpDisplace(pair.right + pair.left);

        return cipherText;
    }

    /**
     * 解密用的方法
     * @param keys          密钥
     * @param cipherText    密文
     * @return              返回值是明文二进制字符串
     */
    public String DESDecipher(String keys,String cipherText){
        String plainText="";
        plainText = IPDisplace(cipherText);
        Key key = new Key(keys);
        ArrayList<String> keysList = key.getKeys();
        Collections.reverse(keysList);
        Iterator<String> keyIterator = keysList.iterator();
        Pair pair = initPair(plainText);
        //System.out.println("111111111111a");
        for (int i =0 ;i<16;i++){
            //System.out.println("pair.right:"+pair.right+" "+pair.right.length());
            String temp = "";
            temp = EpExpand(pair.right);
            //System.out.println("temp:"+temp+" "+temp.length());

            temp = XOR(temp, keyIterator.next(),48);
            //System.out.println("temp:"+temp+" "+temp.length());
            temp = sBoxHandler(temp);
            temp = pBoxHandler(temp);
            temp = XOR(pair.left,temp,32);
            //System.out.println("111111111111d");
            pair.left = pair.right;
            pair.right = temp;
        }
        cipherText = deIpDisplace(pair.right + pair.left);

        return cipherText;
    }
    public static void main(String[] args) {
        try {

            DES des = new DES();
            String key;
            Scanner scanner = new Scanner(System.in);
            //Pattern pattern = Pattern.compile("[a-f][0-9]+");
            System.out.println("输入发送的信息");
            String text = scanner.next();
            StringByte stringByte = new StringByte();
            ArrayList<String> plainText = stringByte.StringToBinary(text);
            ArrayList<String> cipherText = new ArrayList<>();
            System.out.println("输入密钥");
            key = scanner.next();
            Iterator<String> iterator1 = plainText.iterator();
            while (iterator1.hasNext()) {
                String aaa = iterator1.next();
                cipherText.add(des.DESCipher(key, aaa));
            }
            String cipher = stringByte.BinaryToString(cipherText);
            System.out.println("加密完成 密文："+stringByte.BinaryToString(cipherText));
            System.out.println("开始解密");

            ArrayList<String> cipherText1 = new ArrayList<>();
            cipherText1 = stringByte.StringToBinary(cipher);
            Iterator<String> iterator2 = cipherText1.iterator();
            ArrayList<String> test = new ArrayList<>();
            while (iterator2.hasNext()) {
                String aaa = iterator2.next();
                test.add(des.DESDecipher(key, aaa));
            }
            System.out.println("解密完成 明文："+stringByte.BinaryToString(test));
        }catch (Exception e){
            e.getMessage();
        }
    }

}