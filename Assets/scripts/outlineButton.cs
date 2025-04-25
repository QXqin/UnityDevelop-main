using UnityEngine;

public class SpriteAutoOutline : MonoBehaviour
{
    [Header("�����ɫ")]
    public Color outlineColor = new Color(1f, 1f, 0.3f, 1f); // �����Ļ�ɫ
    [Header("��߿�ȣ����絥λ��")]
    public float outlineWidth = 0.12f; // ����

    private SpriteRenderer sr;
    private SpriteRenderer[] outlineSRs = new SpriteRenderer[4];
    private Vector3[] dirs = { Vector3.up, Vector3.down, Vector3.left, Vector3.right };

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        CreateOutlines();
        SetOutlinesActive(false);
    }

    void CreateOutlines()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject go = new GameObject("Outline_" + i);
            go.transform.SetParent(transform);
            go.transform.localPosition = dirs[i] * outlineWidth;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;

            var osr = go.AddComponent<SpriteRenderer>();
            osr.sprite = sr.sprite;
            osr.color = outlineColor;
            osr.sortingLayerID = sr.sortingLayerID;
            osr.sortingOrder = sr.sortingOrder - 1; // �ײ�
            osr.drawMode = sr.drawMode;
            osr.size = sr.size;
            outlineSRs[i] = osr;
        }
    }

    void SetOutlinesActive(bool active)
    {
        foreach (var osr in outlineSRs)
            osr.gameObject.SetActive(active);
    }

    void UpdateOutlines()
    {
        for (int i = 0; i < 4; i++)
        {
            var osr = outlineSRs[i];
            if (osr == null) continue;
            osr.sprite = sr.sprite;
            osr.color = outlineColor;
            osr.flipX = sr.flipX;
            osr.flipY = sr.flipY;
            osr.drawMode = sr.drawMode;
            osr.size = sr.size;
            osr.transform.localScale = Vector3.one; // ��֤������
            osr.sortingLayerID = sr.sortingLayerID;
            osr.sortingOrder = sr.sortingOrder - 1;
            osr.transform.localPosition = dirs[i] * outlineWidth;
        }
    }

    void OnMouseEnter() => SetOutlinesActive(true);
    void OnMouseExit() => SetOutlinesActive(false);

    void Update()
    {
        // ͬ����Ҫ���ԣ���ֹSprite/��С/Layer�ȷ����仯
        UpdateOutlines();
    }
}