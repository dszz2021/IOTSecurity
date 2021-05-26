package Server;

import Message.BodyC.BodyC1;
import Message.BodyC.BodyC2;
import Message.BodyD.*;
import Message.Message;
import Message.MessageBody;
import io.netty.channel.Channel;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

public class GameLobby {
    int numberInLobby;
    Map<String, Channel> idAndChannel;//用户的id和其Channel的对应表
    Map<String, String> idAndName;//用户的id与其昵称的对应表
    ArrayList<String> gameName;//所有游戏的名字
    ArrayList<String> clientNameList;//大厅所有在线玩家的昵称
    Map<String,String> clientAndSessionKey;//用户的ID和对应的sessionKey
    GameLobby(){
        numberInLobby = 0;
        idAndChannel = new HashMap<>();
        idAndName = new HashMap<>();
        gameName = new ArrayList<>();
        clientAndSessionKey = new HashMap<>();
        clientNameList = new ArrayList<>();
    }
    public String getSessionKey(String clientID){
        return clientAndSessionKey.get(clientID);
    }

    //服务器认证
    //返回值为加密后的C2报文（不需要报头和报尾）
    public String authenticator(BodyC1 bodyC1){


        //这个函数处理之后将sessionKey存储在clientAndSessionKey中
        return null;
    }

    //用户认证成功之后，会发送给服务器D1报文作为登录
    //返回的D8、D10、D11报文的list。
    public ArrayList<MessageBody> load(BodyD1 bodyD1, Channel channel){

        return null;
    }

    //修改用户信息
    public BodyD9 changeInformation(BodyD2 bodyD2){

        return null;
    }


    public Message chat(BodyD6 bodyD6){


        return null;
    }

    //通过Id得到玩家昵称
    public String getNameByID(String idClient){
        return idAndName.get(idClient);
    }

    public Channel getChannelByID(String idClient){
        return idAndChannel.get(idClient);
    }

    public Map<String,Channel> getIdAndChannels(){
        return  idAndChannel;
    }
}
