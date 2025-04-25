using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class volumeControl : MonoBehaviour
{
    //需要控制的声音是什么
    private AudioSource menuAudio;
    //获取控制声音
    //获取到滑动条
    private Slider volumnSlider;
    void Start()
    {
        menuAudio = GameObject.FindGameObjectWithTag("mainMenu").transform.GetComponent<AudioSource>();//通过标签名查找
        volumnSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        volumnCtrl();
        closeGameSettingUI();
     
    }

    //控制声音音效
    public void volumnCtrl()
    {
        menuAudio.volume = volumnSlider.value;
    }


    //close gamesetting screen
    public void closeGameSettingUI()
    {
        if(Input.GetKey(KeyCode.Escape))
        {//主菜单
            GameObject mainMenu = GameObject.FindGameObjectWithTag("mainMenu").transform.gameObject;
            //游戏设置界面
            GameObject gameSettingUI = GameObject.FindGameObjectWithTag("options").transform.gameObject;

            mainMenu.transform.GetChild(0).gameObject.SetActive(true);

            gameSettingUI.transform.GetChild(0).gameObject.SetActive(false);
        }

    }
}
