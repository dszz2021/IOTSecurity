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
import java.util.Iterator;
import java.util.Map;
import java.util.Objects;

public class LobbyHandler extends  SimpleChannelInboundHandler<String>{
    String clientID;
    GameLobby gameLobby;
    RoomManager roomManager;
    String sessionKey;
    LobbyHandler(GameLobby gameLobby,RoomManager roomManager){
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
                    BodyD1 bodyD1 = gson.fromJson(message1.getBody(), BodyD1.class);
                    this.clientID = bodyD1.getIDc();
                    ArrayList<MessageBody> messageBodies;
                    messageBodies = gameLobby.load(bodyD1,channel);
                    String bD8 = gson.toJson(messageBodies.get(0));
                    String bD10 = gson.toJson(messageBodies.get(1));
                    String bD11 = gson.toJson(messageBodies.get(2));
                    Message messageD8 = new Message(0xd,0x8,bD8);
                    Message messageD10 = new Message(0xd,0x8,bD10);
                    Message messageD11 = new Message(0xd,0x8,bD11);
                    String json8 = gson.toJson(messageD8);
                    String json10 = gson.toJson(messageD10);
                    String json11 = gson.toJson(messageD11);
                    this.sessionKey = gameLobby.getSessionKey(this.clientID);
                    String sessionKey = gameLobby.getSessionKey(clientID);
                    //DES加密后发送给该发送者
                }
                case 2->{
                    BodyD2 bodyD2 = gson.fromJson(message1.getBody(), BodyD2.class);
                    BodyD9 bodyD9 = gameLobby.changeInformation(bodyD2);
                    String bD9 = gson.toJson(bodyD9);
                    Message messageD9 = new Message(0xD,0x9,bD9);
                    String json9 = gson.toJson(messageD9);
                    String sessionKey = gameLobby.getSessionKey(clientID);
                    //Des 加密后发送给该发送者
                }
                case 3->{
                    BodyD3 bodyD3 = gson.fromJson(message1.getBody(), BodyD3.class);
                    Message messageD12 = roomManager.getRoomList(bodyD3);
                    //Message messageD12 = new Message(0xd,0x12,bD12);
                    String json12 = gson.toJson(messageD12);


                }
                case 4->{
                    BodyD4 bodyD4 = gson.fromJson(message1.getBody(), BodyD4.class);
                    Message messageD13 = roomManager.createRoom(bodyD4,channel);
                    String json13 = gson.toJson(messageD13);

                }
                case 5->{
                    BodyD5 bodyD5 = gson.fromJson(message1.getBody(), BodyD5.class);
                    Message messageD15 = roomManager.joinRoom(bodyD5,channel);
                    String json15 = gson.toJson(messageD15);


                }
                case 6->{
                    BodyD6 bodyD6 = gson.fromJson(message1.getBody(), BodyD6.class);
                    Message messageD16 = gameLobby.chat(bodyD6);
                    String json15 = gson.toJson(messageD16);

                    //加密，得到c
                    String c ="";

                    // 群发
                    Map<String,Channel> idAndChannels = gameLobby.getIdAndChannels();
                    for (Map.Entry<String, Channel> entry : idAndChannels.entrySet()) {
                        //if(entry.getKey().equals(this.clientID)){
                        entry.getValue().writeAndFlush(c);
                        // }
                    }


                }
                case 7->{
                    BodyD7 bodyD7 = gson.fromJson(message1.getBody(), BodyD7.class);
                    Message messageD14 = roomManager.getRoomInformation(bodyD7);
                    String json14 = gson.toJson(messageD14);

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
