using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneTransitionButton : MonoBehaviour
{
    [Header("UI组件")]
    public Button enterButton;       // 按钮
    public Image fadeImage;          // 用来淡出的全屏黑色 Image

    [Header("设置")]
    public string sceneToLoad = "main"; // 要加载的场景名
    public float fadeDuration = 1f;     // 淡出时长

    private void Start()
    {
        // 确保按钮绑定事件
        if (enterButton != null)
            enterButton.onClick.AddListener(OnEnterButtonClick);

        // 初始化为透明
        if (fadeImage != null)
        {
            var c = fadeImage.color;
            c.a = 0;
            fadeImage.color = c;
        }
    }

    private void OnEnterButtonClick()
    {
        // 防止多次点击重复调用
        enterButton.interactable = false;

        // 1. 清除所有带有 "Note" 标签的对象
        ClearAllNotes();

        // 2. 执行淡出动画，然后加载场景
        fadeImage.DOFade(1f, fadeDuration)
            .OnComplete(() =>
            {
                SceneManager.LoadScene(sceneToLoad);
            });
    }

    /// <summary>
    /// 查找并销毁场景中所有带有 "Note" 标签的 GameObject
    /// </summary>
    private void ClearAllNotes()
    {
        // 查找所有带有 Tag = "Note" 的物体
        GameObject[] notes = GameObject.FindGameObjectsWithTag("Note");
        foreach (var note in notes)
        {
            Destroy(note);
        }
    }
}
