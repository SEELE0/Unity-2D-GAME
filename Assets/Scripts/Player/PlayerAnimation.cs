using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();//获取角色动画器
        rb = GetComponent<Rigidbody2D>();//获得角色刚体属性
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));//通过Mathf.Abs设置取绝对值实现左右移动都播放动画
    }
}
