using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    [Header("进度条 & 阶段 GameObject")]
    public Slider progressBar;
    public GameObject[] stages;       // 挂你 5 个带 SpriteRenderer 的 GameObject

    [Header("淡出/淡入时长 (秒)")]
    public float fadeDuration = 0.5f;

    private SpriteRenderer[] _renders;
    private int _currentStage = 0;
    private bool _isFading = false;

    void Start()
    {
        // 缓存各阶段的 SpriteRenderer
        _renders = new SpriteRenderer[stages.Length];
        for (int i = 0; i < stages.Length; i++)
        {
            _renders[i] = stages[i].GetComponent<SpriteRenderer>();
        }

        // 初始化：只有第 0 阶段可见
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

        // 确保新阶段被激活并透明
        stages[toIdx].SetActive(true);
        SetAlpha(_renders[toIdx], 0f);

        // —— 先淡出旧阶段 ——
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float f = elapsed / fadeDuration;
            SetAlpha(_renders[fromIdx], Mathf.Lerp(1f, 0f, f));
            yield return null;
        }
        // 最终状态
        SetAlpha(_renders[fromIdx], 0f);
        stages[fromIdx].SetActive(false);

        // —— 再淡入新阶段 ——
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

    // 辅助：只改精灵的 alpha
    void SetAlpha(SpriteRenderer rend, float a)
    {
        if (rend == null) return;
        Color c = rend.color;
        c.a = Mathf.Clamp01(a);
        rend.color = c;
    }
}
