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
        /*
         * 1.如果播放的不是off动画则爆炸
         * 2.GetCurrentAnimatorStateInfo(0) 获取动画器图层第0层的动画状态
         */
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("bomb_off")) //如果播放的不是off动画则爆炸
        {    
            if (waitTime <= Time.time - startTime)
            {
                anim.Play("bomb_explotion");
            }
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
        
        /*数组*/
        Collider2D[] aroundObjects = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer); //容纳范围内所有碰撞体
        rb.gravityScale = 0; //设置刚体重力为0防止取消碰撞体后调出地图区域
        //循环数组中所有碰撞体
        foreach (var item in aroundObjects )
        {
            Vector3 pos = transform.position - item.transform.position; //计算爆炸力的方向,通过正负进行判断
            item.GetComponent<Rigidbody2D>().AddForce((-pos+Vector3.up) * bombForce,ForceMode2D.Impulse); //给碰撞体添加爆炸力

            if (item.CompareTag("Bomb") && 
                item.GetComponent<Bomb>().anim.GetCurrentAnimatorStateInfo(0).IsName("bomb_off")) //如果碰撞体是炸弹且炸弹处于熄灭状态
            {
                item.GetComponent<Bomb>().TurnOn();//调用炸弹脚本组件中的TurnOn方法
            }
        }
    }

    public void Destroy_bomb()
    {
        Destroy(gameObject);//销毁炸弹
    }
    
    public void TurnOff()
    {
        anim.Play("bomb_off");//播放炸弹熄灭动画
        gameObject.layer = LayerMask.NameToLayer("Npc");//更改图层为npc---目的:防止熄灭的炸弹持续存在在NPC的攻击列表中
        
    }

    public void TurnOn()
    {
        startTime=Time.time;//重置计时器
        anim.Play("bomb_on");//播放炸弹点燃动画
        gameObject.layer = LayerMask.NameToLayer("Bomb");//更改图层为bomb
    }
    
}