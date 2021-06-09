using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            DES des = new DES();
            String cipher = des.cipher("caotamad个比的妈卖批", "密钥");
           Console.WriteLine(cipher);
            String deciper = des.deCipher(cipher, "密钥");
            Console.WriteLine(deciper);

            /*  DES dES = new DES();
              string cipher = dES.cipher("123465","123");
              Console.WriteLine("  cipher: " + cipher);
              string decipher = dES.deCipher(cipher,"123");
              Console.WriteLine("decipher: " + decipher);
  */

            /*   ArrayList array = new ArrayList();
               array.Add("123");
               array.Add("456");
               array.Add("789");
               array.Add("012");
               foreach(String s in array)
               {
                   Console.WriteLine(s);
               }
               Console.WriteLine(" ");
               array.Reverse();
               foreach (String s in array)
               {
                   Console.WriteLine(s);
               }*/

            /*          StringBuilder s = new StringBuilder();
                      s.Append("1111");
                      s.Append("bbbb");
                      Console.WriteLine(s.ToString());*/
            /*string aaa = "abcd";
            char[] a = { 'a', 'b', 'c', 'd' };
            Console.WriteLine(aaa.ToCharArray()[1]);
            Console.WriteLine(string.Join("", a));*/
            /*    string a = Convert.ToString(9, 2);
                while (a.Length != 8)
                {
                    a = "0" + a;
                }
                Console.WriteLine(a);
                int ax = Convert.ToInt32(a, 2);
                Console.WriteLine(ax);*/
        }
    }

    class DES
    {
        public int[] EpTable = {
            32,1,2,3,4,5,
            4,5,6,7,8,9,
            8,9,10,11,12,13,
            12,13,14,15,16,17,
            16,17,18,19,20,21,
            20,21,22,23,24,25,
            24,25,26,27,28,29,
            28,29,30,31,32,1
         };
        public int[] IpTable = {
            58,50,42,34,26,18,10,2,
            60,52,44,36,28,20,12,4,
            62,54,46,38,30,22,14,6,
            64,56,48,40,32,24,16,8,
            57,49,41,33,25,17,9,1,
            59,51,43,35,27,19,11,3,
            61,53,45,37,29,21,13,5,
            63,55,47,39,31,23,15,7};
        public int[] deIPTable = {
            40,8,48,16,56,24,64,32,
            39,7,47,15,55,23,63,31,
            38,6,46,14,54,22,62,30,
            37,5,45,13,53,21,61,29,
            36,4,44,12,52,20,60,28,
            35,3,43,11,51,19,59,27,
            34,2,42,10,50,18,58,26,
            33,1,41,9,49,17,57,25
                             };
        public int[,] S1Box = new int[4, 16] {
            {14,4,13,1,2,15,11,8,3,10,6,12,5,9,0,7},
            {0,15,7,4,14,2,13,1,10,6,12,11,9,5,3,8},
            {4,1,14,8,13,6,2,11,15,12,9,7,3,10,5,0},
            {15,12,8,2,4,9,1,7,5,11,3,14,10,0,6,13}
            };
        public int[,] S2Box = new int[4, 16]{
            {15,1,8,14,6,11,3,4,9,7,2,13,12,0,5,10},
            {3,13,4,7,15,2,8,14,12,0,1,10,6,9,11,5},
            {0,14,7,11,10,4,13,1,5,8,12,6,9,3,2,15},
            {13,8,10,1,3,15,4,2,11,6,7,12,0,5,14,9}
            };
        public int[,] S3Box = new int[4, 16]{
            {10,0,9,14,6,3,15,5,1,13,12,7,11,4,2,8},
            {13,7,0,9,3,4,6,10,2,8,5,14,12,11,15,1},
            {13,6,4,9,8,15,3,0,11,1,2,12,5,10,14,7},
            {1,10,13,0,6,9,8,7,4,15,14,3,11,5,2,12},
            };
        public int[,] S4Box = new int[4, 16]{
            {7,13,14,3,0,6,9,10,1,2,8,5,11,12,4,15},
            {13,8,11,5,6,15,0,3,4,7,2,12,1,10,14,19},
            {10,6,9,0,12,11,7,13,15,1,3,14,5,2,8,4},
            {3,15,0,6,10,1,13,8,9,4,5,11,12,7,2,14}
            };
        public int[,] S5Box = new int[4, 16]{
            {2,12,4,1,7,10,11,6,5,8,3,15,13,0,14,9},
            {14,11,2,12,4,7,13,1,5,0,15,13,3,9,8,6},
            {4,2,1,11,10,13,7,8,15,9,12,5,6,3,0,14},
            {11,8,12,7,1,14,2,13,6,15,0,9,10,4,5,3}
            };
        public int[,] S6Box = new int[4, 16]{
            {12,1,10,15,9,2,6,8,0,13,3,4,14,7,5,11},
            {10,15,4,2,7,12,9,5,6,1,13,14,0,11,3,8},
            {9,14,15,5,2,8,12,3,7,0,4,10,1,13,11,6},
            {4,3,2,12,9,5,15,10,11,14,1,7,6,0,8,13}
            };
        public int[,] S7Box = new int[4, 16] {
            {4,11,2,14,15,0,8,13,3,12,9,7,5,10,6,1},
            {13,0,11,7,4,9,1,10,14,3,5,12,2,15,8,6},
            {1,4,11,13,12,3,7,14,10,15,6,8,0,5,9,2},
            {6,11,13,8,1,4,10,7,9,5,0,15,14,2,3,12}
            };
        public int[,] S8Box = new int[4, 16]{
            {13,2,8,4,6,15,11,1,10,9,3,14,5,0,12,7},
            {1,15,13,8,10,3,7,4,12,5,6,11,0,14,9,2},
            {7,11,4,1,9,12,14,2,0,6,10,13,15,3,5,8},
            {2,1,14,7,4,10,8,13,15,12,9,0,3,5,6,11}
            };
        public int[] PBox = {
            16,7,20,21,29,12,28,17,
            1,15,23,26,5,18,31,10,
            2,8,24,14,32,27,3,9,
            19,13,30,6,22,11,4,25
            };

        class DesPair
        {
            public String left;
            public String right;
        }
        /**
         * 明文初始进行的IP置换
         * @param text 64位
         * @return     64位
         */
        private String IPDisplace(String text)
        {
            char[] temp = new char[64];//置换后返回的二进制数
            for (int i = 0; i < 64; i++)
            {
                temp[i] = text[IpTable[i] - 1];
            }
            return string.Join("", temp);
        }
        /**
         * 对密文进行Ip逆置换
         * @param text 64位
         * @return 64位
         */
        private String deIpDisplace(String text)
        {
            char[] temp = new char[64];//置换后返回的二进制数
            for (int i = 0; i < 64; i++)
            {
                temp[i] = text[deIPTable[i] - 1];
            }
            return string.Join("", temp);
        }
        /**
         * 初始化Pair（将明文转化为左右两侧）
         * @param text 64位的text
         * @return     Pair
         */
        private DesPair initPair(String text)
        {
            DesPair pair = new DesPair();
            pair.left = text.Substring(0, 32);
            pair.right = text.Substring(32,32);
            return pair;
        }
        /**
         *
         * @param left  左侧的
         * @param right 右侧的
         * @param len   两个与或的数的长度(48或32)
         * @return      返回与或的结果
         */
        private String XOR(String left, String right, int len)
        {
            //char[] leftChar = left.ToCharArray();
            //char[] rightChar = right.ToCharArray();
            char[] result = new char[len];
            for (int i = 0; i < len; i++)
            {
                if (left[i] == right[i])
                {
                    result[i] = '0';
                }
                else
                {
                    result[i] = '1';
                }
            }
            //int a = Convert.ToInt32(left, 2);
            //int b = Convert.ToInt32(right, 2);
           // string bs = Convert.ToString(a ^ b, 2);
           // string abs= string.Join("", result);
            return string.Join("", result);
        }
        /**
         * Ep扩展的函数
         * @param text32 传入右半部分32位二进制
         * @return      返回48位
         */
        private String EpExpand(String text32)
        {
            char[] temp = new char[48];
            //System.out.println("111");
            for (int i = 0; i < 48; i++)
            {
                int test = EpTable[i] - 1;
                temp[i] = text32[EpTable[i] - 1];
            }
            //System.out.println(temp);
            return string.Join("", temp);
        }
        /**
         * S盒子代替
         * @param text48 48位
         * @return 32位的code
         */
        private String sBoxHandler(String text48)
        {
            String text32 = "";
            for (int i = 0; i < 8; i++)
            {
                String temp = text48.Substring(i * 6, 6);
                String lineStr = "" + temp[0] + temp[5];
                String rowStr = "" + temp.Substring(1, 4);
                int line = System.Convert.ToInt32(lineStr, 2);
                int row = System.Convert.ToInt32(rowStr, 2);
                switch (i)
                {
                    case 0:
                        {
                            int sNum = S1Box[line, row];
                            String sNumCode = System.Convert.ToString(sNum, 2);
                            while (sNumCode.Length < 4)
                            {
                                sNumCode = "0" + sNumCode;
                            }
                            text32 = text32 + sNumCode;
                            break;
                        }
                    case 1:
                        {
                            int sNum = S2Box[line, row];
                            String sNumCode = System.Convert.ToString(sNum, 2);
                            while (sNumCode.Length < 4)
                            {
                                sNumCode = "0" + sNumCode;
                            }
                            text32 = text32 + sNumCode;
                            break;
                        }
                    case 2:
                        {
                            int sNum = S3Box[line, row];
                            String sNumCode = System.Convert.ToString(sNum, 2);
                            while (sNumCode.Length < 4)
                            {
                                sNumCode = "0" + sNumCode;
                            }
                            text32 = text32 + sNumCode;
                            break;
                        }
                    case 3:
                        {
                            int sNum = S4Box[line, row];
                            String sNumCode = System.Convert.ToString(sNum, 2);
                            while (sNumCode.Length < 4)
                            {
                                sNumCode = "0" + sNumCode;
                            }
                            text32 = text32 + sNumCode;
                            break;
                        }
                    case 4:
                        {
                            int sNum = S5Box[line, row];
                            String sNumCode = System.Convert.ToString(sNum, 2);
                            while (sNumCode.Length < 4)
                            {
                                sNumCode = "0" + sNumCode;
                            }
                            text32 = text32 + sNumCode;
                            break;
                        }
                    case 5:
                        {
                            int sNum = S6Box[line, row];
                            String sNumCode = System.Convert.ToString(sNum, 2);
                            while (sNumCode.Length < 4)
                            {
                                sNumCode = "0" + sNumCode;
                            }
                            text32 = text32 + sNumCode;
                            break;
                        }
                    case 6:
                        {
                            int sNum = S7Box[line, row];
                            String sNumCode = System.Convert.ToString(sNum, 2);
                            while (sNumCode.Length < 4)
                            {
                                sNumCode = "0" + sNumCode;
                            }
                            text32 = text32 + sNumCode;
                            break;
                        }
                    case 7:
                        {
                            int sNum = S8Box[line, row];
                            String sNumCode = System.Convert.ToString(sNum, 2);
                            while (sNumCode.Length < 4)
                            {
                                sNumCode = "0" + sNumCode;
                            }
                            text32 = text32 + sNumCode;
                            break;
                        }
                }
            }
            return text32;
        }

        private String pBoxHandler(String text32)
        {
            char[] temp32 = new char[32];
            for (int i = 0; i < 32; i++)
            {
                temp32[i] = text32[PBox[i] - 1];
            }
            return string.Join("", temp32);
        }

        /**
         * 加密用的方法
         * 参数keys作为密钥
         * 参数plainText是需要加密的明文（64位的字节）
         */
        private String DESCipher(String keys, String plainText)
        {
            String cipherText = "";
            cipherText = IPDisplace(plainText);
            Key key = new Key(keys);
            List<string> arrayList = new List<string>();
            List<string> keysList = key.getKeys();
            DesPair pair = initPair(cipherText);
            foreach(string key1 in keysList)
            {
                String temp = "";
                int x = pair.right.Length;
                temp = EpExpand(pair.right);
                temp = XOR(temp, key1, 48);
                temp = sBoxHandler(temp);
                temp = pBoxHandler(temp);
                temp = XOR(pair.left, temp, 32);
                pair.left = pair.right;
                pair.right = temp;
            }

            /*for (int i = 0; i < 16; i++)
            {
                String temp = "";
                temp = EpExpand(pair.right);
                temp = XOR(temp, (String)keysList[i], 48);
                temp = sBoxHandler(temp);
                temp = pBoxHandler(temp);
                temp = XOR(pair.left, temp, 32);
                pair.left = pair.right;
                pair.right = temp;
            }*/
            cipherText = deIpDisplace(pair.right + pair.left);

            return cipherText;
        }

        /**
         * 解密用的方法
         * @param keys          密钥
         * @param cipherText    密文
         * @return              返回值是明文二进制字符串
         */
        private String DESDecipher(String keys, String cipherText)
        {
            String plainText = "";
            plainText = IPDisplace(cipherText);
            Key key = new Key(keys);
            List<string> keysList = key.getKeys();
            keysList.Reverse();
            DesPair pair = initPair(plainText);
            //System.out.println("111111111111a");
            foreach (string key1 in keysList)
            {
                String temp = "";
                temp = EpExpand(pair.right);
                temp = XOR(temp, key1, 48);
                temp = sBoxHandler(temp);
                temp = pBoxHandler(temp);
                temp = XOR(pair.left, temp, 32);
                pair.left = pair.right;
                pair.right = temp;
            }



/*
            for (int i = 0; i < 16; i++)
            {
                //System.out.println("pair.right:"+pair.right+" "+pair.right.length());
                String temp = "";
                temp = EpExpand(pair.right);
                //System.out.println("temp:"+temp+" "+temp.length());

                temp = XOR(temp, (String)keysList[i], 48);
                //System.out.println("temp:"+temp+" "+temp.length());
                temp = sBoxHandler(temp);
                temp = pBoxHandler(temp);
                temp = XOR(pair.left, temp, 32);
                //System.out.println("111111111111d");
                pair.left = pair.right;
                pair.right = temp;
            }*/
            cipherText = deIpDisplace(pair.right + pair.left);

            return cipherText;
        }

        public String cipher(String text, String key)
        {
            String text1 = text;
            StringByte stringByte = new StringByte();
            List<string> plainText = stringByte.StringToBinary(text);
            List<string> cipherText = new List<string>();

            String key1 = key;

            //Iterator<String> iterator1 = plainText.iterator();
            foreach (String aaa in plainText)
            {
                cipherText.Add(this.DESCipher(key, aaa));
            }
            /* while (iterator1.hasNext())
             {
                 String aaa = iterator1.next();
                 cipherText.add(this.DESCipher(key, aaa));
             }*/
            String cipher = stringByte.BinaryToString(cipherText);

       /*     string test="";
            foreach(string a in cipherText)
            {
                test = test + a;
            }*/
            //System.out.println("加密完成 密文："+stringByte.BinaryToString(cipherText));
            return stringByte.BinaryToString(cipherText);
           // return test;


        }

        public String deCipher(String text, String key)
        {
            StringByte stringByte = new StringByte();
            List<string> cipherText1 = new List<string>();
           /* string temp = text;
            while (temp.Length >= 64)
            {
                string a = temp.Substring(0, 64);
                cipherText1.Add(a);
                temp = temp.Substring(64);
            }*/
            cipherText1 = stringByte.StringToBinary(text);
            List<string> test = new List<string>();
            foreach (String aaa in cipherText1)
            {
                test.Add(this.DESDecipher(key, aaa));
            }
            /*while (iterator2.hasNext())
            {
                String aaa = iterator2.next();
                test.add(this.DESDecipher(key, aaa));
            }*/
            //System.out.println("解密完成 明文："+stringByte.BinaryToString(test));
            return stringByte.BinaryToString(test);
        }

    }

    class Key
    {
        class Pair
        {
            public String leftKey;//28位
            public String rightKey;//28位
        }
        String key;//存储56位的二进制编码
        int[] switchTalbe = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };//左移次数规定的表
        int[] PCTable = {14,17,11,24,1,5,3,28,
                           15,6,21,10,23,19,12,4,
                            26,8,16,7,27,20,13,2,
                            41,52,31,37,47,55,30,40,
                            51,45,33,48,44,49,39,56,
                            34,53,46,42,50,36,29,32};//密钥用的置换PC表
        private List<string> keys;//用来保存每一层的密码
        public Key(String key)
        {
            this.key = stringToKey56(key);
            keys = new List<string>();
        }


        private String stringToKey56(String keyString)
        {
            char[] keyChar = keyString.ToCharArray();
            StringBuilder key56 = new StringBuilder();
            int num = 0;
            while (key56.Length < 56)
            {
                key56.Append(System.Convert.ToString(keyChar[num], 2));
                num++;
                if (num == keyChar.Length)
                {
                    num = 0;
                }
            }
            string aaa = key56.ToString();
            return aaa.Substring(0, 56);
        }
        /**
         *  用于循环移位的函数
         * @param keyPair   被移位的key(左和右)
         * @param round 轮次，确定移位的位数,该参数从0开始传入
         * @return      返回移位之后的左右两个key
         */
        private Pair switchFunc(Pair keyPair, int round)
        {
            char temp1, temp2;
            int switchNum = switchTalbe[round];
            //先移左边
            char[] left = keyPair.leftKey.ToCharArray();
            temp1 = left[0];
            temp2 = left[1];
            for (int i = 0; i < 28 - switchNum; i++)
            {
                left[i] = left[i + switchNum];
            }
            if (switchNum == 1)
            {
                left[27] = temp1;
            }
            else if (switchNum == 2)
            {
                left[26] = temp1;
                left[27] = temp2;
            }
            keyPair.leftKey = new string(left);

            //再移动右边
            char[] right = keyPair.rightKey.ToCharArray();
            temp1 = right[0];
            temp2 = right[1];
            for (int i = 0; i < 28 - switchNum; i++)
            {
                right[i] = left[i + switchNum];
            }
            if (switchNum == 1)
            {
                right[27] = temp1;
            }
            else if (switchNum == 2)
            {
                right[26] = temp1;
                right[27] = temp2;
            }
            keyPair.rightKey = new string(right);
            return keyPair;
        }

        /**
         *
         * @param keyPair
         * @return 某一轮次的48位key
         */
        private String getKey(Pair keyPair)
        {
            String key56 = keyPair.leftKey + keyPair.rightKey;
            char[] key48 = new char[48];
            for (int i = 0; i < 48; i++)
            {
                key48[i] = key56[PCTable[i] - 1];
            }
            return new string(key48);
        }

        public List<string> getKeys()
        {
            //首先初始化Pair
            Pair keyPair = new Pair();
            keyPair.leftKey = key.Substring(0, 28);
            keyPair.rightKey = key.Substring(28, 28);

            //循环得到key48
            for (int i = 0; i < 16; i++)
            {
                String temKey;
                keyPair = switchFunc(keyPair, i);
                temKey = getKey(keyPair);
                keys.Add(temKey);
            }
            return keys;
        }
        /*

                public static void main(String[] args)
                {
                    Key key = new Key("1234567890abcd");
                    ArrayList keys = new ArrayList();
                    keys = key.getKeys();
                    Iterator iterator = keys.iterator();
                    while (iterator.hasNext())
                    {
                        String a = (String)iterator.next();
                        System.out.println(a.length() + "  " + a);
                    }
                }*/
    }


    class StringByte
    {
        /**
         *  该函数将明文转化为二进制字符串，并且64位为一组不足64位的高位补零
         *  模块输入为一串明文字符串text
         *  输出为该字符串对应的64位一组的多组二进制字符串
         */
        public List<string> StringToBinary(String text)
        {
            List<string> binaryCode = new List<string>();
            char[] chars = text.ToCharArray();
            String code = "";
            int i = 0;
            foreach (char a in chars)
            {
                i++;//记录循环次数，每四次循环能凑够一个64位二进制存入arraylist中；
                    //将字符转化为二进制字符串
                String charCode = System.Convert.ToString(a, 2);
                //若二进制字符串不满16位则在高位用0补齐
                if (charCode.Length != 16)
                {
                    int num = 16 - charCode.Length;
                    for (int j = 0; j < num; j++)
                    {
                        charCode = "0" + charCode;
                    }
                }
                code += charCode;
                if (i == 4)
                {
                    //将i置零
                    i = 0;
                    binaryCode.Add(code);
                    code = "";
                }
            }
            //结束循环后，若最后一轮不足64位则高位补零
            if (code != "")
            {
                int num = 64 - code.Length;
                for (int j = 0; j < num; j++)
                {
                    code = "0" + code;
                }
                binaryCode.Add(code);
            }

            return binaryCode;
        }

        /**
         *
         * @param binaryCode 解码之后得到的二进制字符串
         * @return 返回值是该组二进制字符串所代表的明文
         */
        public String BinaryToString(List<string> binaryCode)
        {
            String meanLessText = "0000000000000000";
            String text = "";
            //Iterator<String> iterator = binaryCode.iterator();
            foreach (string code in binaryCode)
            {
                String code1 = code;//code中位64位二进制数
                                    //切成16位的二进制转化为char并将其赋给text；
                while (code1.Length >= 16)
                {
                    String temp = code1.Substring(0, 16);
                    if (!string.Equals(temp, meanLessText))
                    {
                        int intCode = System.Convert.ToInt32(temp, 2);
                        text += (char)intCode;
                    }
                    code1 = code1.Substring(16);
                }
            }
            return text;
        }

        /**
         *这个主函数仅用于测试使用
         */
        /*public static void main(String[] args)
        {
            StringByte stringByte = new StringByte();
            System.out.println(stringByte.BinaryToString(stringByte.StringToBinary("阿巴阿巴阿巴")));
        }*/
    }



}
