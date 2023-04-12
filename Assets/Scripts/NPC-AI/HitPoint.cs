using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other) //碰撞体进入触发器
    {
        if (other.CompareTag("Player")) //如果碰撞体标签为Player
        {
            Debug.Log("玩家受伤");
            // other.GetComponent<Player>().TakeDamage(1); //调用玩家脚本中的TakeDamage方法
            
        }

        if (other.CompareTag("Bomb"))
        {
            Debug.Log("技能释放成功");
        }
    }

}
