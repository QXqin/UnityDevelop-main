using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEndManager : MonoBehaviour
{
    [Header("引用")]
    public Slider progressBar;                // 你的进度条
    public GameObject gameCompletePanel;      // 刚才创建的通关面板
    public MonoBehaviour[] disableOnComplete; // 完成后要禁用的脚本列表

    [Header("可选特效")]
    public AudioClip victorySound;            // 胜利音效
    public ParticleSystem confetti;           // 彩带/烟花特效

    private bool _isCompleted = false;
    private AudioSource _audioSource;

    void Start()
    {
        gameCompletePanel.SetActive(false);
        _audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (_isCompleted) return;

        // 当进度条到达 99% 及以上，就当作已经到顶
        float threshold = progressBar.maxValue * 0.97f;
        if (progressBar.value >= threshold)
        {
            // 强制钉死到最大值
            progressBar.value = progressBar.maxValue;

            _isCompleted = true;
            OnGameComplete();
        }
    }


    void OnGameComplete()
    {

        // 1) 播放音效
        if (victorySound != null)
            _audioSource.PlayOneShot(victorySound);

        // 2) 播放粒子特效
        if (confetti != null)
            confetti.Play();

        // 3) 显示通关面板
        gameCompletePanel.SetActive(true);

        // 4) 禁用继续操作（如鼓风机脚本、StageManager 等）
        foreach (var mb in disableOnComplete)
        {
            if (mb != null) mb.enabled = false;
        }
    }

    // 在 UI Button 的 OnClick 里拖这个方法来重载场景
    public void OnRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
