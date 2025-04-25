using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class audioGetMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //laoding new senario, wont destory object
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        updateGameObjValue();
        getGameObjValue();
    }

    //get and value u want
    public void updateGameObjValue()
    {
        //获取激活的场景 
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.name == "menu")
        {
            GameObject audioSlider = GameObject.FindGameObjectWithTag("options").transform.GetChild(0).GetChild(1).GetChild(1).gameObject;
            PlayerPrefs.SetFloat("volumnValue", audioSlider.GetComponent<Slider>().value);
            print(PlayerPrefs.GetFloat("volumnValue"));
        }
    }

    //赋值
    public void getGameObjValue()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.name == "main")
        {
            GameObject gameAudio = GameObject.Find("audioCtrl").transform.gameObject;//得到音量物体
            gameAudio.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("volumnValue");
        }
        if (activeScene.name == "video")
        {
            GameObject gameAudio = GameObject.Find("videocg").transform.gameObject;//得到音量物体
            gameAudio.GetComponent<VideoPlayer>().SetDirectAudioVolume(0, PlayerPrefs.GetFloat("volumnValue"));
        }
        if (activeScene.name == "slingShot")
        {
            GameObject gameAudio = GameObject.Find("audioCtrl").transform.gameObject;//得到音量物体
            gameAudio.GetComponent<AudioSource>().volume =  PlayerPrefs.GetFloat("volumnValue");
        }
    }
  }
