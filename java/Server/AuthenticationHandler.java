package Server;

import Message.BodyB.BodyB1;
import Message.BodyC.BodyC1;
import Message.Message;
import com.google.gson.Gson;
import io.netty.channel.Channel;
import io.netty.channel.ChannelHandlerContext;
import io.netty.channel.SimpleChannelInboundHandler;

public class AuthenticationHandler extends SimpleChannelInboundHandler<String> {
    GameLobby gameLobby;
    AuthenticationHandler(GameLobby gameLobby){
        this.gameLobby = gameLobby;
    }
    @Override
    protected void channelRead0(ChannelHandlerContext ctx, String msg) throws Exception {
        Channel channel = ctx.channel();
        Gson gson = new Gson();
        Message message1 = gson.fromJson(msg,Message.class);
        if(message1.getHead().getThickType()!=0xc){
            ctx.fireChannelRead(msg);//将信息传递给下一个handler
        }else {
            BodyC1 bodyC1 = gson.fromJson(message1.getBody(), BodyC1.class);
            /*
            调用Server认证部分的函数(小猪)
            输入: bodyC1
            输出： 一段加密后的json字符串密文 ccc
            ArrayList<String> sc = gameLobby.authenticator(bodyC1);
            String c = sc.get(1);
            Message messageBack = new Message(0xc,2,ccc)
            String back = gson.toJson(messageBack);
            channel.writeAndFlush(json)
            */
        }

    }
    @Override
    public void exceptionCaught(ChannelHandlerContext ctx, Throwable cause) throws Exception {
        cause.printStackTrace();
        ctx.close();
    }
}
