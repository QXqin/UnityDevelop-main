using UnityEngine;
using UnityEngine.UI;

public class BlowerController : MonoBehaviour
{
    public Slider progressBar; // 进度条
    public float progressPerClick = 5f; // 每次点击增加的进度
    public Animator blowerAnimator; // 鼓风机动画
    public float decayRate = 5f; // 每秒减少的进度
    public float smoothSpeed = 5f; // 平滑速度

    private float targetProgress = 0f;
    void Start()
    {
        targetProgress = 0f;
        progressBar.value = 0f;
    }
    void Update()
    {
        // 自动缓慢回落
        targetProgress -= decayRate * Time.deltaTime;
        targetProgress = Mathf.Clamp(targetProgress, 0, progressBar.maxValue);

        // 平滑移动进度条
        progressBar.value = Mathf.Lerp(progressBar.value, targetProgress, Time.deltaTime * smoothSpeed);
    }

    void OnMouseDown()
    {
        // 播放鼓风动画
        if (blowerAnimator != null)
        {
            blowerAnimator.SetTrigger("Blow");
        }

        // 推进进度条
        if (progressBar != null)
        {
            targetProgress += progressPerClick;
            targetProgress = Mathf.Clamp(targetProgress, 0, progressBar.maxValue);
        }
    }
}
