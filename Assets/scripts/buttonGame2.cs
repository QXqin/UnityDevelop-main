using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ����SceneManagement�����ռ�

public class buttonGame2 : buttonCtrl
{
    protected override void OnButtonClickEvent()
    {
        base.OnButtonClickEvent();

        Debug.Log("game2Respond");

        // ��ת����Ϊ"music"�ĳ���
        SceneManager.LoadScene("music");
    }
}