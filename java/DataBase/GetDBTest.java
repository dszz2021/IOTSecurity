package DataBase;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

public class GetDBTest {
    public static List MealList() {
        List<Meal> list = new ArrayList<Meal>();
        //调用方法实现对数据库的连接
        Connection conn = DBConnectTest.doConnect();
        try {
            //查询语句，从表格中筛选出字段
            String sql = "select FName,FType,FPrice from food";
            //执行sql，并返回数据列表
            Statement st = conn.createStatement();
            ResultSet rs = st.executeQuery(sql);
            while (rs.next()) {
                //将筛选后的记录逐条添加到集合中，
                Meal meal = new Meal();
                meal.name = rs.getString("FName");
                meal.type = rs.getInt("FType");
                meal.price = rs.getDouble("FPrice");
                list.add(meal);
            }
            rs.close();
            st.close();
        } catch (Exception e) {
            e.printStackTrace();
        }
        //此方法最终返回所有记录的集合
        return list;
    }
}
