using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // 震动持续时间
    public float shakeDuration = 0.2f;
    // 震动强度
    public float shakeMagnitude = 0.3f;

    private Vector3 originalPos;

    void Awake()
    {
        // 记录摄像机初始位置
        originalPos = transform.localPosition;
    }

    public IEnumerator Shake()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            // 生成随机偏移
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // 恢复到初始位置
        transform.localPosition = originalPos;
    }
}
