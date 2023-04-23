using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public static  GameManager Instance;//单例模式 静态变量常驻内存
    
    private Playercontorller player;
    
    public bool gameover ;
    public void Awake() //在脚本实例化时调用
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }

        player = FindObjectOfType<Playercontorller>(); //访问场景中的Playercontorller组件的变量和函数
        
    }

    public void Update()
    {
        gameover = player.isDead; //判断玩家是否死亡
        UIManager.Instance.GameOverUI(gameover);
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //重新加载当前场景
    }
}
