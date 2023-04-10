using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class NPC : MonoBehaviour
{
    NpcBaseState currentState;//保存当前状态
    
    public Animator anim;//动画组件
    public int AnimeState;
    [Header("移动属性")]
    public float speed;
    public Transform pointA,pointB;//获取A，B两点的位置
    public Transform targetPoint;//npc移动目标点
    public List<Transform> attackList = new List<Transform>();//攻击目标列表;
    /*通过list.add和list.remove  添加/移除对象到列表*/
    
    public PatrolState patrolState = new PatrolState();//巡逻状态
    public AttackState attackState = new AttackState();//攻击状态
    public virtual void Init()
    {
        anim=GetComponent<Animator>();
    }
    
    public void Awake() //初始化,先于start函数确保初始化游戏内一直有值不会报错
    {
        Init();
    }
    // Start is called before the first frame update
    void Start()
    {
        TransitionToState(patrolState);//初始状态为巡逻状态
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdate(this); //使用this将类对象作为参数传递给函数方法-----保持状态
        anim.SetInteger("AnimeState",AnimeState); //设置动画状态
    }

    public void TransitionToState(NpcBaseState state)//状态转换
    {
        currentState = state; //切换当前状态到指定状态
        currentState.EnterState(this); //使用this将类对象作为参数传递给函数方法-----进入状态
    }
    
    public void Movement()//移动到目标点
    {
        
        transform.position = Vector2.MoveTowards(transform.position,
                                                        targetPoint.position, 
                                                        speed * Time.deltaTime); //位置移动
        
        FlipDirection();
        
    }
    
    public void Attack()//攻击
    {
        
    }

    /*
     * 使用虚函数实现多态，方便函数重写
     * 多个npc有多个不同的技能，函数重写即可
     */
    public virtual void Skill()//技能  
    {
        
    }

    public void FlipDirection()//判别角色反转
    {
        if (transform.position.x > targetPoint.position.x)
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        else
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);//通过改变欧拉角进行翻转
    }

    public void SwitchPoint()
    {
        if (Mathf.Abs(pointA.position.x - transform.position.x) > Mathf.Abs(pointB.position.x - transform.position.x))
        {
            targetPoint = pointA;
        }
        else
            targetPoint = pointB;

        
    }

    public void OnTriggerStay2D(Collider2D collision) //保持在触发器内
    {
        if(!attackList.Contains(collision.transform)) //如果不在列表中添加
            attackList.Add(collision.transform);
    }

    public void OnTriggerExit2D(Collider2D collision)//离开触发器
    {
        
        attackList.Remove(collision.transform);
    }
}
   
