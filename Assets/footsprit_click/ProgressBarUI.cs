using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [Header("����")]
    public Image fillImage; // ֻ��Ҫ����õ� Image

    private float _targetFill = 0f;
    private float _fillVelocity = 0f;
    public float smoothTime = 0.2f; // ƽ��ʱ��

    void Update()
    {
        if (fillImage == null) return;

        // ƽ������
        float currentFill = fillImage.fillAmount;
        float nextFill = Mathf.SmoothDamp(currentFill, _targetFill, ref _fillVelocity, smoothTime);
        fillImage.fillAmount = nextFill;
    }

    public void SetTargetFill(float normalized)
    {
        _targetFill = Mathf.Clamp01(normalized);
    }
}
