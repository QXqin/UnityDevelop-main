using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneTransitionButton : MonoBehaviour
{
    [Header("UI 组件")]
    public Button enterButton;
    public Image fadeImage;

    [Header("设置")]
    public string sceneToLoad = "main";
    public float fadeDuration = 1f;

    private NoteSpawner noteSpawner;

    void Start()
    {
        // 1. 绑定按钮
        if (enterButton != null)
            enterButton.onClick.AddListener(OnEnterButtonClick);

        // 2. 初始黑幕全透明
        if (fadeImage != null)
        {
            var c = fadeImage.color; c.a = 0; fadeImage.color = c;
        }

        // 3. 找到生成器实例
        noteSpawner = FindObjectOfType<NoteSpawner>();
    }

    void OnEnterButtonClick()
    {
        // 防止多次点
        enterButton.interactable = false;

        // —— 停止生成器 —— 
        if (noteSpawner != null)
        {
            noteSpawner.enabled = false;       // 禁用脚本
            noteSpawner.StopAllCoroutines();   // 停止协程
        }

        // —— 销毁所有 Note 实例 —— 
        foreach (var note in GameObject.FindGameObjectsWithTag("Note"))
            Destroy(note);

        // —— 清理残留的 MissEffect —— 


        // —— 淡出再切场景 —— 
        fadeImage.DOFade(1f, fadeDuration)
                 .OnComplete(() => SceneManager.LoadScene(sceneToLoad));
    }
}
