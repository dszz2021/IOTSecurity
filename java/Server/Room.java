package Server;


import Message.Message;

import java.nio.channels.Channel;
import java.util.Map;

//这个类里面写从准备到游戏结束的逻辑
//    该类的一个对象代表一个房间
public class Room {
    int numberInRoom;//房间中的人数
    Map<String, Channel> cIDAndChannel;//房间中人的id和Channel；

    //处理准备模块
    public Message ready()


}
