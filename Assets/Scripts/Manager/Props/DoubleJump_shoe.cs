using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DoubleJump_shoe 
{
    public static bool SetProp()
    {
        if (ToolSession.CurrentTool.Contains("�е�����֮ѥ"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
