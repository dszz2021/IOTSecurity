package test;

import com.google.gson.Gson;
import io.netty.bootstrap.Bootstrap;
import io.netty.channel.Channel;
import io.netty.channel.ChannelInitializer;
import io.netty.channel.ChannelPipeline;
import io.netty.channel.EventLoopGroup;
import io.netty.channel.nio.NioEventLoopGroup;
import io.netty.channel.socket.SocketChannel;
import io.netty.channel.socket.nio.NioSocketChannel;
import io.netty.handler.codec.DelimiterBasedFrameDecoder;
import io.netty.handler.codec.Delimiters;
import io.netty.handler.codec.string.StringDecoder;
import io.netty.handler.codec.string.StringEncoder;
import io.netty.util.CharsetUtil;

import java.io.BufferedReader;
import java.io.InputStreamReader;

public class Client {
    public static void main(String[] args) throws Exception{
        EventLoopGroup eventLoopGroup = new NioEventLoopGroup();
        try{
            Bootstrap bootstrap = new Bootstrap();
            bootstrap.group(eventLoopGroup).channel(NioSocketChannel.class)
                    .handler(new ChannelInitializer<SocketChannel>() {
                        @Override
                        protected void initChannel(SocketChannel socketChannel) throws Exception {
                            ChannelPipeline pipeline = socketChannel.pipeline();

                            pipeline.addLast(new DelimiterBasedFrameDecoder(4096, Delimiters.lineDelimiter()));
                            pipeline.addLast(new StringDecoder(CharsetUtil.UTF_8));
                            pipeline.addLast(new StringEncoder(CharsetUtil.UTF_8));
                            pipeline.addLast(new MyChatClientHandler());

                        }
                    });

            Channel channel = bootstrap.connect("localhost",8080).sync().channel();

            //标准输入
            BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(System.in));

            Gson gson = new Gson();
            //利用死循环，不断读取客户端在控制台上的输入内容
            for (;;){
               /* System.out.println("输入姓名");
                String name = bufferedReader.readLine();
                System.out.println("输入年龄");
                int age = Integer.parseInt(bufferedReader.readLine());
                ArrayList<String> friend = new ArrayList<>();
                friend.add("张三");
                friend.add("李四");
                friend.add("王二麻子");
                friend.add("六六五");
                //Message1 message1 = new Message1(name,age,friend);
                //String json = gson.toJson(message1);
                System.out.println(json);
                channel.writeAndFlush(json+"\r\n");*/
                String s = bufferedReader.readLine();
                channel.writeAndFlush(s+"\r\n");
            }

        }finally {
            eventLoopGroup.shutdownGracefully();
        }
    }
}