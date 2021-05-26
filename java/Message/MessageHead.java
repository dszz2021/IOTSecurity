package Message;

import Message.BodyA.BodyA1;

public class MessageHead {
    int thickType;//报文粗粒度分类，（A-E五类报文类和1-8八类错误码）（16进制）
    int thinType;//报文细致的分类
    int length;//报文MessageBody通过Json序列化之后的字符串长度
    public MessageHead(int thickType,int thinType,int length){
        this.length = length;
        this.thickType = thickType;
        this.thinType = thinType;
    }

    public int getLength() {
        return length;
    }

    public int getThickType() {
        return thickType;
    }

    public int getThinType() {
        return thinType;
    }
}
