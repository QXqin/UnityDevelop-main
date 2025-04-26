using UnityEngine;
using UnityEngine.UI;

public class BlowerController : MonoBehaviour
{
    public Slider progressBar;
    public ParticleSystem progressParticle; // 挂在 Handle 下的粒子系统
    public ProgressBarUI progressBarUI;

    public float progressPerClick = 5f;
    public float decayRate = 5f;
    public float smoothSpeed = 50f;
    public Animator blowerAnimator;

    private float targetProgress = 0f;
    private ParticleSystem.EmissionModule _emission;
    [Tooltip("进度100%时的最大发射速率")]
    public float maxEmissionRate = 50f;

    void Start()
    {
        _emission = progressParticle.emission;
        _emission.rateOverTime = 0f;

        // 粒子基础调整
        var main = progressParticle.main;
        main.startSpeed = 4f;
        main.startSize = 0.4f;
        main.startLifetime = 0.8f;
        main.startColor = Color.yellow; // 默认黄色（然后 Color Over Lifetime 再变化）

        progressBar.value = 0f;
        targetProgress = 0f;
        if (progressBarUI != null)
            progressBarUI.SetTargetFill(0f);
    }



    void Update()
    {
        // 进度回落
        targetProgress = Mathf.Clamp(
            targetProgress - decayRate * Time.deltaTime,
            0f, progressBar.maxValue);

        // 平滑推进 Slider
        progressBar.value = Mathf.MoveTowards(
            progressBar.value,
            targetProgress,
            smoothSpeed * Time.deltaTime);

        // 3) 动态调整粒子发射速率
        float normalized = progressBar.value / progressBar.maxValue;
        _emission.rateOverTime = normalized * maxEmissionRate;
    }

    void OnMouseDown()
    {
        if (blowerAnimator != null)
            blowerAnimator.SetTrigger("Blow");

        // 更新目标进度
        targetProgress = Mathf.Clamp(
            targetProgress + progressPerClick,
            0f, progressBar.maxValue);

        // 点击时概率爆发粒子
        if (progressParticle != null)
        {
            float chance = Random.value; // 0 ~ 1之间的随机数
            if (chance < 0.3f) // 30%的概率
            {
                progressParticle.Emit(30); // 爆发30个粒子
            }
        }
    }


}