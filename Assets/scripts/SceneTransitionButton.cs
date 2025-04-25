using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneTransitionButton : MonoBehaviour
{
    [Header("UI���")]
    public Button enterButton;       // ��ť
    public Image fadeImage;          // ����������ȫ����ɫ Image

    [Header("����")]
    public string sceneToLoad = "main"; // Ҫ���صĳ�����
    public float fadeDuration = 1f;     // ����ʱ��

    private void Start()
    {
        // ȷ����ť���¼�
        if (enterButton != null)
            enterButton.onClick.AddListener(OnEnterButtonClick);

        // ��ʼ��Ϊ͸��
        if (fadeImage != null)
        {
            var c = fadeImage.color;
            c.a = 0;
            fadeImage.color = c;
        }
    }

    private void OnEnterButtonClick()
    {
        // ��ֹ��ε���ظ�����
        enterButton.interactable = false;

        // 1. ������д��� "Note" ��ǩ�Ķ���
        ClearAllNotes();

        // 2. ִ�е���������Ȼ����س���
        fadeImage.DOFade(1f, fadeDuration)
            .OnComplete(() =>
            {
                SceneManager.LoadScene(sceneToLoad);
            });
    }

    /// <summary>
    /// ���Ҳ����ٳ��������д��� "Note" ��ǩ�� GameObject
    /// </summary>
    private void ClearAllNotes()
    {
        // �������д��� Tag = "Note" ������
        GameObject[] notes = GameObject.FindGameObjectsWithTag("Note");
        foreach (var note in notes)
        {
            Destroy(note);
        }
    }
}
