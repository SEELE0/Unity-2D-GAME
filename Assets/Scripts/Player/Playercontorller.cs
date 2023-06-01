using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontorller : MonoBehaviour,IDamageable
{
    private Rigidbody2D rb;//定义私有Rigidbody变量(刚体)
    private Animator anim;//定义私有Animator变量(动画器)
    public float speed; //横向移动速度
    public float jumpForce; //跳跃力度
    
    private FixedJoystick joystick; //定义虚拟摇杆变量
    //因为想在unity引擎窗口中调整所以用public变量
    
    [Header("Ground Check")]
    public Transform groundCheck;//
    public float checkRadius; //检测半径
    public LayerMask groundLayer;//指定要在 Physics.Raycast 中使用的图层。
    
    [Header("States Check")] //使用该 PropertyAttribute 在 Inspector 中的某些字段上方添加标题。
    //可以在unity中查看脚本时将下列变量归类在States Check 下
    public bool isGround;//地面检测
    public bool canJump;
    public bool isJump;

    [Header("特效效果FX")]
    public GameObject jumpFx;  //GameObject类是Unity 场景中所有实体的基类。
    public GameObject landFx;

    [Header("攻击设定")]
    public GameObject bomb;//炸弹游戏实体
    public float nextAttack = 0; //预存下次攻击事件标杆
    public float attackRate; //攻击频率技能CD

    [Header("玩家状态")] 
    public float health; //玩家生命值
    public bool isDead = false; //玩家死亡状态
    public int jumpCount = 1; //可跳跃次数
    public int maxJumpCount  = 2; //最大跳跃次数,实现二段跳

    [Header("道具详情")] 
    public List<string> Props;
    public GameObject Pet_Cat;
    
    
    // Start is called before the first frame update
    //start函数游戏开始时执行一次
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();//获得角色刚体
        anim = GetComponent<Animator>();//获取匹配角色动画器组件
        health = GameManager.Instance.LoadHealth();

        joystick = FindObjectOfType<FixedJoystick>();   //获取虚拟摇杆组件
        
        UIManager.Instance.UpdateHealth(health); //更新UI
        
        GameManager.Instance.Isplayer(this);
        
        SetPet();
        Props=ToolSession.CurrentTool;
    }

    // Update is called once per frame
    //update函数每一帧执行一次
    //实际操作中update响应实际操作例如键盘输入
    void Update()
    {
        anim.SetBool("dead",isDead);
        if (isDead)
        {
            return;
        }
        checkinput();
        
    } 

    private void FixedUpdate()  //固定时间执行，一般1S执行50次；实际操作中Fixedupdate响应物理执行的方法
    {
        if (isDead)
        {
            rb.velocity = Vector2.zero;//设置刚体的线性速度为0
            return;
        }
        PhysicsCheck();
        Movement();
        jump();
    }

    void checkinput() {//检测输入 

        
        /*
            如果嵌入商城系统后有道具功能为多段跳，可以更改此处条件语句
            为if (Input.GetButtonDown("Jump"))
         */
        /*if (ToolSession.CurrentTool.Contains("有点神奇之靴")) //如果当前道具列表包含多段跳道具
        {
              
        }*/

        if (DoubleJump_shoe.SetProp())  //检测是否有鞋子道具
        {
            if (Input.GetButtonDown("Jump") && jumpCount < maxJumpCount)
            {
                jumpCount++;
                canJump = true;
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump") && isGround)//在用户按下由 buttonName 标识的虚拟按钮的帧期间返回 true。
            {
                canJump = true;
                // Debug.Log("按下了跳跃键");
            }
        }

        
        /*if (Input.GetButtonDown("Jump"))//在用户按下由 buttonName 标识的虚拟按钮的帧期间返回 true。
        {
            Debug.Log("按下了跳跃键");
            canJump = true;
        }*/
        
        
        //添加道具检测

        if(Input.GetKeyDown(KeyCode.E))
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("大炮发射");
            
        }
    }

    void Movement()//人物移动
    {
        
        /*键盘操作*/
        //float horizontalInput = Input.GetAxis("Horizontal");  //横向虚拟轴 ；值大小 -1~1 包括小数
        
        float horizontalInput = Input.GetAxisRaw("Horizontal"); //横向虚拟轴；值大小 -1~1 不包括小数
        
        /*//虚拟摇杆操作
        float horizontalInput = joystick.Horizontal; //获取虚拟摇杆水平轴输入*/
        
        
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);  //定义刚体的**线性速度**，横向速度更改，纵向不变
        
         //键盘操作 判断
         if (horizontalInput != 0) {//控制人物左移右移人物翻转
            transform.localScale = new Vector3(horizontalInput, 1, 1);//通过控制缩放Scale的三维变量实现
        }
        
        /*
        //虚拟摇杆操作 判断
        if (horizontalInput > 0) //控制人物左移右移人物翻转
        {
            transform.eulerAngles = new Vector3(0,0,0);
        }
        else if (horizontalInput < 0)
        {
            transform.eulerAngles = new Vector3(0,180,0);
        }
        */


    }
    
    void jump()
    {
        if (isGround)
        {
            jumpCount = 1; //重置跳跃次数
        }
        if (canJump)
        {
            // Debug.Log("跳跃");
            isJump = true;
            jumpFx.SetActive(true); //播放跳跃特效
            jumpFx.transform.position = transform.position + new Vector3(0,-0.55f,0);
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);//设置rb.velocity.x目的是让角色在跳跃时可以左右位移
            rb.gravityScale = 4;  //跳起时更改重力大小为4
            canJump = false;
        }
        
        
    }

    public void ButtonJump() 
    {
        if (isGround)
            canJump = true;
    }
    
    
    void PhysicsCheck() //物理检测
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);//检测是否在地面上
        if (isGround ) 
        {
            rb.gravityScale = 1; //触地时更改重力大小为1
            isJump = false;
        }
        else
        {
            rb.gravityScale = 4;  //跳起时更改重力大小为4、
        }

    }

    public void landfx()//animetion event
    {
        landFx.SetActive(true);
        landFx.transform.position = transform.position + new Vector3(0, -0.8f, 0);

    }

    public void Attack()//animetion event
    {
        if (Time.time > nextAttack)
        {
            nextAttack = Time.time + attackRate;
            Instantiate(bomb, transform.position + new Vector3(0, 0.5f, 0),bomb.transform.rotation); //生成炸弹预制体
        }
    }

    public void OnDrawGizmos()//在unity中显示检测范围;Unity内置函数
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);//绘制圆形检测范围
    }

    public void GetHit(float damage)
{
    /*实现受伤短暂无敌*/
    if (!anim.GetCurrentAnimatorStateInfo(1).IsName("player_hit")) //如果当前动画状态不为player_hit
    {
        health -= damage; //减少生命值
        if (health < 1 && !isDead) //如果生命值小于1且未死亡
        {
            health = 0; //生命值小于1时设为0
            isDead = true;
            // Destroy(gameObject);//销毁游戏对象
        }
        anim.SetTrigger("hit");
        
        UIManager.Instance.UpdateHealth(health); //更新UI
    }
}

    public void SetPet()
{
    if (pet.SetPet())
    {
        Pet_Cat.SetActive(true);
    }
}

    /*public void Connon()
    {
        
    }*/

}

