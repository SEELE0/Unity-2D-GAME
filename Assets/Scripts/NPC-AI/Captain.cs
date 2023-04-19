using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Captain : NPC,IDamageable
{
    SpriteRenderer spriteRenderer; //获取精灵渲染器

    public override void Update()
    {
        base.Update();
        
        if(AnimeState == 0)
            spriteRenderer.flipX = false;
    }
    
    
    public override void Init()
    {
        base.Init();
     
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    
    public override void Skill()
    {
        base.Skill();//调用父类方法

        if (anim.GetCurrentAnimatorStateInfo(1).IsName("Skill"))
        {
            spriteRenderer.flipX = true;
            if (transform.position.x > targetPoint.position.x) //炸弹在左侧向右跑
            {
                Debug.Log("向右跑");
                transform.position = Vector2.MoveTowards(transform.position,
                    transform.position + Vector3.right, speed * 2 * Time.deltaTime); 
            }
            else //炸弹在右侧向左跑
            {
                Debug.Log("向左跑");
                transform.position = Vector2.MoveTowards(transform.position,
                    transform.position + Vector3.left, speed * 2 * Time.deltaTime);
            }
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
    
    public override void FlipDirection() //翻转方向
    {
        if (transform.position.x < targetPoint.position.x) //如果npc位置小于目标点位置
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
