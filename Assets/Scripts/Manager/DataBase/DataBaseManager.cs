using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using UnityEditor.MemoryProfiler;
using System.Security.Cryptography;
using Script.DataBase;

public class DataBaseManager : MonoBehaviour
{
    // MySQL 数据库信息。
    private string _connectionString;

    // private MySqlConnection Conn = null;

    // private MD5 md5Hash = MD5.Create();


    public static DataBaseManager Instance; // 单例模式

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

            /*报错解决:
             报错信息:MySqlException: There is already an open DataReader associated with this Connection which must be closed first.
             
             猜测原因：
                在同一个连接中不能同时打开两个DataReader
                
                第一个查询语句创建了一个名为readerUser的DataReader，
                并且在此期间你还使用了另一个查询语句创建了另一个DataReader（名为readerProps）。
                由于  在同一连接上只能有一个DataReader处于活动状态，  所以在这种情况下，你需要先关闭第一个DataReader（readerUser），
                然后才能执行第二个查询并创建第二个DataReader（readerProps）。
                因此，可以尝试在使用第二个DataReader之前关闭第一个DataReader
            解决方法：
                1.使用using语句来自动释放资源
                2.使用多个连接   
             
             
             */
            
            
            // Conn.Open();
            using (MySqlCommand cmd = new MySqlCommand(sql, Conn))  //添加参数
            {
                cmd.Parameters.AddWithValue("@username", username);//添加参数
                cmd.Parameters.AddWithValue("@password", password);//添加参数
                using (MySqlDataReader readerUser = cmd.ExecuteReader())
                {
                    if (readerUser.Read())
                    {
                        Debug.Log("登录成功");
                        
                        readerUser.Close(); // 关闭第一个DataReader
                        //查询道具
                        string queryProps = "SELECT goods_name FROM orders WHERE order_userid = (SELECT id FROM shop_user where users = @username ) and order_succ ='1' ";
                        using (MySqlCommand cmdProps = new MySqlCommand(queryProps, Conn))  //添加参数
                        {
                            cmdProps.Parameters.AddWithValue("@username", username);//添加参数
                            using (MySqlDataReader readerProps = cmdProps.ExecuteReader()) //执行查询
                            {
                                List<string>Props = new List<string>();
                                while (readerProps.Read())//读取数据
                                {
                                    Debug.Log(readerProps.GetString("goods_name"));
                                    string propName = readerProps.GetString("goods_name");
                                    Props.Add(propName);
                                }
                                ToolSession.CurrentTool = Props; //初始化当前用户道具
                                ToolSession.show(); //显示当前用户拥有的道具
                            }
                        }

                        
                        MainUIManager.Instance.MainMenu();
                        UserSession.CurrentUser = username;
                        UserSession.show();
                        
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

    /*public void checkTool(string username, MySqlConnection Conn)
    {
        //查询道具
        string sql = "SELECT goods_name FROM orders WHERE order_userid = (SELECT id FROM shop_user where users = @username ) and order_succ ='1' ";
        
        using (Conn)
        {
            using (MySqlCommand cmd = new MySqlCommand(sql, Conn))  //执行查询)
            {
                cmd.Parameters.AddWithValue("@username", username);//添加参数
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    
                    
                    
                    /*if (reader.Read())
                    {
                        Debug.Log("当前用户拥有有点神奇之靴");
                        return true;
                    }
                    else
                    {
                        Debug.Log("当前用户 不拥有 有点神奇之靴");
                        return false;
                    }#1#
                }
            }
        }
    }*/

    
}