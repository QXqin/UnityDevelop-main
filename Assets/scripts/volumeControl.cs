using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class volumeControl : MonoBehaviour
{
    //��Ҫ���Ƶ�������ʲô
    private AudioSource menuAudio;
    //��ȡ��������
    //��ȡ��������
    private Slider volumnSlider;
    void Start()
    {
        menuAudio = GameObject.FindGameObjectWithTag("mainMenu").transform.GetComponent<AudioSource>();//ͨ����ǩ������
        volumnSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        volumnCtrl();
        closeGameSettingUI();
     
    }

    //����������Ч
    public void volumnCtrl()
    {
        menuAudio.volume = volumnSlider.value;
    }


    //close gamesetting screen
    public void closeGameSettingUI()
    {
        if(Input.GetKey(KeyCode.Escape))
        {//���˵�
            GameObject mainMenu = GameObject.FindGameObjectWithTag("mainMenu").transform.gameObject;
            //��Ϸ���ý���
            GameObject gameSettingUI = GameObject.FindGameObjectWithTag("options").transform.gameObject;

            mainMenu.transform.GetChild(0).gameObject.SetActive(true);

            gameSettingUI.transform.GetChild(0).gameObject.SetActive(false);
        }

    }
}
