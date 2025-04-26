using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video; // �����Ƶ���������ռ�
using UnityEngine.UI;    // ���UI�����ռ�
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    [Header("��������ͼ�񣬰� ID=1,2,3 ˳������")]
    [SerializeField] GameObject[] endImages; // ���� = 3

    [Header("��Ƶ��������")]
    [SerializeField] VideoPlayer victoryVideoPlayer; // ��Ƶ���������
    [SerializeField] RawImage videoScreen;          // ��Ƶ��ʾ�õ�RawImage
    [SerializeField] AudioSource backgroundMusic;   // �������֣���ѡ��

    [Header("�ӳ�Ч��")]
    [SerializeField] float fadeDuration = 1f;          // ����ʱ��
    [SerializeField] Image delayOverlay;               // ��͸������
    [SerializeField] Text countdownText;               // ����ʱ�ı�

    IEnumerator PlayVictorySequence()
    {
        // ������ʾ����
        float timer = 0;
        while (timer < fadeDuration)
        {
            delayOverlay.color = Color.Lerp(Color.clear, new Color(0, 0, 0, 0.8f), timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        // ��ʾ3�뵹��ʱ
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        // ���ص���ʱԪ��
        countdownText.gameObject.SetActive(false);

        // ������Ƶ��������...
    }
    void Start()
    {
        // ��ʼ����Ƶϵͳ
        InitializeVideoSystem();

        // 1. ��ȫ������
        foreach (var go in endImages)
            go.SetActive(false);

        // 2. ��ʾ����ɵ���Ϸͼ��
        int completedCount = 0;
        for (int i = 1; i <= endImages.Length; i++)
        {
            if (GameStatus.IsCompleted(i))
            {
                endImages[i - 1].SetActive(true);
                completedCount++;
            }
        }

        // 3. ���ȫ������򲥷���Ƶ
        if (completedCount >= 3)
        {
            StartCoroutine(PlayVictoryVideo());
        }
    }

    void InitializeVideoSystem()
    {
        // ȷ����Ƶ�����ʼ״̬
        if (videoScreen != null)
        {
            videoScreen.gameObject.SetActive(false);
            videoScreen.color = Color.clear;
        }

        // ������Ƶ������
        if (victoryVideoPlayer != null)
        {
            victoryVideoPlayer.playOnAwake = false;
            victoryVideoPlayer.isLooping = false;
            victoryVideoPlayer.loopPointReached += OnVideoEnd;
        }
    }

    System.Collections.IEnumerator PlayVictoryVideo()
    {
        // ��ͣ��������
        if (backgroundMusic != null)
        {
            backgroundMusic.Pause();
        }

        // ׼����Ƶ
        victoryVideoPlayer.Prepare();
        while (!victoryVideoPlayer.isPrepared)
        {
            yield return null;
        }

        // ��ʾ��Ƶ����
        videoScreen.gameObject.SetActive(true);
        videoScreen.color = Color.white;
        videoScreen.texture = victoryVideoPlayer.texture;

        // ��ʼ����
        victoryVideoPlayer.Play();

        // ��������UIԪ�أ���ѡ��
        foreach (var img in endImages)
        {
            img.SetActive(false);
        }
    }

    void OnVideoEnd(VideoPlayer source)
    {
        // �ָ���������
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }

        // ������Ƶ����
        videoScreen.gameObject.SetActive(false);

        // ������ʾ���ͼ��
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
        // ֹͣ��Ƶ���ţ�������ڲ��ţ�
        if (victoryVideoPlayer != null && victoryVideoPlayer.isPlaying)
        {
            victoryVideoPlayer.Stop();
            OnVideoEnd(victoryVideoPlayer);
        }

        SceneManager.LoadScene(sceneName);
    }
}