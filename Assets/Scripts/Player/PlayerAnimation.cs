using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private Playercontorller contorller;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();//获取匹配角色动画器组件
        rb = GetComponent<Rigidbody2D>();//获得匹配角色刚体属性组件
        contorller = GetComponent<Playercontorller>();//获取匹配脚本属性组件
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));//通过Mathf.Abs设置取绝对值实现左右移动都播放动画
        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetBool("jump",contorller.isJump);//控制跳跃动画切换
        anim.SetBool("ground", contorller.isGround);//控制着陆动画切换
    }

}
