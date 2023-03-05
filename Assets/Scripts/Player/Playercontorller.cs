using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontorller : MonoBehaviour
{
    private Rigidbody2D rb;//定义私有Rigidbody变量(刚体)
    public float speed; //横向移动速度
    public float jumpForce; //跳跃力度
    //因为想在unity引擎窗口中调整所以用public变量
    
    [Header("Ground Check")]
    public Transform groundCheck;
    public float checkRadius; //检测半径
    public LayerMask groundLayer;//指定要在 Physics.Raycast 中使用的图层。


    [Header("States Check")] //使用该 PropertyAttribute 在 Inspector 中的某些字段上方添加标题。
    //可以在unity中查看脚本时将下列变量归类在States Check 下
    public bool isGround;//地面检测
    public bool canJump;


    // Start is called before the first frame update
    //start函数游戏开始时执行一次
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();//获得角色刚体
    }

    // Update is called once per frame
    //update函数每一帧执行一次
    //实际操作中update响应实际操作例如键盘输入
    void Update()
    {
        checkinput();
    }

    private void FixedUpdate()  //固定时间执行，一般1S执行50次；实际操作中Fixedupdate响应物理执行的方法
    {
        PhysicsCheck();
        Movement();
        jump();
    }

    void checkinput() {//检测输入 

        /*
            如果嵌入商城系统后有道具功能为多段跳，可以更改此处条件语句
            为if (Input.GetButtonDown("Jump"))
         */
        if (Input.GetButtonDown("Jump") && isGround)//在用户按下由 buttonName 标识的虚拟按钮的帧期间返回 true。
        {
            canJump = true;
        }
    }

    void Movement()//人物移动
    {
        //float horizontalInput = Input.GetAxis("Horizontal");  //横向虚拟轴 ；值大小 -1~1 包括小数
        float horizontalInput = Input.GetAxisRaw("Horizontal"); //横向虚拟轴；值大小 -1~1 不包括小数
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);  //定义刚体的**线性速度**，横向速度更改，纵向不变
        
        if (horizontalInput != 0) {//控制人物左移右移人物翻转
            transform.localScale = new Vector3(horizontalInput, 1, 1);//通过控制缩放Scale的三维变量实现
        }
    }
    
    void jump()
    {
        if (canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);//设置rb.velocity.x目的是让角色在跳跃时可以左右位移
            rb.gravityScale = 4;  //跳起时更改重力大小为4
            canJump = false;
        }
        
    }

    void PhysicsCheck() //物理检测
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);//检测是否在地面上
        if (isGround) 
        {
            rb.gravityScale = 1; //触地时更改重力大小为1
            //canJump = false;
        }
        /*else
        {
            rb.gravityScale = 4;  //跳起时更改重力大小为4、
        }*/

    }

    public void OnDrawGizmos()//在unity中显示检测范围;Unity内置函数
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);//绘制圆形检测范围
    }

}

