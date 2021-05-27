package RSA;

import java.math.BigInteger;

public class test {
    public static void main(String[] args) {
        BigInteger test;
        int test1 = BigInteger.valueOf(187).toString().length();
        BigInteger p = BigInteger.valueOf(13);
        BigInteger q = BigInteger.valueOf(11);
        BigInteger faiN = p.subtract(BigInteger.valueOf(1)).multiply( q.subtract(BigInteger.valueOf(1)) );
        //int test1 = test.compareTo(BigInteger.ZERO);
        System.out.println(test1);
    }
}
