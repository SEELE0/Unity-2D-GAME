using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class pet 
{
    public static bool SetPet()
    {
        if (ToolSession.CurrentTool.Contains("≥ËŒÔ√®ﬂ‰"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
