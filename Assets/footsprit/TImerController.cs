using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
// ��Ҫ���� static System.Net.Mime ���޹�����

public class TimerController : MonoBehaviour
{
    [Header("��ʱ������")]
    public float timeLimit = 60f;

    [Header("UI ����")]
    public UnityEngine.UI.Text timerText;

    private float currentTime;
    private bool isTimerRunning;

    void Start()
    {
        currentTime = timeLimit;
        isTimerRunning = true;

        // ��鵥���Ƿ����
        if (AppleGame.GameManager.Instance == null)
        {
            ;
        }
            //Debug.LogError("δ�ҵ� GameManager ������");
    }

    void Update()
    {
        if (!isTimerRunning) return;

        currentTime -= Time.deltaTime;
        UpdateTimerUI();

        if (currentTime <= 0f)
        {
            isTimerRunning = false;
            currentTime = 0f;
            // ��ȫ�޶������ռ䣬��ֹ���������õ���� GameManager
            AppleGame.GameManager.Instance.GameOver();
        }
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
            timerText.text = Mathf.CeilToInt(currentTime) + "s";
    }
}
