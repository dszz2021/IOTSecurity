package Server;



import DES.DES;
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
    DES des;
    String clientID;
    GameLobby gameLobby;
    RoomManager roomManager;
    String sessionKey;
    LobbyHandler(GameLobby gameLobby,RoomManager roomManager){
        des = new DES();
        this.gameLobby = gameLobby;
        this.roomManager = roomManager;
    }
    @Override
    protected void channelRead0(ChannelHandlerContext channelHandlerContext, String s) throws Exception {
        Channel channel = channelHandlerContext.channel();
        if(s.substring(0,9).equals("clientID:")) {
            this.clientID = s.substring(9);
            this.sessionKey = gameLobby.getSessionKey(clientID);
        }
        else {
            String decipher = des.cipher(s, sessionKey);
            Gson gson = new Gson();
            Message message1 = gson.fromJson(decipher, Message.class);
            if (message1.getHead().getThickType() == 0xD) {
                switch (message1.getHead().getThinType()) {
                    case 1 -> {
                        BodyD1 bodyD1 = gson.fromJson(message1.getBody(), BodyD1.class);
                        this.clientID = bodyD1.getIDc();
                        ArrayList<Message> messageArrayList = gameLobby.load(bodyD1, channel);
                        for (Message m : messageArrayList) {
                            String messageJson = gson.toJson(m);
                            String cipher = des.cipher(messageJson, sessionKey);
                            channel.writeAndFlush(cipher);
                        }

                    }
                    case 2 -> {
                        BodyD2 bodyD2 = gson.fromJson(message1.getBody(), BodyD2.class);
                        Message message9 = gameLobby.changeInformation(bodyD2);
                        String msJson9 = gson.toJson(message9);
                        String cipher = des.cipher(msJson9, sessionKey);
                        channel.writeAndFlush(cipher);
                    }
                    case 3 -> {
                        BodyD3 bodyD3 = gson.fromJson(message1.getBody(), BodyD3.class);
                        Message messageD12 = roomManager.getRoomList(bodyD3);
                        String msJson12 = gson.toJson(messageD12);
                        String cipher = des.cipher(msJson12, sessionKey);
                        channel.writeAndFlush(cipher);

                    }
                    case 4 -> {
                        BodyD4 bodyD4 = gson.fromJson(message1.getBody(), BodyD4.class);
                        Message messageD13 = roomManager.createRoom(bodyD4, channel);
                        String msJson13 = gson.toJson(messageD13);
                        String cipher = des.cipher(msJson13, sessionKey);
                        channel.writeAndFlush(cipher);
                    }
                    case 5 -> {
                        BodyD5 bodyD5 = gson.fromJson(message1.getBody(), BodyD5.class);
                        Message messageD15 = roomManager.joinRoom(bodyD5, channel);
                        String msJson15 = gson.toJson(messageD15);
                        String cipher = des.cipher(msJson15, sessionKey);
                        channel.writeAndFlush(cipher);

                    }
                    case 6 -> {
                        BodyD6 bodyD6 = gson.fromJson(message1.getBody(), BodyD6.class);
                        Message messageD16 = gameLobby.chat(bodyD6);
                        String msJson16 = gson.toJson(messageD16);
                        // 群发
                        Map<String, Channel> idAndChannels = gameLobby.getIdAndChannels();
                        for (Map.Entry<String, Channel> entry : idAndChannels.entrySet()) {
                            //得到相应的client的key
                            String key = gameLobby.getSessionKey(entry.getKey());
                            //将信息进行加密
                            String cipher = des.deCipher(msJson16,key);
                            entry.getValue().writeAndFlush(cipher);
                        }
                    }
                    case 7 -> {
                        BodyD7 bodyD7 = gson.fromJson(message1.getBody(), BodyD7.class);
                        Message messageD14 = roomManager.getRoomInformation(bodyD7);
                        String json14 = gson.toJson(messageD14);
                        String cipher = des.cipher(json14, sessionKey);
                        channel.writeAndFlush(cipher);
                    }

                }
            } else if (message1.getHead().getThickType() == 0xe) {
                channelHandlerContext.fireChannelRead(s);
            } else {
            /*
            报错
             */
            }
        }

    }



    @Override
    public void exceptionCaught(ChannelHandlerContext ctx, Throwable cause) throws Exception {
        cause.printStackTrace();
        ctx.close();
    }


}
