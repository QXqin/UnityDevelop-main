using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public Slider progressBar;
    public GameObject[] stages; // Stage0 ~ Stage4

    private int currentStage = -1;

    void Update()
    {
        int stage = GetStageByProgress(progressBar.value);

        if (stage != currentStage)
        {
            SwitchToStage(stage);
            currentStage = stage;
        }
    }

    int GetStageByProgress(float progress)
    {
        float percent = progress / progressBar.maxValue;

        if (percent >= 0.8f) return 4;
        if (percent >= 0.6f) return 3;
        if (percent >= 0.4f) return 2;
        if (percent >= 0.2f) return 1;
        return 0;
    }

    void SwitchToStage(int stageIndex)
    {
        for (int i = 0; i < stages.Length; i++)
        {
            stages[i].SetActive(i == stageIndex);
        }
    }
}
