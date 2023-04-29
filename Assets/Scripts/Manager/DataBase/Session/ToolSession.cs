using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ToolSession 
{
    public static List<string> CurrentTool { get; set; } //当前工具

    public static void show()
    {
        foreach (string item in CurrentTool)
        {
            Debug.Log("用户道具有"+item);
        }
    }
}
