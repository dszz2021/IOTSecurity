package Message;

public class Message {
    MessageHead head;
    String body;        //利用Json序列化之后的报文信息
    String sign;

    public Message(int thickType,int thinType,String body){
        this.head = new MessageHead(thickType,thinType,body.length());
        this.body = body;
        this.sign = getSign(body);
    }

    /**
     *              通过序列化的body字符串利用RSA私钥进行加密生成数字签名。
     * @param body
     * @return
     */
    private String getSign(String body){
        /*
           这里调用RSA加密生成签名
         */
        return "";
    }

    public MessageHead getHead() {
        return head;
    }

    public String getBody() {
        return body;
    }

    public String getSign() {
        return sign;
    }
}
