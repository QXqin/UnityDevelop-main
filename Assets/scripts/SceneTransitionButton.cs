using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneTransitionButton : MonoBehaviour
{
    [Header("UI ���")]
    public Button enterButton;
    public Image fadeImage;

    [Header("����")]
    public string sceneToLoad = "main";
    public float fadeDuration = 1f;

    private NoteSpawner noteSpawner;

    void Start()
    {
        // 1. �󶨰�ť
        if (enterButton != null)
            enterButton.onClick.AddListener(OnEnterButtonClick);

        // 2. ��ʼ��Ļȫ͸��
        if (fadeImage != null)
        {
            var c = fadeImage.color; c.a = 0; fadeImage.color = c;
        }

        // 3. �ҵ�������ʵ��
        noteSpawner = FindObjectOfType<NoteSpawner>();
    }

    void OnEnterButtonClick()
    {
        // ��ֹ��ε�
        enterButton.interactable = false;

        // ���� ֹͣ������ ���� 
        if (noteSpawner != null)
        {
            noteSpawner.enabled = false;       // ���ýű�
            noteSpawner.StopAllCoroutines();   // ֹͣЭ��
        }

        // ���� �������� Note ʵ�� ���� 
        foreach (var note in GameObject.FindGameObjectsWithTag("Note"))
            Destroy(note);

        // ���� ��������� MissEffect ���� 


        // ���� �������г��� ���� 
        fadeImage.DOFade(1f, fadeDuration)
                 .OnComplete(() => SceneManager.LoadScene(sceneToLoad));
    }
}
