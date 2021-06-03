package Server;

import com.google.gson.Gson;
import DES.DES;
import Message.BodyA.TicketV;
import Message.BodyB.Authenticator;
import Message.BodyC.BodyC1;
import Message.BodyC.BodyC2;
import Message.BodyD.*;
import Message.ErrorInfo.Body11;
import Message.ErrorInfo.Body24;
import Message.Message;
import Message.MessageBody;
import com.google.gson.Gson;
import io.netty.channel.Channel;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

public class GameLobby {
    DES des;
    Gson gson;
    int numberInLobby;
    Map<String, Channel> idAndChannel;//用户的id和其Channel的对应表
    Map<String, String> idAndName;//用户的id与其昵称的对应表
    ArrayList<String> clientNameList;//大厅所有在线玩家的昵称
    ArrayList<String> clientIDList;//大厅所有在线玩家的昵称

    Map<String,String> gameIDAndName;//所有游戏的名字

    Map<String,String> clientAndSessionKey;//用户的ID和对应的sessionKey
    GameLobby(){
        des = new DES();
        gson = new Gson();
        numberInLobby = 0;
        idAndChannel = new HashMap<>();
        idAndName = new HashMap<>();
        gameIDAndName = new HashMap<>();
        clientAndSessionKey = new HashMap<>();
        clientNameList = new ArrayList<>();
        getGameName();

    }
    public boolean addGame(String gameID,String gameName){
        /**
         * 更新游戏列表数据库
         */
        this.gameIDAndName.put(gameID,gameName);
        return true;
    }
    private void getGameName(){
        /**
         * 从数据库中将游戏ID和游戏名提取出来,放在gameIDAndName中
         */
        gameIDAndName.put("1","J斗地主J");
    }

    public String getSessionKey(String clientID){
        return clientAndSessionKey.get(clientID);
    }

    //服务器认证
    //返回值为加密后的C2报文（不需要报头和报尾）
    public ArrayList<String> authenticator(BodyC1 bodyC1){
        String KeyV = "123";//查找TGS和Server的共享密钥KeyV
        String EncodeTicketV = des.deCipher(bodyC1.getTicketV(),KeyV);//先用DES解密成Json封装的字符串，得到String类型的TicketV
        TicketV tickeV = gson.fromJson(EncodeTicketV, TicketV.class);//将字符串还原成TicketV

        //将Authenticator解密再解封装
        String EncodeAuthenticator = des.deCipher(bodyC1.getAuthenticator(), tickeV.getKeyCAndV());
        Authenticator authenticator = gson.fromJson(EncodeAuthenticator,Authenticator.class);

        String keyCV = tickeV.getKeyCAndV();
        String clientID = tickeV.getIDc();
        /**
         * 认证
         */

        ArrayList<String> stringArrayList = new ArrayList<>();
        BodyC2 bodyC2 = new BodyC2(authenticator.getTS3()+1);
        String jsonC2 = gson.toJson(bodyC2);
        String EncodeBody = des.cipher(jsonC2,keyCV);

        Message messageC2 = new Message(0xc,0x2,EncodeBody);
        String jsonMessage= gson.toJson(messageC2);

        stringArrayList.add(jsonMessage);
        stringArrayList.add(clientID);

        clientAndSessionKey.put(clientID,keyCV);

        return stringArrayList;
    }

    //用户认证成功之后，会发送给服务器D1报文作为登录
    //返回的D8、D10、D11报文的list。
    public ArrayList<Message> load(BodyD1 bodyD1, Channel channel){
        ArrayList<Message> messageArrayList = new ArrayList<>();
        //判断这个人是否登录

        /**
         *      加锁
         */
        {
            if (clientIDList.contains(bodyD1.getIDc())) {
                //该Id已经登录,返回错误报文
                Body24 body24 = new Body24();
                String json24 = gson.toJson(body24);
                Message message = new Message(0x2, 0x4, json24);
                messageArrayList.add(message);
                return messageArrayList;
            }
            clientIDList.add(bodyD1.getIDc());
        }

        //先将该用户的channel加入大厅中,更新在线人员列表
        String clientID = bodyD1.getIDc();
        String name = "";//用户昵称
        String score = "0";
        /**
         * 查数据库，通过ID查出昵称和积分
         */
        if(true){
            //成功查到该账号信息
        }
        else {
            //数据库中无该ID时，直接注册进数据库，默认昵称为游客+时间戳，初始积分设置为200
            long time1 = System.currentTimeMillis();
            String time = String.valueOf(time1/1000);
            name ="游客"+ time;
            score = "200";
        }

        /**
         *   加锁
         */
        {
            idAndName.put(clientID, name);
            clientNameList.add(name);
            idAndChannel.put(bodyD1.getIDc(), channel);
        }

        BodyD8 bodyD8 = new BodyD8(true,name,score);
        String json8 = gson.toJson(bodyD8);
        BodyD10 bodyD10 = new BodyD10(clientNameList);
        String json10 = gson.toJson(bodyD10);
        BodyD11 bodyD11 = new BodyD11(gameIDAndName);
        String json11 = gson.toJson(bodyD11);
        Message message8 = new Message(0xd,0x8,json8);
        Message message10 = new Message(0xd,0x10,json10);
        Message message11 = new Message(0xd,0x11,json11);
        messageArrayList.add(message8);
        messageArrayList.add(message10);
        messageArrayList.add(message11);
        return messageArrayList;
    }

    //修改用户信息
    public Message changeInformation(BodyD2 bodyD2){
        Message message9;
        boolean successful = true;
        String id = bodyD2.getIDc();
        String name = bodyD2.getName();
        /**
         *  查数据库，  将个人信息修该
         */
        //修改成功后连接
        if(successful){
            //修改成功
            BodyD9 bodyD9 = new BodyD9(successful);
            String json9 = gson.toJson(bodyD9);
            message9 = new Message(0xd,0x9,json9);


        }else {
            BodyD9 bodyD9 = new BodyD9(!successful);
            bodyD9.setReason("失败原因");
            String json9 = gson.toJson(bodyD9);
            message9 = new Message(0xd,0x9,json9);

        }
        return message9;
    }


    public Message chat(BodyD6 bodyD6){
        String name = idAndName.get(bodyD6.getIDc());
        BodyD16 bodyD16 = new BodyD16(name,bodyD6.getText());
        String bd16 = gson.toJson(bodyD16);
        Message message = new Message(0xd,0x16,bd16);
        return message;
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
