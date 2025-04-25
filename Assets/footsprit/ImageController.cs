using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageController : MonoBehaviour
{
    private SpriteRenderer theSR;

    [Header("精灵设置")]
    public Sprite defaultImage;
    public Sprite pressedImage;

    [Header("按键设置")]
    public KeyCode keyToPress;

    [Header("位置标记（拖入空物体作为标记）")]
    [Tooltip("按下时要移动到的标记点")]
    public Transform pressedMarker;

    private Vector3 defaultPosition;

    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();

        // 记录初始位置，松开时恢复
        defaultPosition = transform.position;

        // 确保一开始显示默认精灵
        theSR.sprite = defaultImage;
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            // 切换到按下状态的精灵
            theSR.sprite = pressedImage;
            // 移动到指定标记点
            if (pressedMarker != null)
                transform.position = pressedMarker.position;
        }

        if (Input.GetKeyUp(keyToPress))
        {
            // 恢复默认精灵
            theSR.sprite = defaultImage;
            // 恢复到原始位置
            transform.position = defaultPosition;
        }
    }
}
