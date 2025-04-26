using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [Header("引用")]
    public Image fillImage; // 只需要填充用的 Image

    private float _targetFill = 0f;
    private float _fillVelocity = 0f;
    public float smoothTime = 0.2f; // 平滑时间

    void Update()
    {
        if (fillImage == null) return;

        // 平滑过渡
        float currentFill = fillImage.fillAmount;
        float nextFill = Mathf.SmoothDamp(currentFill, _targetFill, ref _fillVelocity, smoothTime);
        fillImage.fillAmount = nextFill;
    }

    public void SetTargetFill(float normalized)
    {
        _targetFill = Mathf.Clamp01(normalized);
    }
}
