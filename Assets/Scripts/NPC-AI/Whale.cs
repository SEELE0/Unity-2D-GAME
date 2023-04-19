using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whale : NPC,IDamageable
{
    public float scale; //缩放比例
    
    
    public void Swalow() //吞噬动画效果----动画器 Event
    {
        targetPoint.GetComponent<Bomb>()?.TurnOff(); //调用Bomb脚本组件中的TurnOff方法
        targetPoint.gameObject.SetActive(false); //炸弹消失
        
        transform.localScale *= scale; //缩放比例

        if (transform.localScale.x > 2f) //如果缩放比例大于3f
        {
            /*transform.localScale /= scale ; //缩放比例*/
            while (transform.localScale != new Vector3(1f,1f,1f))//当缩放比例等于1f时
            {
                transform.localScale /= scale ; //缩放比例
            }
            
            // transform.localScale = new Vector3(1f,1f,1f); //设置缩放比例
            /*targetPoint.gameObject.SetActive(true);*/
            //找到Active是false的bomb然后恢复为true
            /*GameObject[] bombs = GameObject.FindGameObjectsWithTag("Bomb");
            bombs[1].transform.position = transform.position; //设置炸弹位置
            bombs[1].gameObject.SetActive(true); //炸弹出现*/
            /*for(int i=0 ;i<=1 ; i++)//遍历数组
            {
                bombs[i].transform.position = transform.position; //设置炸弹位置
                bombs[i].gameObject.SetActive(true); //炸弹出现
            }*/
        }

        
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
