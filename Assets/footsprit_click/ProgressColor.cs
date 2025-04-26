using UnityEngine;
using UnityEngine.UI;

public class ProgressColor : MonoBehaviour
{
    public Slider progressBar;
    public Image fillImage;

    public Color coldColor = Color.blue;
    public Color hotColor = Color.red;

    void Update()
    {
        float t = progressBar.value / progressBar.maxValue;
        fillImage.color = Color.Lerp(coldColor, hotColor, t);
    }
}
