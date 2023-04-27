using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using UnityEditor.MemoryProfiler;
using System.Security.Cryptography;
using Script.DataBase;

public class DataBaswManager : MonoBehaviour
{
    // 这里需要替换为你自己的 MySQL 数据库信息。
    private string _connectionString;

    // private MySqlConnection Conn = null;

    // private MD5 md5Hash = MD5.Create();


    public static DataBaswManager Instance; // 单例模式

    private void Awake() //Awake()方法在Start()方法之前执行
    {
        // connectionString = "Server=localhost;Database=shop;Uid=root;Pwd=114514;Port=3306;";
        /*目的:场景切换时不删除gameManager
        所以要进行判断防止出现两个manager*/
        DBConn.CloseConnection();
        if (Instance == null) //判断实例是否为空
        {
            Instance = this; //如果为空则创建一个实例
        }
        else
        {
            Destroy(gameObject); //如果不为空则销毁当前实例
        }
    }

    public void testConnect()
    {
        DBConn.testConnect();
    }


    public MySqlConnection createConnection()
    {
        return DBConn.createConnection();
    }

    public bool checkUser(string username, string password, MySqlConnection Conn)
    {
        /*使用参数化查询来避免SQL注入攻击*/
        string sql = "SELECT * FROM shop_user WHERE users = @username AND password = @password";
        /*
         //需要手动设置close
        Conn.Open();
        MySqlCommand cmd = new MySqlCommand(sql, Conn);  //
        cmd.Parameters.AddWithValue("@username", username);//添加参数
        cmd.Parameters.AddWithValue("@password", password);//添加参数
        MySqlDataReader reader = cmd.ExecuteReader(); //执行查询
        if (reader.Read())
        {
            Debug.Log("登录成功");
                        
            return true;
        }
        else
        {
            Debug.Log("登录失败");
            loginFail.SetActive(true); //登录失败提示
            return false;
        }
        */


        //使用using语句来自动释放资源
        using (Conn)
        {
            /*MySqlCommand cmd = new MySqlCommand(sql, Conn);
            cmd.Parameters.AddWithValue("@username", username); //添加参数
            cmd.Parameters.AddWithValue("@password", password); //添加参数
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                Debug.Log("登录成功");
                MainUIManager.Instance.MainMenu();
                return true;
            }
            else
            {
                Debug.Log("登录失败");
                MainUIManager.Instance.loginFail();
                return false;
            }*/

            /*Conn.Open();*/
            using (MySqlCommand cmd = new MySqlCommand(sql, Conn))  //执行查询)
            {
                cmd.Parameters.AddWithValue("@username", username);//添加参数
                cmd.Parameters.AddWithValue("@password", password);//添加参数
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Debug.Log("登录成功");
                        MainUIManager.Instance.MainMenu();
                        return true;
                    }
                    else
                    {
                        Debug.Log("登录失败");
                        MainUIManager.Instance.loginFail();
                        return false;
                    }
                }
            }
        }
        // MySqlCommand cmd = new MySqlCommand(sql, Conn);  //执行查询
    }
}