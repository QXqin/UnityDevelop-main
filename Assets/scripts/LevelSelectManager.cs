using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct LevelInfo
{
    public string key;      // 与 GameManager 中的 levelKey 对应
    public GameObject icon; // Inspector 中填写“完成”图标对象
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
            // 读取偏好：若不存在则默认 0（未完成）
            bool done = PlayerPrefs.GetInt(lvl.key + "_Completed", 0) == 1;
            if (lvl.icon != null)
                lvl.icon.SetActive(done);  // 激活或隐藏图标
        }
    }
}
