using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEndManager : MonoBehaviour
{
    [Header("����")]
    public Slider progressBar;                // ��Ľ�����
    public GameObject gameCompletePanel;      // �ղŴ�����ͨ�����
    public MonoBehaviour[] disableOnComplete; // ��ɺ�Ҫ���õĽű��б�

    [Header("��ѡ��Ч")]
    public AudioClip victorySound;            // ʤ����Ч
    public ParticleSystem confetti;           // �ʴ�/�̻���Ч

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

        // ������������ 99% �����ϣ��͵����Ѿ�����
        float threshold = progressBar.maxValue * 0.97f;
        if (progressBar.value >= threshold)
        {
            // ǿ�ƶ��������ֵ
            progressBar.value = progressBar.maxValue;

            _isCompleted = true;
            OnGameComplete();
        }
    }


    void OnGameComplete()
    {

        // 1) ������Ч
        if (victorySound != null)
            _audioSource.PlayOneShot(victorySound);

        // 2) ����������Ч
        if (confetti != null)
            confetti.Play();

        // 3) ��ʾͨ�����
        gameCompletePanel.SetActive(true);

        // 4) ���ü�����������ķ���ű���StageManager �ȣ�
        foreach (var mb in disableOnComplete)
        {
            if (mb != null) mb.enabled = false;
        }
    }

    // �� UI Button �� OnClick ����������������س���
    public void OnRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
