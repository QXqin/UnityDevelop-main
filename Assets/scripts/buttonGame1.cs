using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ����SceneManagement�����ռ�
using DG.Tweening;
public class buttonGame1 : buttonCtrl
{
    protected override void OnButtonClickEvent()
    {
        base.OnButtonClickEvent();

        Debug.Log("game1Respond");

        // ��ת����Ϊ"music"�ĳ���
        SceneManager.LoadScene("slingShot");
    }
 
}