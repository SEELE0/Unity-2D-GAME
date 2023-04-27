using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using Script.DataBase;
using UnityEngine;
using UnityEngine.UI;

public class login : MonoBehaviour
{
    private MD5 md5hash = MD5.Create(); 
    private MySqlConnection Conn = DBConn.createConnection();
    
    [Header("配置输入框")]
    public InputField usernameInput;
    public InputField passwordInput;
    /*private void Awake()
    {
        MySqlConnection Conn = DataBaswManager.Instance.createConnection();
    }*/
    
    
    private string GetMd5Hash(MD5 md5Hash, string input)
    {
        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

        StringBuilder sBuilder = new StringBuilder(); //创建一个可变字符串

        for (int i = 0; i < data.Length; i++) //将字节转换为十六进制字符串
        {
            sBuilder.Append(data[i].ToString("x2"));  //x2表示以十六进制格式输出
        }
        
        Debug.Log("密码加密成功" + sBuilder);
        return sBuilder.ToString();
    }
    
    public void LoginButtonCheck()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;
        string passwordMd5 = GetMd5Hash(md5hash, password);
        MySqlConnection Conn = DataBaswManager.Instance.createConnection();
        DataBaswManager.Instance.checkUser(username, passwordMd5, Conn);
        /*if (DataBaswManager.Instance.checkUser(username, passwordMd5, Conn))
        {
            MainUIManager.Instance.MainMenu();
        }*/
    }
    
    
    
}
