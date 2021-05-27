package DataBase;

import java.sql.Connection;
import java.sql.DriverManager;

public class DBConnectTest {
    private static final String URL = "jdbc:mysql://127.0.0.1:3306/程序设计?useUnicode=true&characterEncoding=utf-8&useSSL=false";
    private static final String USER = "root";
    private static final String PASSWORD = "Djt.000125";
    private static Connection conn;

    static Connection doConnect() {
        try {
            // 加载MySQL驱动程序
            Class.forName("com.mysql.cj.jdbc.Driver");
            // 获得数据库的连接
            conn = DriverManager.getConnection(URL, USER, PASSWORD);
        } catch (Exception e) {
            e.printStackTrace();
        }
        return conn;
    }
}
