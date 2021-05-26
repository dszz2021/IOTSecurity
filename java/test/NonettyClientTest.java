package test;

import java.io.DataInputStream;
import java.net.InetAddress;
import java.net.Socket;

public class NonettyClientTest {
    public static void main(String[] args) throws Exception {
        int port = 8899;
        byte ipAddressTemp[]={127,0,0,1};
        InetAddress ip = InetAddress.getByAddress(ipAddressTemp);
        Socket socket = new Socket(ip,port);
        while (true) {
            DataInputStream input = new DataInputStream(socket.getInputStream());
            byte[] buf = new byte[256];
            int count = input.read(buf);
            System.out.println("server:" + new String(buf));
        }

    }
}
