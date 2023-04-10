using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*攻击状态*/
public class AttackState : NpcBaseState
{
    public override void EnterState(NPC npc)
    {
        Debug.Log("发现敌人");
    }

    public override void OnUpdate(NPC npc)
    {
        if (npc.attackList.Count == 0) //如果视野中消失 
        {
            npc.TransitionToState(npc.patrolState);
        }
    }
}
