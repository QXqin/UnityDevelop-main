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

    // �����������ť OnClick ����
    public void Skip()
    {
        countTime += 30f;
        Debug.Log("���� 30 �룬��ǰ time = " + countTime);
    }
}