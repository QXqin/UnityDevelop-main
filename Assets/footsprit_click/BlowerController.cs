using UnityEngine;
using UnityEngine.UI;

public class BlowerController : MonoBehaviour
{
    public Slider progressBar;
    public ParticleSystem progressParticle; // ���� Handle �µ�����ϵͳ
    public ProgressBarUI progressBarUI;

    public float progressPerClick = 5f;
    public float decayRate = 5f;
    public float smoothSpeed = 50f;
    public Animator blowerAnimator;

    private float targetProgress = 0f;
    private ParticleSystem.EmissionModule _emission;
    [Tooltip("����100%ʱ�����������")]
    public float maxEmissionRate = 50f;

    void Start()
    {
        _emission = progressParticle.emission;
        _emission.rateOverTime = 0f;

        // ���ӻ�������
        var main = progressParticle.main;
        main.startSpeed = 4f;
        main.startSize = 0.4f;
        main.startLifetime = 0.8f;
        main.startColor = Color.yellow; // Ĭ�ϻ�ɫ��Ȼ�� Color Over Lifetime �ٱ仯��

        progressBar.value = 0f;
        targetProgress = 0f;
        if (progressBarUI != null)
            progressBarUI.SetTargetFill(0f);
    }



    void Update()
    {
        // ���Ȼ���
        targetProgress = Mathf.Clamp(
            targetProgress - decayRate * Time.deltaTime,
            0f, progressBar.maxValue);

        // ƽ���ƽ� Slider
        progressBar.value = Mathf.MoveTowards(
            progressBar.value,
            targetProgress,
            smoothSpeed * Time.deltaTime);

        // 3) ��̬�������ӷ�������
        float normalized = progressBar.value / progressBar.maxValue;
        _emission.rateOverTime = normalized * maxEmissionRate;
    }

    void OnMouseDown()
    {
        if (blowerAnimator != null)
            blowerAnimator.SetTrigger("Blow");

        // ����Ŀ�����
        targetProgress = Mathf.Clamp(
            targetProgress + progressPerClick,
            0f, progressBar.maxValue);

        // ���ʱ���ʱ�������
        if (progressParticle != null)
        {
            float chance = Random.value; // 0 ~ 1֮��������
            if (chance < 0.3f) // 30%�ĸ���
            {
                progressParticle.Emit(30); // ����30������
            }
        }
    }


}