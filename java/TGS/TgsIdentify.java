package TGS;

import DES.DES;
import Message.BodyA.TicketTGS;
import Message.BodyA.TicketV;
import Message.BodyB.Authenticator;
import Message.BodyB.BodyB1;
import Message.BodyB.BodyB2;
import Message.ErrorInfo.Body81;
import Message.ErrorInfo.Body82;
import Message.Message;
import com.google.gson.Gson;

import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;

public class TgsIdentify {

    public String TgsDoIdentify(BodyB1 bodyB1){
        DES des1 = new DES();
        String KeyV = "";//查找TGS与Server的共享密钥KeyV
        String idv = bodyB1.getIDv();

        //将被json封装的TicketTgs字符串重新转为TicketTgs类型
        String KeyTGS = "";//查找AS和TGS的共享密钥KeyTGS
        String EncodeTicket = des1.deCipher(bodyB1.getTicketTgs(),KeyTGS);//先用DES解密成Json封装的字符串
        Gson jsonR = new Gson();
        TicketTGS ticketTGS = jsonR.fromJson(EncodeTicket,TicketTGS.class);

        //将Authenticator解密再解封装
        String EncodeAuthenticator = des1.deCipher(bodyB1.getAuthenticator(), ticketTGS.getKeyCAndTgs());
        Authenticator authenticator = jsonR.fromJson(EncodeAuthenticator,Authenticator.class);

        String ts2 = ticketTGS.getTS2();//获取TicketTgs内的TS2
        String hour = ts2.substring(6,8);//获取TS2中的表示小时的两位字符
        int effectiveHour = Integer.valueOf(hour).intValue()+3;//将小时转为int类型并加上lifetime的三小时
        Calendar cal = Calendar.getInstance();
        int currentHour = cal.get(Calendar.HOUR_OF_DAY);//获取当前小时，存为int


        if(idv!="") { //IDv查找失败，返回错误码0x81
            Body81 body81 = new Body81();
            String error81 = jsonR.toJson(body81);
            Message messageError81 = new Message(0x8,0x1,error81);
            String replyError1 = jsonR.toJson(messageError81);
            String EncodeReplyError81 = des1.cipher(replyError1,KeyV);
            return EncodeReplyError81;
        }
        if(currentHour>effectiveHour){//AS提供的Ticket过期，返回错误码0x82
            Body82 body82 = new Body82();
            String error82 = jsonR.toJson(body82);
            Message messageError82 = new Message(0x8,0x2,error82);
            String replyError82 = jsonR.toJson(messageError82);
            String EncodeReplyError82 = des1.cipher(replyError82,KeyV);
            return EncodeReplyError82;
        }
        else{
            TicketV ticketV = CreateTicket(bodyB1,ticketTGS);
            BodyB2 reply = TgsDoReply(ticketV);
            String bodyJson = jsonR.toJson(reply);
            Message message = new Message(0xb,0x2,bodyJson);
            String replyMessage  = jsonR.toJson(message);
            String EncodeReply = des1.cipher(replyMessage, KeyV);
            return EncodeReply;
        }


    }

    public TicketV CreateTicket(BodyB1 bodyB1,TicketTGS ticketTGS){
        String idv = bodyB1.getIDv();


        String KeyCV = "";
        //查找数据库并存入KeyC&V

        Date dd = new Date();
        SimpleDateFormat df = new SimpleDateFormat("MM-dd-HH:mm");
        String time4 = df.format(dd);
        TicketV ticketV = new TicketV.TicketVBuilder().
                KeyCAndV(KeyCV).IDc(ticketTGS.getIDc()).
                adc(ticketTGS.getADc()).
                IDv(bodyB1.getIDv()).
                TS4(time4).
                Lifetime4("3").
                build();

        return ticketV;

    }

    public BodyB2 TgsDoReply(TicketV ticketV)
    {
        Gson json1 = new Gson();
        DES des = new DES();

        String ticket = json1.toJson(ticketV);//将TicketV用Json封装成String
        String KeyV = "";//查找TGS和Server共享密钥KeyV
        String EncodeTiketV = des.cipher(ticket,KeyV);//将封装完毕的String加密


        BodyB2 bodyB2 = new BodyB2(ticketV.getKeyCAndV(),ticketV.getIDv(),ticketV.getTS4(),EncodeTiketV);
        return bodyB2;
    }

}