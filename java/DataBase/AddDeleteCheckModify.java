package DataBase;

import java.sql.*;
import java.util.List;
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.ArrayList;

//import Message;

public class AddDeleteCheckModify {
    //增
    public void add(Meal meal) {
        try {
            // 获取数据库的连接
            Connection conn = DBConnectTest.doConnect();
            // 设置SQL规则
            String sql = "INSERT INTO food(FName,FType,FPrice) VALUES (?,?,?)";
            // 预处理sql语句
            PreparedStatement prepare = conn.prepareStatement(sql);
            // 设置sql语句中的values值
            prepare.setString(1, meal.name);
            prepare.setInt(2, meal.type);
            prepare.setDouble(3, meal.price);
            // 执行SQL语句，实现数据添加
            prepare.execute();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public boolean AddUserInfo(String info){
        try {
            String[] tmp=info.split(" ");
            // 获取数据库的连接
            Connection conn = DBConnectTest.doConnect();
            // 设置SQL规则
            String sql = "INSERT INTO user_message(UName,Uid,Ubean) VALUES (?,?,?)";
            // 预处理sql语句
            PreparedStatement prepare = conn.prepareStatement(sql);
            // 设置sql语句中的values值
            prepare.setString(1, tmp[0]);
            prepare.setString(2, tmp[1]);
            prepare.setString(3, tmp[2]);
            // 执行SQL语句，实现数据添加
            prepare.execute();
            return true;
        } catch (SQLException e) {
            e.printStackTrace();
            return false;
        }
    }
    //删
    public void delete(String mealName) {
        try {
            // 获取数据库的连接
            Connection conn = DBConnectTest.doConnect();
            // 设置SQL规则
            //String sql = "INSERT INTO table1(id,user,password,age) VALUES (?,?,?,?)";
            String sql = "delete from food where FName=?";
            // 预处理sql语句
            PreparedStatement prepare = conn.prepareStatement(sql);
            // 设置sql语句中的values值
            prepare.setString(1, mealName);
            // 执行SQL语句，实现数据添加
            prepare.execute();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    //查
    public void check(String mealName) {
        //获取之前的数据集合
        List<Meal> list = GetDBTest.MealList();
        //foreach遍历集合
        for (Meal meal : list) {
            //通过菜品姓名匹配
            if (meal.name.equals(mealName)) {
                //匹配正确获取菜品类型和单价
                int mealType = meal.type;
                double mealPrice = meal.price;
            }
        }
    }
    public String CheckUserInfo(String id){
        String info="null";
        try {
            Connection conn = DBConnectTest.doConnect();
            //查询语句，从表格中筛选出字段
            String sql = "select UName,Ubean from user_message where Uid= " + id;
            //执行sql，并返回数据列表
            Statement st = conn.createStatement();
            ResultSet rs = st.executeQuery(sql);
            if (rs.next()){
                info=rs.getString("Uname");
                info=info+" ";
                info=info+Integer.toString(rs.getInt("Ubean"));
            }
        }
        catch(SQLException e){
            e.printStackTrace();
        }
        return info;
    }

    public String CheckUserPwd(String Cid){
        String pwd="null";
        try {
            Connection conn = DBConnectTest.doConnect();
            //查询语句，从表格中筛选出字段
            String sql = "select Cpwd from as_client where Cid= " + Cid;
            //执行sql，并返回数据列表
            Statement st = conn.createStatement();
            ResultSet rs = st.executeQuery(sql);
            if (rs.next()){
                pwd=rs.getString("Cpwd");
            }
        }
        catch(SQLException e){
            e.printStackTrace();
        }
        return pwd;
    }

    public boolean CheckUserId(String Cid){
        try {
            Connection conn = DBConnectTest.doConnect();
            //查询语句，从表格中筛选出字段
            String sql = "select Cpwd from as_client where Cid= " + Cid;
            //执行sql，并返回数据列表
            Statement st = conn.createStatement();
            ResultSet rs = st.executeQuery(sql);
            if (rs.next()){
                return false;
            }
        }
        catch(SQLException e){
            e.printStackTrace();
        }
        return true;
    }

    public boolean CheckTgsId(String TGSid){
        try {
            Connection conn = DBConnectTest.doConnect();
            //查询语句，从表格中筛选出字段
            String sql = "select Cpwd from as_tgs where TGSid= " + TGSid;
            //执行sql，并返回数据列表
            Statement st = conn.createStatement();
            ResultSet rs = st.executeQuery(sql);
            if (rs.next()){
                return false;
            }
        }
        catch(SQLException e){
            e.printStackTrace();
        }
        return true;
    }

    public String CheckKeyTGS(String TGSid){
        String KeyTGS="null";
        try {
            Connection conn = DBConnectTest.doConnect();
            //查询语句，从表格中筛选出字段
            String sql = "select KEYtgs from as_tgs where TGSid= " + TGSid;
            //执行sql，并返回数据列表
            Statement st = conn.createStatement();
            ResultSet rs = st.executeQuery(sql);
            if (rs.next()){
                KeyTGS = rs.getString("Keytgs");
            }
        }
        catch(SQLException e){
            e.printStackTrace();
        }
        return KeyTGS;
    }

    public String CheckKeyServer(String Sid){
        String KeyServer="null";
        try {
            Connection conn = DBConnectTest.doConnect();
            //查询语句，从表格中筛选出字段
            String sql = "select KEYserver from tgs where Sid= " + Sid;
            //执行sql，并返回数据列表
            Statement st = conn.createStatement();
            ResultSet rs = st.executeQuery(sql);
            if (rs.next()){
                KeyServer = rs.getString("KeyServer");
            }
        }
        catch(SQLException e){
            e.printStackTrace();
        }
        return KeyServer;
    }

    public boolean CheckServerID(String Sid){
        try {
            Connection conn = DBConnectTest.doConnect();
            //查询语句，从表格中筛选出字段
            String sql = "select KEYserver from tgs where Sid= " + Sid;
            //执行sql，并返回数据列表
            Statement st = conn.createStatement();
            ResultSet rs = st.executeQuery(sql);
            if (rs.next()){
                return false;
            }
        }
        catch(SQLException e){
            e.printStackTrace();
        }
        return true;
    }


    public boolean ChangeUserInfo(String info){
        //success return 1, else 0
        String[] tmp=info.split(" ");
        try {
            Connection conn = DBConnectTest.doConnect();
            //查询语句，从表格中筛选出字段
            String sql = "update user_message set Uname=? where Uid=?";
            PreparedStatement prepare = conn.prepareStatement(sql);
            // 设置sql语句中的values值
            prepare.setString(1, tmp[1]);
            prepare.setString(2, tmp[0]);
            // 执行SQL语句，实现数据更新
            prepare.execute();
            return true;
        }
        catch(SQLException e){
            e.printStackTrace();
            return false;
        }
    }
    public String GetGameList(){
        String info="";
        try {
            Connection conn = DBConnectTest.doConnect();
            //查询语句，从表格中筛选出字段
            String sql = "select Gid,Gname from user_message";
            //执行sql，并返回数据列表
            Statement st = conn.createStatement();
            ResultSet rs = st.executeQuery(sql);
            int Status;
            while (rs.next()){
                Status=rs.getInt("Status");
                if(Status==0)
                    continue;
                info+=Integer.toString(rs.getInt("Gid"));
                info+=" ";
                info+=rs.getString("Gname");
                info+=" ";
            }
        }
        catch(SQLException e){
            info="null";
            e.printStackTrace();
        }
        return info;
    }
    public String GetGameInfo(int Gid){
        String info="";
        try {
            Connection conn = DBConnectTest.doConnect();
            //查询语句，从表格中筛选出字段
            String sql = "select Summary from user_message where Gid="+Integer.toString(Gid);
            //执行sql，并返回数据列表
            Statement st = conn.createStatement();
            ResultSet rs = st.executeQuery(sql);

            if (rs.next()){
                info=Integer.toString(rs.getInt("Count"));
                info+=" ";
                info+=rs.getString("Summary");
            }
        }
        catch(SQLException e){
            info="null";
            e.printStackTrace();
        }
        return info;
    }
    public int Login(String mes){
        String[] Info=mes.split(" ");
        try {
            Connection conn = DBConnectTest.doConnect();
            //查询语句，从表格中筛选出字段
            String sql = "select Cpwd from as_client where Cid="+Info[0];
            //执行sql，并返回数据列表
            Statement st = conn.createStatement();
            ResultSet rs = st.executeQuery(sql);

            if (rs.next()){
                String pwd=rs.getString("Cpwd");
                if(pwd==Info[1])
                    return 1;
                else
                    return 0;
            }
            return -1;
        }
        catch(SQLException e){
            e.printStackTrace();
            return -1;
        }
    }
    //改
    public void modify(String mealName, int mealType, double newPrice) {
        try {
            // 获取数据库的连接
            Connection conn = DBConnectTest.doConnect();
            // 设置SQL规则
            String sql = "Update food set FType=?,FPrice=? where FName=?";
            // 预处理sql语句
            PreparedStatement prepare = conn.prepareStatement(sql);
            // 设置sql语句中的values值
            prepare.setInt(1, mealType);
            prepare.setDouble(2, newPrice);
            prepare.setString(3, mealName);
            // 执行SQL语句，实现数据更新
            prepare.execute();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }
}
