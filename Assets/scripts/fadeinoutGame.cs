using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeInOnStart : MonoBehaviour
{
    public Image fadeImage;          // 拖拽黑幕 Image
    public float fadeDuration = 2.5f;  // 动画时长

    void Start()
    {
        if (fadeImage != null)
        {
            fadeImage.raycastTarget = true;
            fadeImage.color = new Color(0, 0, 0, 1); // 确保初始是黑的

            // 淡入（黑 ➜ 透明）
            fadeImage.DOFade(0f, fadeDuration).OnComplete(() =>
            {
                fadeImage.raycastTarget = false; // 淡入完成后可交互
            });
        }
    }
}
