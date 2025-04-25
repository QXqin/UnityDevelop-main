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
        //load scene ���ص��ǳ����±꣨��build setting���Ҳ�scene���±� �˴�menuΪ0����
        StartCoroutine(LoadScene(1));
        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

    }
    //��ʼ��Ϸ
    private IEnumerator LoadScene(int index)
    {
        animator.SetBool("fadein", true);
        animator.SetBool("fadeout", false);

        yield return new WaitForSeconds(1);

        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(index);
        // ���Ƴ��ɶ��ģ���ֹ�ظ�
        asyncOp.completed -= OnLoadScene;
        // ������¶���
        asyncOp.completed += OnLoadScene;
    }

    private void OnLoadScene(AsyncOperation op)
    {
        // ȷ�� animator ��Ȼ����
        if (animator != null)
        {
            animator.SetBool("fadein", false);
            animator.SetBool("fadeout", true);
        }
        // ȡ�����ģ������´��ظ�����
        op.completed -= OnLoadScene;
    }


    //��Ϸ����
    public void openGameSetting()
    {
        GameObject mainMenu = GameObject.FindGameObjectWithTag("mainMenu").transform.gameObject;//get �����˵��Ķ���
        //��Ϸ���ý���
        GameObject optionUI = GameObject.FindGameObjectWithTag("options").transform.gameObject;//get ��ѡ��˵��Ķ���

        mainMenu.transform.GetChild(0).gameObject.SetActive(false);

        optionUI.transform.GetChild(0).gameObject.SetActive(true);
    }

    //�˳���Ϸ
    public void exitGame()
    {

        Application.Quit();
    }
}



