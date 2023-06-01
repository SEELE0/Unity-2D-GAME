using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    private Animator anim;
    // private Animation anim;
    
    // public bool shoot;
    [Header("炮弹预制体")]
    public GameObject BombPrefab;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false); //禁用炮台
        if (ToolSession.CurrentTool.Contains("大炮"))
        {
            Debug.Log("大炮");
            gameObject.SetActive(true); //启用炮台
            anim = GetComponent<Animator>();
        }
        
        
        // anim = GetComponent<Animator>();
        // anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        ShootConnon();
    }

    bool IsPlayerNearby()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);//检测半径为1的圆形区域内的碰撞体
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }
    
    void LaunchBomb() //发射炸弹 anim event
    {
        // Vector3 cannonPos = transform.position; //获取炮台位置
        
        GameObject bomb = Instantiate(BombPrefab,transform.position , Quaternion.identity); //实例化炸弹
        
        // GameObject bomb = Instantiate(BombPrefab, transform.position, transform.rotation);//实例化炸弹
        bomb.transform.position = transform.position; //炸弹位置为炮台位置
        bomb.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10f, ForceMode2D.Impulse); //给炸弹一个向右的力
    }

    void ShootConnon()
    {
        if (Input.GetKeyDown(KeyCode.R) && IsPlayerNearby())
        {
            /*shoot = true;
            anim.SetBool("shoot", shoot);*/
            anim.SetTrigger("shoot");
            // anim.enabled = true;
            // anim.Play("attack");
            // LaunchBomb();
            // shoot = false;
            
            //关闭动画
            // anim.enabled = false;
        }
    }
}
