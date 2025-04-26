using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct LevelInfo
{
    public string key;      // �� GameManager �е� levelKey ��Ӧ
    public GameObject icon; // Inspector ����д����ɡ�ͼ�����
}

public class LevelSelectManager : MonoBehaviour
{
    public LevelInfo[] levels;

    void Start()
    {
        UpdateCompletedIcons();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateCompletedIcons();
    }

    private void UpdateCompletedIcons()
    {
        foreach (var lvl in levels)
        {
            // ��ȡƫ�ã�����������Ĭ�� 0��δ��ɣ�
            bool done = PlayerPrefs.GetInt(lvl.key + "_Completed", 0) == 1;
            if (lvl.icon != null)
                lvl.icon.SetActive(done);  // ���������ͼ��
        }
    }
}
