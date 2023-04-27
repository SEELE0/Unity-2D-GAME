/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdsButton : MonoBehaviour, IUnityAdsListener
{
    private string gameID;
    
#if UNITY_IOS
     gameID = "5257300";
#elif UNITY_ANDROID
     gameID = "5257301";
#endif

    Button adsButton;

    // 播放广告的类型 奖赏广告
    public string placementId = "Rewarded_Android";


    void Start()
    {
        adsButton = GetComponent<Button>();

        // // 广告是否已经在后台的服务器加载就绪，如果加载好是可以播放的，按钮可以点按
        // adsButton.interactable = Advertisement.IsReady(placementId);
        // 当Button按下的时候为Button添加函数方法
        if (adsButton)
            adsButton.onClick.AddListener(ShowRewardAds);

        // 广告初始化
        // Advertisement.AddListener(this);
        Advertisement.Initialize(gameID, false, this);
    }

    // button监听的函数方法
    public void ShowRewardAds()
    {
        // 显示广告
        Advertisement.Show(placementId);
    }

    public void OnUnityAdsReady(string placementId)
    {
        if (Advertisement.IsReady(placementId))
            Debug.Log("广告准备就绪！");
    }

    public void OnUnityAdsDidError(string message)
    {
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    // 广告播放结束的时候
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Failed: break;
            case ShowResult.Skipped: break;
            case ShowResult.Finished:
                Debug.Log("广告播放结束，发放奖励！");
                // 玩家血值重新改为3
                FindObjectOfType<Playercontorller>().health = 3;
                // 修改死亡状态
                FindObjectOfType<Playercontorller>().isDead = false;
                // 更新血量UI
                UIManager.Instance.UpdateHealth(FindObjectOfType<Playercontorller>().health);
                break;
        }
    }
}*/