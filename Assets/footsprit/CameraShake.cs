using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // �𶯳���ʱ��
    public float shakeDuration = 0.2f;
    // ��ǿ��
    public float shakeMagnitude = 0.3f;

    private Vector3 originalPos;

    void Awake()
    {
        // ��¼�������ʼλ��
        originalPos = transform.localPosition;
    }

    public IEnumerator Shake()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            // �������ƫ��
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // �ָ�����ʼλ��
        transform.localPosition = originalPos;
    }
}
