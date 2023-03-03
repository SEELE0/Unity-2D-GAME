using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontorller : MonoBehaviour
{
    private Rigidbody2D rb;//����˽��Rigidbody����(����)
    public float speed; //�����ƶ��ٶ�
    public float jumpForce; //��Ծ����
    //��Ϊ����unity���洰���е���������public����
    
    [Header("Ground Check")]
    public Transform groundCheck;
    public float checkRadius; //���뾶
    public LayerMask groundLayer;//ָ��Ҫ�� Physics.Raycast ��ʹ�õ�ͼ�㡣


    [Header("States Check")] //ʹ�ø� PropertyAttribute �� Inspector �е�ĳЩ�ֶ��Ϸ���ӱ��⡣
    //������unity�в鿴�ű�ʱ�����б���������States Check ��
    public bool isGround;//������
    public bool canJump;


    


    // Start is called before the first frame update
    //start������Ϸ��ʼʱִ��һ��
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();//��ý�ɫ����
    }

    // Update is called once per frame
    //update����ÿһִ֡��һ��
    //ʵ�ʲ�����update��Ӧʵ�ʲ��������������
    void Update()
    {
        checkinput();
    }

    private void FixedUpdate()  //�̶�ʱ��ִ�У�һ��1Sִ��50�Σ�ʵ�ʲ�����Fixedupdate��Ӧ����ִ�еķ���
    {
        PhysicsCheck(); 
        Movement();
        jump();
    }

    void checkinput() {//������� 
        if (Input.GetButtonDown("Jump")&&isGround)//���û������� buttonName ��ʶ�����ⰴť��֡�ڼ䷵�� true��
        {
            canJump = true;
        }
    }

    void Movement()//�����ƶ�
    {
        //float horizontalInput = Input.GetAxis("Horizontal");  //���������� ��ֵ��С -1~1 ����С��
        float horizontalInput = Input.GetAxisRaw("Horizontal"); //���������᣻ֵ��С -1~1 ������С��
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);  //�����ٶȣ������ٶȸ��ģ����򲻱�

        
        if (horizontalInput != 0) {//�������������������﷭ת
            transform.localScale = new Vector3(horizontalInput, 1, 1);//ͨ����������Scale����ά����ʵ��
        }
    }
    
    void jump()
    {
        if (canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);//����rb.velocity.xĿ�����ý�ɫ����Ծʱ��������λ��
            rb.gravityScale = 4;  //����ʱ����������СΪ4
            canJump = false;
        }
    }

    void PhysicsCheck() //������
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);//����Ƿ��ڵ�����
        if (isGround) 
        {
            rb.gravityScale = 1; //����ʱ����������СΪ1
        }
    }

    public void OnDrawGizmos()//��unity����ʾ��ⷶΧ;Unity���ú���
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);//����Բ�μ�ⷶΧ
    }

}

