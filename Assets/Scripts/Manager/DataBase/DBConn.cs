using MySql.Data.MySqlClient;
using System;
using System.Data;
using UnityEngine;

namespace Script.DataBase
{
    public static class DBConn
    {
        private static string connectionString = "Server=localhost;Database=gameshop;Uid=root;Pwd=114514;Port=3306;";
        private static MySqlConnection Conn = null;
        
        public static void testConnect()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();

                Debug.Log("MySQL Connection State: " + connection.State);
                /*Debug.Log("MySQL 数据库: " + connection.Database);
                String sql = "SELECT * FROM `shop_user`"; 
                var command = new MySqlCommand(sql, connection); 
                command.ExecuteNonQuery();  // 执行查询
                MySqlDataReader reader = command.ExecuteReader(); // 读取查询结果
                while (reader.Read())
                {
                    Debug.Log(reader.GetString("users"));
                }*/

                connection.Close();
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.ToString());
            }
        }
        
        public static MySqlConnection createConnection()
        {
            // string connectionString = "Server=localhost;Database=shop;Uid=root;Pwd=114514;Port=3306;";
            // MySqlConnection Conn = null;
            
            try
            {
                Conn = new MySqlConnection(connectionString);
                Conn.Open();
                Debug.Log("MySQL Connection State: " + Conn.State); //显示数据库状态
                Debug.Log("MySQL 数据库: " + Conn.Database); //显示数据库名称
                    
            }
                
            catch (System.Exception ex)
            {
                Debug.LogError(ex.ToString());
            }

            return Conn;
        }
        
        public static void CloseConnection()  //关闭数据库连接
        {
            if (Conn != null && Conn.State != ConnectionState.Closed)
            {
                Conn.Close();
            }
        }
    }
}