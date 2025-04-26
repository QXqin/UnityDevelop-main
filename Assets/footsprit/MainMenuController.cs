using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video; // 添加视频播放命名空间
using UnityEngine.UI;    // 添加UI命名空间
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    [Header("三个结束图像，按 ID=1,2,3 顺序拖入")]
    [SerializeField] GameObject[] endImages; // 长度 = 3

    [Header("视频播放设置")]
    [SerializeField] VideoPlayer victoryVideoPlayer; // 视频播放器组件
    [SerializeField] RawImage videoScreen;          // 视频显示用的RawImage
    [SerializeField] AudioSource backgroundMusic;   // 背景音乐（可选）

    [Header("延迟效果")]
    [SerializeField] float fadeDuration = 1f;          // 渐变时间
    [SerializeField] Image delayOverlay;               // 半透明遮罩
    [SerializeField] Text countdownText;               // 倒计时文本

    IEnumerator PlayVictorySequence()
    {
        // 渐变显示遮罩
        float timer = 0;
        while (timer < fadeDuration)
        {
            delayOverlay.color = Color.Lerp(Color.clear, new Color(0, 0, 0, 0.8f), timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        // 显示3秒倒计时
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        // 隐藏倒计时元素
        countdownText.gameObject.SetActive(false);

        // 继续视频播放流程...
    }
    void Start()
    {
        // 初始化视频系统
        InitializeVideoSystem();

        // 1. 先全部隐藏
        foreach (var go in endImages)
            go.SetActive(false);

        // 2. 显示已完成的游戏图标
        int completedCount = 0;
        for (int i = 1; i <= endImages.Length; i++)
        {
            if (GameStatus.IsCompleted(i))
            {
                endImages[i - 1].SetActive(true);
                completedCount++;
            }
        }

        // 3. 如果全部完成则播放视频
        if (completedCount >= 3)
        {
            StartCoroutine(PlayVictoryVideo());
        }
    }

    void InitializeVideoSystem()
    {
        // 确保视频组件初始状态
        if (videoScreen != null)
        {
            videoScreen.gameObject.SetActive(false);
            videoScreen.color = Color.clear;
        }

        // 配置视频播放器
        if (victoryVideoPlayer != null)
        {
            victoryVideoPlayer.playOnAwake = false;
            victoryVideoPlayer.isLooping = false;
            victoryVideoPlayer.loopPointReached += OnVideoEnd;
        }
    }

    System.Collections.IEnumerator PlayVictoryVideo()
    {
        // 暂停背景音乐
        if (backgroundMusic != null)
        {
            backgroundMusic.Pause();
        }

        // 准备视频
        victoryVideoPlayer.Prepare();
        while (!victoryVideoPlayer.isPrepared)
        {
            yield return null;
        }

        // 显示视频界面
        videoScreen.gameObject.SetActive(true);
        videoScreen.color = Color.white;
        videoScreen.texture = victoryVideoPlayer.texture;

        // 开始播放
        victoryVideoPlayer.Play();

        // 隐藏其他UI元素（可选）
        foreach (var img in endImages)
        {
            img.SetActive(false);
        }
    }

    void OnVideoEnd(VideoPlayer source)
    {
        // 恢复背景音乐
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }

        // 隐藏视频界面
        videoScreen.gameObject.SetActive(false);

        // 重新显示完成图标
        for (int i = 1; i <= endImages.Length; i++)
        {
            if (GameStatus.IsCompleted(i))
            {
                endImages[i - 1].SetActive(true);
            }
        }
        SceneManager.LoadScene("menu");
    }

    public void OnStartGame(string sceneName)
    {
        // 停止视频播放（如果正在播放）
        if (victoryVideoPlayer != null && victoryVideoPlayer.isPlaying)
        {
            victoryVideoPlayer.Stop();
            OnVideoEnd(victoryVideoPlayer);
        }

        SceneManager.LoadScene(sceneName);
    }
}