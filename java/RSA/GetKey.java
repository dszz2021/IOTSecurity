package RSA;

import java.math.BigInteger;
import java.util.Random;

class PublicKeyPair{
    BigInteger publicKey;
    BigInteger n;

    public BigInteger getPublicKey() {
        return publicKey;
    }

    public BigInteger getN(){
        return n;
    }

    public void setPublicKeyPair(BigInteger publicKey,BigInteger n) {
        this.publicKey = publicKey;
        this.n = n;
    }
}

class PrivateKeyPair{
    BigInteger privateKey;
    BigInteger n;

    public BigInteger getPrivateKey() {
        return privateKey;
    }

    public BigInteger getN(){
        return n;
    }

    public void setPublicKeyPair(BigInteger privateKey,BigInteger n) {
        this.privateKey = privateKey;
        this.n = n;
    }
}

public class GetKey {
    BigInteger publicKey ;
    BigInteger privateKey;
    BigInteger n;

    private boolean isPrimeNumber(BigInteger num){
        boolean flag = true;
        for(int i=2;i<Math.sqrt(num.longValue());i++){
            if(num.mod(BigInteger.valueOf(i)).compareTo(BigInteger.ZERO)==0){
                flag = false;
                break;
            }
        }
        return flag;
    }


    public boolean setKey(BigInteger p,BigInteger q){
        //p = BigInteger.probablePrime(10,new Random());
        //q = BigInteger.probablePrime(10,new Random());
        //判断一下 是否素数 while
        if((p.compareTo(q)==0)||(!(isPrimeNumber(p)&&isPrimeNumber(q))))
        {
            //其中有一个不是素数或p=q
            return false;
        }
        n = p.multiply(q);//n和q相乘
        BigInteger faiN = p.subtract(BigInteger.ONE).multiply( q.subtract(BigInteger.ONE) );//计算(p-1)*(q-1);

        while(true){
            long i = (long) (Math.random()*(faiN.longValue()-2L)+ 2L);
            if(faiN.gcd(BigInteger.valueOf(i)).compareTo(BigInteger.ONE)==0){
                publicKey = new BigInteger(String.valueOf(i));
                break;
            }
        }

        privateKey = BigInteger.valueOf(publicKey.modInverse(faiN).longValue());//得到私钥
        System.out.println("密钥 n = "+n.toString()+" 公钥："+publicKey.longValue()+"   私钥:" + privateKey.longValue());
        return true;
    }


    public PrivateKeyPair getPrivateKeyPair() {
        PrivateKeyPair privateKeyPair = new PrivateKeyPair();
        privateKeyPair.setPublicKeyPair(privateKey,n);
        return privateKeyPair;
    }

    public PublicKeyPair getPublicKeyPair() {
        PublicKeyPair publicKeyPair = new PublicKeyPair();
        publicKeyPair.setPublicKeyPair(publicKey,n);
        return publicKeyPair;
    }
}