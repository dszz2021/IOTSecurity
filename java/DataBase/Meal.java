package DataBase;

import java.sql.Connection;
import java.util.List;

public class Meal {
    String name;
    int type;
    double price;

    public static void main(String[] args) {
        List<Meal> list = GetDBTest.MealList();
        for (Meal meal : list) {
            System.out.println(meal.name + " " + meal.type + " " + meal.price);
        }
    }
}