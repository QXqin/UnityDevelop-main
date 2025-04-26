using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    [Header("������ & �׶� GameObject")]
    public Slider progressBar;
    public GameObject[] stages;       // ���� 5 ���� SpriteRenderer �� GameObject

    [Header("����/����ʱ�� (��)")]
    public float fadeDuration = 0.5f;

    private SpriteRenderer[] _renders;
    private int _currentStage = 0;
    private bool _isFading = false;

    void Start()
    {
        // ������׶ε� SpriteRenderer
        _renders = new SpriteRenderer[stages.Length];
        for (int i = 0; i < stages.Length; i++)
        {
            _renders[i] = stages[i].GetComponent<SpriteRenderer>();
        }

        // ��ʼ����ֻ�е� 0 �׶οɼ�
        for (int i = 0; i < stages.Length; i++)
        {
            if (i == 0)
            {
                stages[i].SetActive(true);
                SetAlpha(_renders[i], 1f);
            }
            else
            {
                stages[i].SetActive(false);
                SetAlpha(_renders[i], 0f);
            }
        }
    }

    void Update()
    {
        if (_isFading) return;

        int newStage = GetStageByProgress(progressBar.value);
        if (newStage != _currentStage)
            StartCoroutine(FadeStageSequence(_currentStage, newStage));
    }

    int GetStageByProgress(float progress)
    {
        float t = progress / progressBar.maxValue;
        if (t >= 0.8f) return 4;
        if (t >= 0.6f) return 3;
        if (t >= 0.4f) return 2;
        if (t >= 0.2f) return 1;
        return 0;
    }

    IEnumerator FadeStageSequence(int fromIdx, int toIdx)
    {
        _isFading = true;

        // ȷ���½׶α����͸��
        stages[toIdx].SetActive(true);
        SetAlpha(_renders[toIdx], 0f);

        // ���� �ȵ����ɽ׶� ����
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float f = elapsed / fadeDuration;
            SetAlpha(_renders[fromIdx], Mathf.Lerp(1f, 0f, f));
            yield return null;
        }
        // ����״̬
        SetAlpha(_renders[fromIdx], 0f);
        stages[fromIdx].SetActive(false);

        // ���� �ٵ����½׶� ����
        elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float f = elapsed / fadeDuration;
            SetAlpha(_renders[toIdx], Mathf.Lerp(0f, 1f, f));
            yield return null;
        }
        SetAlpha(_renders[toIdx], 1f);

        _currentStage = toIdx;
        _isFading = false;
    }

    // ������ֻ�ľ���� alpha
    void SetAlpha(SpriteRenderer rend, float a)
    {
        if (rend == null) return;
        Color c = rend.color;
        c.a = Mathf.Clamp01(a);
        rend.color = c;
    }
}
