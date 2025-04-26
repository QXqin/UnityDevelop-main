using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
// 不要再用 static System.Net.Mime 等无关引用

public class TimerController : MonoBehaviour
{
    [Header("计时器设置")]
    public float timeLimit = 60f;

    [Header("UI 引用")]
    public UnityEngine.UI.Text timerText;

    private float currentTime;
    private bool isTimerRunning;

    void Start()
    {
        currentTime = timeLimit;
        isTimerRunning = true;

        // 检查单例是否就绪
        if (AppleGame.GameManager.Instance == null)
        {
            ;
        }
            //Debug.LogError("未找到 GameManager 单例！");
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
            // 完全限定命名空间，防止编译器引用到别的 GameManager
            AppleGame.GameManager.Instance.GameOver();
        }
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
            timerText.text = Mathf.CeilToInt(currentTime) + "s";
    }
}
