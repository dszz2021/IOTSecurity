package Server;


import Message.BodyE.*;
import Message.Message;
import Message.MessageBody;
import io.netty.channel.Channel;

import java.util.ArrayList;
import java.util.Map;

enum state{
    GameEntry,
    GamePreparation,
    GameStart,
    GetCards,
    ChaseForLandlord,
    Double,
    PlayCards,
    GameEnd,
    GameExit;
}
//这个类里面写从准备到游戏结束的逻辑
//    该类的一个对象代表一个房间
public class Room {


    String roomHostID;//房主ID
    ArrayList<String> clientIDList;//房间人的idList
    String roomID;
    String beiZhuText;
    Map<String, Channel> clientIDAndChannel;//房间中人员id和Channel的对应表
    int numberInRoom;//房间中的人数

    Room(String roomID,String beiZhuText,String roomHostID){
        clientIDList =new ArrayList<>();
        this.roomID = roomID;
        this.beiZhuText = beiZhuText;
        this.roomHostID = roomHostID;
        numberInRoom = 0;
    }

    Map<String, Channel> cIDAndChannel;//房间中人的id和Channel；
    static int count;

    //MessageBody info;
    state Gamestate=state.GameEntry;

    /**
     *              准备状态的操作
     * @param bodyE1
     * @return
     */
    public ArrayList<Message> GamePreparation(BodyE1 bodyE1){
        if(Gamestate==state.GameEntry)
            count=0;
        return null;
    }

    /**
     *              三个人都准备后进入游戏开始发牌
     * @return
     */
    private Message GameStart(){


        return null;
    }

    /**
     *             发牌
     * @return
     */
    private Message GetCards(){

        return null;
    }

    /**
     *             抢地主
     * @param bodyE2
     * @return
     */
    public Message ChaseForLandlord(BodyE2 bodyE2){

        return null;
    }

    /**
     *             加倍
     * @param bodyE3
     * @return
     */
    public Message Double(BodyE3 bodyE3){

        return null;
    }

    /**
     *            出牌  并记录是否有牌
     * @param bodyE4
     * @return
     */
    public Message CheckCardsCounts(BodyE4 bodyE4){
        return null;
    }

    public Message chat(BodyE18 bodyE18){

        return null;
    }

    //游戏中途退出
    public Message quitInGame(BodyE6 bodyE6){

        return null;
    }

    public Message quitBeforeGame(BodyE18 bodyE18){

        return null;
    }


    /**
     * 下面的几个函数都有用
     *
     */

    public String getRoomID() {
        return roomID;
    }

    public String getBeiZhuText() {
        return beiZhuText;
    }
    //得到房间中所有人的channel；
    public ArrayList<Channel> getChannels(){
        ArrayList<Channel> channelArrayList = new ArrayList<>();
        for(String id: clientIDList){
            channelArrayList.add(clientIDAndChannel.get(id));
        }
        return channelArrayList;
    }
    public Channel getChannel(String id){
        return cIDAndChannel.get(id);
    }
    public void joinRoom(String id,Channel channel){
        clientIDList.add(id);
        clientIDAndChannel.put(id,channel);
        numberInRoom++;
    }

    public int getNumberInRoom() {
        return numberInRoom;
    }
}
