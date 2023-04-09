/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;*/
//有限状态机--- 基础状态
public abstract  class NpcBaseState //抽象类
{
   public abstract void EnterState(NPC npc); //进入状态
   public abstract void OnUpdate(NPC npc); //保持状态
   
}
