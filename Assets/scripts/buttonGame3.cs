using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonGame3 : buttonCtrl
{
    [Header("������ʾ���")]
    public GameObject popupPanel;          // ָ�򳡾������ص���ʾ���
    public CanvasGroup popupCanvasGroup;   // �����붯���õ� CanvasGroup
    public CanvasGroup fade;   // ��fade�����õ� CanvasGroup
    public float popupFadeDuration = 0.5f; // ����ʱ��

    [Header("��������")]
    public string sceneToLoad = "cooking"; // Ҫ���صĳ�����

    private void Start()
    {
        // ����ʼ������͸��
        if (popupPanel != null)
            popupPanel.SetActive(false);

        if (popupCanvasGroup != null)
            popupCanvasGroup.alpha = 0f;
    }

    protected override void OnButtonClickEvent()
    {
        base.OnButtonClickEvent();
        // ����Э�̣��ȵ�����壬�ٵȰ����г���
        StartCoroutine(ShowPopupAndLoad());
    }

    private IEnumerator ShowPopupAndLoad()
    {
        // 1. ������岢����͸����
        if (popupPanel != null)
            popupPanel.SetActive(true);

        if (popupCanvasGroup != null)
        {
            popupCanvasGroup.alpha = 0f;          // ��ȫ͸����ʼ
            popupCanvasGroup.blocksRaycasts = true; // �赲���������Ҫ��
            // 2. ִ�е��붯��
            yield return popupCanvasGroup
                .DOFade(1f, popupFadeDuration)    // alpha 0��1
                .SetEase(Ease.Linear)
                .WaitForCompletion();             // �ȶ������
        }

        // 3. �ȴ������
        Debug.Log("�ȴ������������");
        while (!Input.anyKeyDown)
            yield return null;
        if (popupCanvasGroup != null)
        {
            popupCanvasGroup.alpha = 1f;          // ��ȫ͸����ʼ
            popupCanvasGroup.blocksRaycasts = true; // �赲���������Ҫ��
            // 2. ִ�е��붯��
            yield return popupCanvasGroup
                .DOFade(0f, popupFadeDuration)    // alpha 0��1
                .SetEase(Ease.Linear)
                .WaitForCompletion();             // �ȶ������
        }
        if (fade != null)
        {
            fade.alpha = 0f;          // ��ȫ͸����ʼ
            popupCanvasGroup.blocksRaycasts = true; // �赲���������Ҫ��
            // 2. ִ�е��붯��
            yield return fade
                .DOFade(1f, popupFadeDuration)    // alpha 0��1
                .SetEase(Ease.Linear)
                .WaitForCompletion();             // �ȶ������
        }
        Debug.Log("��⵽�������л�����");
        // 4. �г���
        SceneManager.LoadScene(sceneToLoad);
    }
}
