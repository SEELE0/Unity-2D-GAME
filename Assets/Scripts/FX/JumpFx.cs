using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpFx : MonoBehaviour
{
    public void Finish()
    {
        gameObject.SetActive(false);//停用 GameObject 
/*目的:通过在帧动画中添加事件实现落地动画播放一次后消失，且落地动画与场景可以进行交互 */
    }
}
