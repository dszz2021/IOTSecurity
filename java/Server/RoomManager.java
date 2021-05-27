package Server;

import Message.BodyD.*;
import Message.Message;
import com.google.gson.Gson;
import io.netty.channel.Channel;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.HashMap;
import java.util.Map;

public class RoomManager {
    Gson gson = new Gson();

    Map<String,Room> roomMap;//所有房间id和房间对象的对应表
    Map<String,String> roomIDAndClientName;//房间id和房主id的对应表
    ArrayList<String> roomNameList;

    RoomManager(){
        gson = new Gson();
        roomMap = new HashMap<String, Room>();
        roomIDAndClientName = new HashMap<>();
        roomNameList = new ArrayList<>();
    }

    //用户选择斗地主游戏后，返回该游戏的房间名列表
    public Message getRoomList(BodyD3 bodyD3){
        ArrayList<String> roomIDList = new ArrayList<>();
        BodyD12 bodyD12 = new BodyD12(bodyD3.getIDgame(),roomNameList);
        String bd12 = gson.toJson(bodyD12);
        Message message12 = new Message(0xd,0x12,bd12);
        return message12;
    }

    //查看某房间详细信息
    public Message getRoomInformation(BodyD7 bodyD7){
        String roomId = bodyD7.getIDroom();
        Room room = roomMap.get(roomId);
        String roomHostID = roomIDAndClientName.get(roomId);
        String text = room.getBeiZhuText();
        int number = room.getNumberInRoom();
        BodyD14 bodyD14 = new BodyD14(roomHostID,text,String.valueOf(number));
        String bd14 = gson.toJson(bodyD14);
        Message message = new Message(0xd,0x14,bd14);

        return message;
    }

    //用户创建房间
    //注意分配房间id时考虑线程安全
    public Message createRoom(BodyD4 bodyD4, Channel channel){
        boolean successful = true;
        String roomHostID = bodyD4.getIDc();//房主id
        long time1 = System.currentTimeMillis();
        String time = String.valueOf(time1/1000);
        String roomID = "斗地主"+time;
        Room room = new Room(roomID, bodyD4.getText(),roomHostID);

        room.joinRoom(bodyD4.getIDc(),channel);
        /**
         * 加锁
         */
        {
            roomMap.put(roomID, room);
            roomNameList.add(roomID);
            roomIDAndClientName.put(roomID, roomHostID);
        }
        BodyD13 bodyD13 = new BodyD13(successful,bodyD4.getIDgame(),roomID);
        String jsonD13 = gson.toJson(bodyD13);
        Message message13 = new Message(0xd,0x13,jsonD13);

        return message13;
    }

    //用户加入房间
    //考虑线程安全
    public Message joinRoom(BodyD5 bodyD5,Channel channel){
        String roomID = bodyD5.getIDroom();
        Room room = roomMap.get(roomID);
        room.joinRoom(bodyD5.getIDc(), channel);
        BodyD15 bodyD15 = new BodyD15(true);
        String json15 = gson.toJson(bodyD15);
        Message message15 = new Message(0xd,0x15,json15);



        return message15;
    }

    //通过RoomID得到Room的实例
    public Room getRoomByRoomID(String id){
        return roomMap.get(id);
    }

}
