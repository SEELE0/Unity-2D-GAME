using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public static  GameManager Instance;//单例模式 静态变量常驻内存
    
    private Playercontorller player;
    private Door doorExit;

    public bool gameover ;
    
    public List<NPC> npcs = new List<NPC>(); //NPC列表
    public void Awake() //在脚本实例化时调用
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        
        else
        {
            Destroy(gameObject);
        }

        /*player = FindObjectOfType<Playercontorller>(); //访问场景中的Playercontorller组件的变量和函数
        doorExit  = FindObjectOfType<Door>(); //访问场景中的Door组件的变量和函数*/
    }

    public void Update()
    {
        gameover = player.isDead; //判断玩家是否死亡
        UIManager.Instance.GameOverUI(gameover);
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //重新加载当前场景
        PlayerPrefs.DeleteKey("playerHealth"); //删除存储的Health，恢复血量
    }
    
    //观察者模式
    /*
     * 1.观察者模式是一种设计模式，它定义了对象之间的一对多依赖，这样一来，当一个对象改变状态，依赖它的对象都会收到通知并自动更新。
     * 2.观察者模式是一种对象行为型模式。
     * 3.观察者模式包含以下主要角色。
     */
    public void IsNpc(NPC npc) //判断NPC是否存在
    {
        
        npcs.Add(npc);
        /*if (!npcs.Contains(npc)) //如果NPC列表中不包含NPC
        {
            npcs.Add(npc);
        }*/
    }
    public void Isplayer(Playercontorller player)
    {
        this.player = player;
    }
    
    public void IsExitDoor(Door doorExit)
    {
        this.doorExit = doorExit;
    }
    
    public void NpcDead(NPC npc) //判断NPC是否死亡
    {
        /*if (npcs.Contains(npc)) //如果NPC列表中包含NPC
        {
            npcs.Remove(npc);
        }*/
        
        npcs.Remove(npc);
        if (npcs.Count == 0) //如果NPC列表中没有NPC,开门
        {
            doorExit.OpenDoor();  //调用Door脚本中的OpenDoor方法，开门
            SaveData(); //存储数据
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //加载下一个场景
    }
    
    public void QuitGame()
    {
        Application.Quit(); //退出游戏
    }

    public float LoadHealth()
    {
        
        
        if(!PlayerPrefs.HasKey("playerHealth")) //如果没有存储Health
        {
            PlayerPrefs.SetFloat("playerHealth",3f);
        }
        float currentHealth = PlayerPrefs.GetFloat("playerHealth"); //获取Health
        
        
        
        return currentHealth;
        
    }
    
    public void SaveData()
    {
        /*
         * 调试时需要
         * 编辑 -->重置所有playerPrefs  来清空缓存
         */
        
        PlayerPrefs.SetFloat("playerHealth",player.health); //存储Health
        PlayerPrefs.Save(); //构建后在相应位置保存数据
    }
    
    
}
