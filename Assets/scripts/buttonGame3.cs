using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonGame3 : buttonCtrl
{
    [Header("弹出提示面板")]
    public GameObject popupPanel;          // 指向场景中隐藏的提示面板
    public CanvasGroup popupCanvasGroup;   // 做淡入动画用的 CanvasGroup
    public CanvasGroup fade;   // 做fade动画用的 CanvasGroup
    public float popupFadeDuration = 0.5f; // 淡入时长

    [Header("场景设置")]
    public string sceneToLoad = "cooking"; // 要加载的场景名

    private void Start()
    {
        // 面板初始隐藏且透明
        if (popupPanel != null)
            popupPanel.SetActive(false);

        if (popupCanvasGroup != null)
            popupCanvasGroup.alpha = 0f;
    }

    protected override void OnButtonClickEvent()
    {
        base.OnButtonClickEvent();
        // 启动协程：先淡入面板，再等按键切场景
        StartCoroutine(ShowPopupAndLoad());
    }

    private IEnumerator ShowPopupAndLoad()
    {
        // 1. 激活面板并重置透明度
        if (popupPanel != null)
            popupPanel.SetActive(true);

        if (popupCanvasGroup != null)
        {
            popupCanvasGroup.alpha = 0f;          // 从全透明开始
            popupCanvasGroup.blocksRaycasts = true; // 阻挡点击（如需要）
            // 2. 执行淡入动画
            yield return popupCanvasGroup
                .DOFade(1f, popupFadeDuration)    // alpha 0→1
                .SetEase(Ease.Linear)
                .WaitForCompletion();             // 等动画完毕
        }

        // 3. 等待任意键
        Debug.Log("等待任意键继续…");
        while (!Input.anyKeyDown)
            yield return null;
        if (popupCanvasGroup != null)
        {
            popupCanvasGroup.alpha = 1f;          // 从全透明开始
            popupCanvasGroup.blocksRaycasts = true; // 阻挡点击（如需要）
            // 2. 执行淡入动画
            yield return popupCanvasGroup
                .DOFade(0f, popupFadeDuration)    // alpha 0→1
                .SetEase(Ease.Linear)
                .WaitForCompletion();             // 等动画完毕
        }
        if (fade != null)
        {
            fade.alpha = 0f;          // 从全透明开始
            popupCanvasGroup.blocksRaycasts = true; // 阻挡点击（如需要）
            // 2. 执行淡入动画
            yield return fade
                .DOFade(1f, popupFadeDuration)    // alpha 0→1
                .SetEase(Ease.Linear)
                .WaitForCompletion();             // 等动画完毕
        }
        Debug.Log("检测到按键，切换场景");
        // 4. 切场景
        SceneManager.LoadScene(sceneToLoad);
    }
}
