using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DoubleJump_shoe 
{
    public static bool SetProp()
    {
        if (ToolSession.CurrentTool.Contains("有点神奇之靴"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
