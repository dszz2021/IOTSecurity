package DataBase;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.SQLException;
import java.util.List;

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
