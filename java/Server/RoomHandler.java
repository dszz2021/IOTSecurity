package Server;

import Message.BodyB.BodyB1;
import Message.BodyC.BodyC1;
import Message.BodyD.*;
import Message.MessageBody;
import Message.Message;
import com.google.gson.Gson;
import io.netty.channel.Channel;
import io.netty.channel.ChannelHandlerContext;
import io.netty.channel.SimpleChannelInboundHandler;

import java.util.ArrayList;

public class RoomHandler extends  SimpleChannelInboundHandler<String>{
    String clientID;
    GameLobby gameLobby;
    RoomManager roomManager;
    RoomHandler(GameLobby gameLobby,RoomManager roomManager){
        this.gameLobby = gameLobby;
        this.roomManager = roomManager;
    }
    @Override
    protected void channelRead0(ChannelHandlerContext channelHandlerContext, String s) throws Exception {
        Channel channel = channelHandlerContext.channel();
        /*
            将s解密
         */
        Gson gson = new Gson();
        Message message1 = gson.fromJson(s,Message.class);
        if(message1.getHead().getThickType()==0xD){
            switch (message1.getHead().getThinType()){
                case 1->{
                    //DES加密后发送给该发送者
                }
                case 2->{
                    //Des 加密后发送给该发送者
                }
                case 3->{


                }
                case 4->{

                }
                case 5->{


                }
                case 6->{

                }
                case 7->{

                }

            }
        }else if(message1.getHead().getThickType()==0xe){
            channelHandlerContext.fireChannelRead(s);
        }else {
            /*
            报错
             */
        }

    }
    @Override
    public void exceptionCaught(ChannelHandlerContext ctx, Throwable cause) throws Exception {
        cause.printStackTrace();
        ctx.close();
    }


}
