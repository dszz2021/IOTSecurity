package Server;


import Message.BodyE.BodyE1;
import Message.Message;
import Message.MessageBody;

import java.nio.channels.Channel;
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
    int numberInRoom;//房间中的人数
    Map<String, Channel> cIDAndChannel;//房间中人的id和Channel；
    static int count;

    MessageBody info;
    state Gamestate=state.GameEntry;
    public Message GamePreparation(MessageBody mes){
        if(Gamestate==state.GameEntry)
            count=0;

        return null;

    }
    public Message GameStart(MessageBody mes){


        return null;
    }
    public Message GetCards(MessageBody mes){

        return null;
    }
    public Message ChaseForLandlord(MessageBody mes){

        return null;
    }
    public Message Double(MessageBody mes){

        return null;
    }
    public Message CheckCardsCounts(MessageBody mes){
        return null;
    }
    private void GameState(){
        //switch Gamestate
        if(Gamestate==state.GameEntry||Gamestate==state.GamePreparation)
            GamePreparation(info);
        else if(Gamestate==state.GameStart)
            GameStart(info);
        else if(Gamestate==state.GetCards)
            GetCards(info);
        else if(Gamestate==state.ChaseForLandlord)
            ChaseForLandlord(info);
        else if(Gamestate==state.Double)
            Double(info);
        else if(Gamestate==state.PlayCards)
            CheckCardsCounts(info);
        else if(Gamestate==state.GameEnd)
            Gamestate=state.GameExit;
        else if(Gamestate==state.GameExit)
            return;
    }

}
