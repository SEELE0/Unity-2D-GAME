using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    
    
    //单例模式
    /*
     * 单例模式:
     * 1.构造函数私有化
     * 2.提供一个静态的公共的方法，返回一个实例
     * 3.提供一个静态的私有的实例
     * 4.在静态的公共的方法中判断实例是否为空，如果为空则创建一个实例
     *
     *什么是单例模式:
     * 保证一个类只有一个实例，并提供一个访问它的全局访问点
     * 好处:
     * 1.节省内存空间
     * 2.避免对资源的多重占用
     * 3.方便数据共享
     * 4.方便进行资源管理
     * 5.方便进行全局控制
     * 6.方便进行全局访问
     * 7.方便进行全局更新
     * 8.方便进行全局销毁
     * 9.方便进行全局初始化
     * 10.方便进行全局重置
     * 11.方便进行全局清理
     * 12.方便进行全局释放
     * 13.方便进行全局回收
     * 14.方便进行全局重用
     * 15.方便进行全局重载
     * 16.方便进行全局重写
     * 17.方便进行全局重启
     * 18.方便进行全局重置


     */
    
    public static UIManager Instance;  //单例模式 静态变量常驻内存
    public GameObject healthBar; //血条
    
    [Header("UI元素")] //UI元素
    public GameObject pauseMenu; //暂停面板
    public Slider bossHealthBar; //Boss血条
    
    public void Awake() //Awake()方法在Start()方法之前执行
    {
        /*目的:场景切换时不删除gameManager
        所以要进行判断防止出现两个manager*/
        if (Instance == null)  //判断实例是否为空
        {
            Instance = this; //如果为空则创建一个实例
        }
        else
        {
            Destroy(gameObject); //如果不为空则销毁当前实例
        }
        

    }

    public void UpdateHealth(float currentHealth)
    {
        // int i=0;
        switch (currentHealth)
        {
            case 3:
                int i=0;
                while (i<3)
                {
                    healthBar.transform.GetChild(i).gameObject.SetActive(true);//获取子物体
                    i++;
                }
                break;
            case 2:
                healthBar.transform.GetChild(0).gameObject.SetActive(true);
                healthBar.transform.GetChild(1).gameObject.SetActive(true);
                healthBar.transform.GetChild(2).gameObject.SetActive(false);
                break;
            case 1:
                healthBar.transform.GetChild(0).gameObject.SetActive(true);
                healthBar.transform.GetChild(1).gameObject.SetActive(false);
                healthBar.transform.GetChild(2).gameObject.SetActive(false);
                break;
            case 0:
                int x=0;
                while (x<3)
                {
                    healthBar.transform.GetChild(x).gameObject.SetActive(false);//获取子物体
                    x++;
                }
                break;
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true); //显示暂停面板
        
        Time.timeScale = 0; //暂停游戏
    }
    
    public void ResumeGame()
    {
        pauseMenu.SetActive(false); //隐藏暂停面板
        
        Time.timeScale = 1; //恢复游戏
    }
    
    public void SetBossHealth(float health)
    {
        bossHealthBar.maxValue = health;
    }

    public void UpdateBossHealth(float health)
    {
        bossHealthBar.value = health;
    }

}
