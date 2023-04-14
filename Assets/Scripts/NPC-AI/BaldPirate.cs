using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaldPirate : NPC,IDamageable
{
    public override void FlipDirection()
    {
        if (transform.position.x < targetPoint.position.x) //如果npc位置大于目标点位置
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        else
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);//通过改变欧拉角进行翻转
    }
    public void GetHit(float damage)
    {
        health -= damage;
            if (health < 1)
            {
                health = 0;
                isDead = true;
                // anim.SetBool("dead",isDead);
                // Destroy(gameObject);
            }
            anim.SetTrigger("hit");
    }
}
