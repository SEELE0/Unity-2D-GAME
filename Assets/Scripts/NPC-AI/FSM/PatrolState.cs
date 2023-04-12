using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//有限状态机---巡逻状态

public class PatrolState:NpcBaseState
    {
        public override void EnterState(NPC npc) //重写方法
        {
            npc.AnimeState = 0;//设置动画状态 --idle
            
            npc.SwitchPoint();//设置目标点
            
        }

        public override void OnUpdate(NPC npc)
        {
            /*这里需要注意IsName内部的属性与动画器中的命名一致*/
            if(!npc.anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))//如果idle动画播放完毕
            {
                npc.AnimeState = 1;//设置动画状态为其他动画
                npc.Movement();
            }
            
            
            if (Mathf.Abs(npc.transform.position.x - npc.targetPoint.position.x) < 0.01f) //如果到达目标点
            {
                npc.TransitionToState(npc.patrolState);//切换状态
            }

            if (npc.attackList.Count > 0) //如果发现敌人
            {
                npc.TransitionToState(npc.attackState);
            }

            
        }
    }

