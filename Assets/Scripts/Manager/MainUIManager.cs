using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIManager : MonoBehaviour
{
    public static MainUIManager Instance;  //单例模式 静态变量常驻内存
    [Header("配置登录失败提示")]
    public GameObject loginFailText;

    public void Awake() //Awake()方法在Start()方法之前执行
    {
        UserSession.CurrentUser = null; //初始化当前用户
        // ToolSession.CurrentTool = null; //初始化当前工具
        ToolSession.CurrentTool = new List<string>(); //初始化当前工具
        
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

    public void loginFail()
    {
        loginFailText.SetActive(true); //登录失败提示
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(1); //加载主菜单场景
    }

}
