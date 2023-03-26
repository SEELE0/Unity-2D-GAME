using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Animator anim;
    private Collider2D col;
    private Rigidbody2D rb;

    public float startTime;
    public float waitTime;
    public float bombForce; //爆炸威力

    [Header("Check")]
    public float radius;//爆炸范围
    public LayerMask targetLayer; //检测图层


    // Start is called before the first frame update
    void Start()
    {
        anim=GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        startTime = Time.time;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waitTime <= Time.time - startTime)
        {
            anim.Play("bomb_explotion");
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius); //绘制可视化范围
    }

    public void Explotion()//控制爆炸,同时设置为爆炸动画animation event
    {
        
        col.enabled = false;//取消选中爆炸炸弹的碰撞体
        Collider2D[] aroundObjects = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer); //容纳范围内所有碰撞体
        rb.gravityScale = 0; //设置刚体重力为0防止取消碰撞体后调出地图区域
        //循环数组中所有碰撞体
        foreach (var item in aroundObjects )
        {
            Vector3 pos = transform.position - item.transform.position; //计算爆炸力的方向,通过正负进行判断
            item.GetComponent<Rigidbody2D>().AddForce((-pos+Vector3.up) * bombForce,ForceMode2D.Impulse); //给碰撞体添加爆炸力

        }
    }

    public void Destroy_bomb()
    {
        Destroy(gameObject);//销毁炸弹
    }
}