using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/*攻击状态*/
public class AttackState : NpcBaseState
{
    public override void EnterState(NPC npc)
    {
        Debug.Log("发现敌人");
        npc.AnimeState = 2; //设置动画状态进入攻击动画图层
        npc.targetPoint = npc.attackList[0]; //设置目标点为敌人
    }

    public override void OnUpdate(NPC npc)
    {
        if(npc.hasBomb) //如果有炸弹
            return;  //直接返回
        
        if (npc.attackList.Count == 0) //如果视野中消失 
        {
            npc.TransitionToState(npc.patrolState);
        }
        
        if (npc.attackList.Count > 1) //如果视野中有多个敌人
        {
            for (int i = 0; i < npc.attackList.Count; i++)
            {
                if (Mathf.Abs(npc.transform.position.x - npc.attackList[i].position.x) <
                    Mathf.Abs(npc.transform.position.x - npc.targetPoint.position.x)) //如果其他敌人距离比目标点更近
                {
                    npc.targetPoint = npc.attackList[i]; //设置目标点为敌人
                }
            }
        }else if (npc.attackList.Count == 1) //如果视野中只有一个敌人
        {
            npc.targetPoint = npc.attackList[0]; //设置目标点为敌人
        }

        if (npc.targetPoint.CompareTag("Player"))
        {
            npc.Attack();
        }

        if (npc.targetPoint.CompareTag("Bomb"))
        {
            npc.Skill();
        }
        
        npc.Movement();
        
        
    }

}
