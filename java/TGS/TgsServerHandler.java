package TGS;

import Message.BodyA.BodyA1;
import Message.BodyB.BodyB1;
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

public class TgsServerHandler extends SimpleChannelInboundHandler<String>{


    /**
     * 服务器端收到任何一个客户端的消息都会触发这个方法
     */
    @Override
    protected void channelRead0(ChannelHandlerContext ctx, String msg) throws Exception {
        Channel channel = ctx.channel();
        Gson gson = new Gson();
        Message message1 = gson.fromJson(msg,Message.class);
        if(message1.getHead().getThickType()!=0xb){
            /*
            生成相应错误码的报文，并发回。
            */
        }else {
            BodyB1 bodyB1 = gson.fromJson(message1.getBody(), BodyB1.class);
            /*
            调用TGS认证部分的函数（苏方）
            输入: bodyB1
            输出： 一段加密后的json字符串密文 bbb。*/
            TgsIdentify identify = new TgsIdentify();
            String messageback = identify.TgsDoIdentify(bodyB1);

            channel.writeAndFlush(messageback);

        }

    }

    @Override
    public void handlerRemoved(ChannelHandlerContext ctx) throws Exception {

    }

    //连接处于活动状态
    @Override
    public void channelActive(ChannelHandlerContext ctx) throws Exception {

    }

    @Override
    public void channelInactive(ChannelHandlerContext ctx) throws Exception {
    }

    @Override
    public void exceptionCaught(ChannelHandlerContext ctx, Throwable cause) throws Exception {
        cause.printStackTrace();
        ctx.close();
    }

}
