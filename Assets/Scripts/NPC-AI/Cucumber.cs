using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cucumber : NPC,IDamageable //继承NPC类
{
    public void Setoff()//设置炸弹吹灭动画效果----动画器 Event
    {
        //?判断targetPoint是否为空，如果为空则不执行后面的代码
        targetPoint.GetComponent<Bomb>()?.TurnOff();//调用Bomb脚本组件中的TurnOff方法
        // Debug.Log("炸弹设置成功");
    }

    

    /*    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/


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
