using UnityEngine;
using UnityEngine.UI;

public class BlowerController : MonoBehaviour
{
    public Slider progressBar; // ������
    public float progressPerClick = 5f; // ÿ�ε�����ӵĽ���
    public Animator blowerAnimator; // �ķ������
    public float decayRate = 5f; // ÿ����ٵĽ���
    public float smoothSpeed = 5f; // ƽ���ٶ�

    private float targetProgress = 0f;
    void Start()
    {
        targetProgress = 0f;
        progressBar.value = 0f;
    }
    void Update()
    {
        // �Զ���������
        targetProgress -= decayRate * Time.deltaTime;
        targetProgress = Mathf.Clamp(targetProgress, 0, progressBar.maxValue);

        // ƽ���ƶ�������
        progressBar.value = Mathf.Lerp(progressBar.value, targetProgress, Time.deltaTime * smoothSpeed);
    }

    void OnMouseDown()
    {
        // ���Źķ綯��
        if (blowerAnimator != null)
        {
            blowerAnimator.SetTrigger("Blow");
        }

        // �ƽ�������
        if (progressBar != null)
        {
            targetProgress += progressPerClick;
            targetProgress = Mathf.Clamp(targetProgress, 0, progressBar.maxValue);
        }
    }
}
