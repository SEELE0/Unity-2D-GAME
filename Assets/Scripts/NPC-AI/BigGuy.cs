using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigGuy : NPC,IDamageable
{

    public int power;
    
    public Transform pickUpPoint;
    
    public void PickUpBomb() //拾取炸弹 animation event
    {
        if (targetPoint.CompareTag("Bomb") && !hasBomb)
        {
            targetPoint.gameObject.transform.position = pickUpPoint.position; //把炸弹放到手上
            
            targetPoint.SetParent(pickUpPoint);//把炸弹设置为手的子物体
            targetPoint.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic; //设置炸弹为刚体类型
            hasBomb = true;
        }

    }

    public void ThrowAway() //扔炸弹 animation event
    {
        if (hasBomb)//如果手上有炸弹
        {
            targetPoint.SetParent(transform.parent.parent); //把炸弹设置为手的子物体
            targetPoint.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic; //设置炸弹为刚体类型
            
            if(FindObjectOfType<Playercontorller>().gameObject.transform.position.x - transform.position.x < 0) //玩家在左侧
                targetPoint.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1)*power, ForceMode2D.Impulse); //给炸弹一个向上的力
            else
            {
                targetPoint.GetComponent<Rigidbody2D>().AddForce(new Vector2(1,1)*power,ForceMode2D.Impulse);
            }
            hasBomb = false;
        }
    }




    public override void FlipDirection()
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
