using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UserSession
{
    public static string CurrentUser { get; set; } //当前用户
    
    public static void show()
    {
        Debug.Log("当前登录用户"+CurrentUser);
    }
}
