using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 引入SceneManagement命名空间

public class buttonGame2 : buttonCtrl
{
    protected override void OnButtonClickEvent()
    {
        base.OnButtonClickEvent();

        Debug.Log("game2Respond");

        // 跳转到名为"music"的场景
        SceneManager.LoadScene("music");
    }
}