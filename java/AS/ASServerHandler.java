package AS;

import Message.BodyA.BodyA1;
import Message.Message;
import com.google.gson.Gson;
import io.netty.channel.Channel;
import io.netty.channel.ChannelHandlerContext;
import io.netty.channel.SimpleChannelInboundHandler;
import io.netty.channel.group.ChannelGroup;
import io.netty.channel.group.DefaultChannelGroup;
import io.netty.util.concurrent.GlobalEventExecutor;

import java.util.HashMap;
import java.util.Map;

public class ASServerHandler extends SimpleChannelInboundHandler<String>{

    //保留所有与服务器建立连接的channel对象，这边的GlobalEventExecutor在写博客的时候解释一下，看其doc
    //private static ChannelGroup channelGroup = new DefaultChannelGroup(GlobalEventExecutor.INSTANCE);
    //Map<String,ChannelGroup> channelGroupMap = new HashMap<>();

    /**
     * 服务器端收到任何一个客户端的消息都会触发这个方法
     */
    @Override
    protected void channelRead0(ChannelHandlerContext ctx, String msg) throws Exception {
        /*Channel channel = ctx.channel();

        channelGroup.forEach(ch -> {
            if(channel !=ch){
                ch.writeAndFlush(channel.remoteAddress() +" 发送的消息:" +msg+" \n");
            }else{
                ch.writeAndFlush(" 【自己】"+msg +" \n");
            }
        });*/
        System.out.println(msg);

        Channel channel = ctx.channel();
        Gson gson = new Gson();
        Message message1 = gson.fromJson(msg,Message.class);
        if(message1.getHead().getThickType()!=0xa){
            /*
            生成相应错误码的报文，并发回。
            */
        }else {

            /*
            调用AS认证部分的函数（苏方）(该函数考虑并发线程安全)
            输入: bodyA1
            输出： 一段json字符串 aaa。*/
            BodyA1 bodyA1 = gson.fromJson(message1.getBody(),BodyA1.class);
            AsIdentify Identify = new AsIdentify();
            String messageBack = Identify.AsDoIdentify(bodyA1);




            channel.writeAndFlush(messageBack+"\n");

        }

    }

    @Override
    public void handlerRemoved(ChannelHandlerContext ctx) throws Exception {
       /* Channel channel = ctx.channel();
        channelGroup.writeAndFlush(" 【服务器】 -" +channel.remoteAddress() +" 离开\n");

        //验证一下每次客户端断开连接，连接自动地从channelGroup中删除调。
        System.out.println(channelGroup.size());
        //当客户端和服务端断开连接的时候，下面的那段代码netty会自动调用，所以不需要人为的去调用它
        //channelGroup.remove(channel);*/
    }

    //连接处于活动状态
    @Override
    public void channelActive(ChannelHandlerContext ctx) throws Exception {
       /* Channel channel = ctx.channel();
        System.out.println(channel.remoteAddress() +" 上线了");
        */
        /**
         * 调用channelGroup的writeAndFlush其实就相当于channelGroup中的每个channel都writeAndFlush
         *
         * 先去广播，再将自己加入到channelGroup中
         */
        /*
        channelGroup.writeAndFlush(" 【服务器】 -" +channel.remoteAddress() +" 加入\n");
        channelGroup.add(channel);
        */
    }

    @Override
    public void channelInactive(ChannelHandlerContext ctx) throws Exception {
        /*Channel channel = ctx.channel();
        System.out.println(channel.remoteAddress() +" 下线了");*/
    }

    @Override
    public void exceptionCaught(ChannelHandlerContext ctx, Throwable cause) throws Exception {
        cause.printStackTrace();
        ctx.close();
    }

}
