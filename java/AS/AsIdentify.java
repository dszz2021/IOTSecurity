package AS;


import DES.DES;
import Message.BodyA.BodyA1;
import Message.BodyA.BodyA2;
import Message.BodyA.TicketTGS;
import Message.ErrorInfo.Body71;
import Message.ErrorInfo.Body72;
import Message.Message;
import com.google.gson.Gson;

import java.util.Date;

import java.text.SimpleDateFormat;


public class AsIdentify {

    public String AsDoIdentify(BodyA1 bodyA1){
        String idc = bodyA1.getIDc();
        String idtgs = bodyA1.getIDtgs();
        String ts1 = bodyA1.getTS1();
        String KeyC = "123";//查找用户的密码KeyC，即每一个Client和AS服务器的共享密钥

        Gson jsonR = new Gson();
        DES des1 = new DES();

        if(idc!="") {//IDc查找失败，返回错误码0x71
            Body71 body71 = new Body71();
            String error71 = jsonR.toJson(body71);
            Message messageError71 = new Message(0x7,0x1,error71);
            String replyError71 = jsonR.toJson(messageError71);
            String EncodeReplyError71 = des1.cipher(replyError71,KeyC);
            return EncodeReplyError71;
        }
        if(idtgs!=""){//IDtgs查找失败，返回错误码0x72
            Body72 body72 = new Body72();
            String error72 = jsonR.toJson(body72);
            Message messageError72 = new Message(0x7,0x2,error72);
            String replyError72 = jsonR.toJson(messageError72);
            String EncodeReplyError72 = des1.cipher(replyError72,KeyC);
            return EncodeReplyError72;
        }
        else{
            TicketTGS ticketTGS = CreateTicket(bodyA1);
            BodyA2 reply = AsDoReply(ticketTGS,bodyA1);//获取BodyA2
            String bodyJson = jsonR.toJson(reply);//将获取的BodyA2转为String
            String EncodeBody = des1.cipher(bodyJson,KeyC);//将转为String的BodyA2加密
            Message message = new Message(0xa,0x2,EncodeBody);//将加密后的BodyA2封装成Message
            String replyMessage  = jsonR.toJson(message);//将Message封装为String
            return replyMessage;
        }
    }

    public TicketTGS CreateTicket(BodyA1 bodyA1){
        String idc = bodyA1.getIDc();
        String idtgs = bodyA1.getIDtgs();
        String KeyCTGS = "";//查找数据库
        String adc = "";

        Date dd = new Date();
        SimpleDateFormat df = new SimpleDateFormat("MM-dd-HH:mm");
        String time2 = df.format(dd);


        TicketTGS ticketTGS = new TicketTGS.TicketTGSBuilder().KeyCAndTgs(KeyCTGS).IDc(idc).ADc(adc).IDtgs(idtgs).TS2(time2).Lifetime2("3").build();
        return ticketTGS;


    }

    public BodyA2 AsDoReply(TicketTGS ticketTGS,BodyA1 bodyA1)
    {
        DES des = new DES();
        Gson json1 = new Gson();

        String jsonticket = json1.toJson(ticketTGS);//将TicketTGS用Json封装成String
        String KeyTGS = "";//查找AS和TGS共享密钥KeyTGS
        String EncodeTicketTGS = des.cipher(jsonticket,KeyTGS);//将封装完毕的String加密

        BodyA2 bodyA2 = new BodyA2.BodyA2Builder().KeyCandTgs(ticketTGS.getKeyCAndTgs()).IDtgs(bodyA1.getIDtgs()).TS2(ticketTGS.getTS2()).Lifetime2(ticketTGS.getLifetime2()).TicketTgs(EncodeTicketTGS).build();
        return bodyA2;
    }

}
