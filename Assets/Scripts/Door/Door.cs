using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
     Animator anim;
     BoxCollider2D coll;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); //获取匹配角色动画器组件
        coll = GetComponent<BoxCollider2D>(); //获取匹配角色碰撞器组件
        GameManager.Instance.IsExitDoor(this); //调用GameManager脚本中的IsExitDoor方法，传入当前脚本
        
        coll.enabled = false; //禁用碰撞器
    }

    /*// Update is called once per frame
    void Update()
    {
        
    }*/
    public void OpenDoor()  
    {
        anim.Play("open");
        coll.enabled = true; //启用碰撞器
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) //如果碰撞体标签为Player
        {
            GameManager.Instance.NextLevel();  //调用GameManager脚本中的NextLevel方法，进入下一个房间
        }
    }
}
