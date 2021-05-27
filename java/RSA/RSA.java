package RSA;

import java.math.BigInteger;
import java.util.Random;

public class RSA {
    /**
     * 用于加密
     * @param text       需要加密的明文字符串
     * @param publicKey  公钥
     * @param n          公钥n
     * @return           返回加密后的密文字符串
     */
    public String encipher(String text, BigInteger publicKey, BigInteger n){
        StringBuilder string = new StringBuilder();
        char[] plainText = text.toCharArray();
        for(int i =0;i<plainText.length;i++){
            BigInteger mText = BigInteger.valueOf(text.charAt(i));
            BigInteger cText = mText.modPow(publicKey,n);
            String plusZero = "";
            int bit = 0;
            //指定位数
            while(bit<n.toString().length()-cText.toString().length()){
                plusZero +="0";
                bit++;
            }
            string.append(plusZero).append(cText);
        }
        return string.toString();
    }

    /**
     * 用于解密
     * @param text          需要解密的密文字符串
     * @param privateKey    私钥
     * @param n             私钥n
     * @return              返回解密后的明文字符串
     */
    public String decipher(String text,BigInteger privateKey,BigInteger n){
        StringBuilder string= new StringBuilder();
        for(int i = 0 ;i<text.length()/n.toString().length();i++){
            String tempText = text.substring(i*n.toString().length(),(i+1)*n.toString().length());
            BigInteger cText =BigInteger.valueOf(Long.parseLong(tempText));
            BigInteger mText = cText.modPow(privateKey,n);
            string.append((char) mText.intValue());
        }
        return string.toString();
    }

    public static void main(String[] args) {
        GetKey getKey = new GetKey();
        RSA rsa = new RSA();
        while (!getKey.setKey(BigInteger.probablePrime(10,new Random()),BigInteger.probablePrime(10,new Random()))){
            System.out.println("请使用质数p和质数q");
        }
        PublicKeyPair publicKeyPair = getKey.getPublicKeyPair();
        PrivateKeyPair privateKeyPair = getKey.getPrivateKeyPair();
        String cText = rsa.encipher("可以吗?",publicKeyPair.getPublicKey(),publicKeyPair.getN());
        System.out.println("密文："+ cText);
        String mText = rsa.decipher(cText,privateKeyPair.getPrivateKey(),privateKeyPair.getN());
        System.out.println("明文："+ mText);
    }
}