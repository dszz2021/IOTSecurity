package Server;

import Message.BodyD.*;
import Message.Message;
import io.netty.channel.Channel;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

public class RoomManager {
    Map<String,Room> roomMap;//所有房间id和房间对象的对应表
    Map<String,String> roomIDAndClientName;//房间id和房主id的对应表
    RoomManager(){
        roomMap = new HashMap<String, Room>();
        roomIDAndClientName = new HashMap<>();
    }

    //用户选择斗地主游戏后，返回该游戏的房间名列表
    public Message getRoomList(BodyD3 bodyD3){
        ArrayList<String> roomIDList = new ArrayList<>();


        return null;
    }

    //查看某房间详细信息
    public Message getRoomInformation(BodyD7 bodyD7){

        return null;
    }

    //用户创建房间
    //注意分配房间id时考虑线程安全
    public Message createRoom(BodyD4 bodyD4, Channel channel){

        return null;
    }

    //用户加入房间
    //考虑线程安全
    public Message joinRoom(BodyD5 bodyD5,Channel channel){

        return null;
    }

    //通过RoomID得到Room的实例
    public Room getRoomByRoomID(String id){
        return roomMap.get(id);
    }

}
