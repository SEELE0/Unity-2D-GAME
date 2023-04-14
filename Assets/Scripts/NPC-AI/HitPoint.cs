using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 普通攻击打击判定点
 */
public class HitPoint : MonoBehaviour 
{
    [Header("攻击力")]
    public float AttackDamage;//攻击力
    
    [Header("可否技能释放判定")]
    public bool bombAvilable; //是否可以释放技能
    private int dir; //方向
    
    public void OnTriggerEnter2D(Collider2D other) //碰撞体进入触发器
    {
        if(transform.position.x<other.transform.position.x) //如果碰撞体在左边
            dir = 1; //设置方向为1 
        else
            dir = -1; //设置方向为-1
        if (other.CompareTag("Player")) //如果碰撞体标签为Player
        {
            Debug.Log("玩家受伤");
            // other.GetComponent<Player>().TakeDamage(1); //调用玩家脚本中的TakeDamage方法
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir, 1) * 5,ForceMode2D.Impulse);//给玩家施加力(斜上方)
            other.GetComponent<IDamageable>().GetHit(AttackDamage);//调用IDamageable接口中的GetHit方法
        }

        if (other.CompareTag("Bomb")&&bombAvilable) //如果碰撞体标签为Bomb
        {
            Debug.Log("技能释放成功");
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir, 1) * 10,ForceMode2D.Impulse);//给炸弹施加力(斜上方)
            // bombAvilable = false; //设置技能不可用
        }

    }

}
