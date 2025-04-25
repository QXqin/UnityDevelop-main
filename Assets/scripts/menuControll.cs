using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
public class menuControll : MonoBehaviour
{
    public Button btnStart;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        btnStart.onClick.AddListener(startGame);

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void startGame()
    {
        //load scene 加载的是场景下标（在build setting中右侧scene的下标 此处menu为0）；
        StartCoroutine(LoadScene(1));
        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

    }
    //开始游戏
    private IEnumerator LoadScene(int index)
    {
        animator.SetBool("fadein", true);
        animator.SetBool("fadeout", false);

        yield return new WaitForSeconds(1);

        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(index);
        // 先移除旧订阅，防止重复
        asyncOp.completed -= OnLoadScene;
        // 再添加新订阅
        asyncOp.completed += OnLoadScene;
    }

    private void OnLoadScene(AsyncOperation op)
    {
        // 确保 animator 仍然存在
        if (animator != null)
        {
            animator.SetBool("fadein", false);
            animator.SetBool("fadeout", true);
        }
        // 取消订阅，避免下次重复调用
        op.completed -= OnLoadScene;
    }


    //游戏设置
    public void openGameSetting()
    {
        GameObject mainMenu = GameObject.FindGameObjectWithTag("mainMenu").transform.gameObject;//get 到主菜单的对象
        //游戏设置界面
        GameObject optionUI = GameObject.FindGameObjectWithTag("options").transform.gameObject;//get 到选项菜单的对象

        mainMenu.transform.GetChild(0).gameObject.SetActive(false);

        optionUI.transform.GetChild(0).gameObject.SetActive(true);
    }

    //退出游戏
    public void exitGame()
    {

        Application.Quit();
    }
}



