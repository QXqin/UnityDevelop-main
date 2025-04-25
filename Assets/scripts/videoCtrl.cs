using UnityEngine;
using UnityEngine.SceneManagement;

public class videoCtrl : MonoBehaviour
{
    public float countTime = 0;

    void Update()
    {
        countTime += Time.deltaTime;
        if (countTime > 30.0f)
        {
            SceneManager.LoadScene(2);
        }
    }

    // 这个方法给按钮 OnClick 调用
    public void Skip()
    {
        countTime += 30f;
        Debug.Log("跳过 30 秒，当前 time = " + countTime);
    }
}