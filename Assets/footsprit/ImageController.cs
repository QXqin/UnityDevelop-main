using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageController : MonoBehaviour
{
    private SpriteRenderer theSR;

    [Header("��������")]
    public Sprite defaultImage;
    public Sprite pressedImage;

    [Header("��������")]
    public KeyCode keyToPress;

    [Header("λ�ñ�ǣ������������Ϊ��ǣ�")]
    [Tooltip("����ʱҪ�ƶ����ı�ǵ�")]
    public Transform pressedMarker;

    private Vector3 defaultPosition;

    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();

        // ��¼��ʼλ�ã��ɿ�ʱ�ָ�
        defaultPosition = transform.position;

        // ȷ��һ��ʼ��ʾĬ�Ͼ���
        theSR.sprite = defaultImage;
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            // �л�������״̬�ľ���
            theSR.sprite = pressedImage;
            // �ƶ���ָ����ǵ�
            if (pressedMarker != null)
                transform.position = pressedMarker.position;
        }

        if (Input.GetKeyUp(keyToPress))
        {
            // �ָ�Ĭ�Ͼ���
            theSR.sprite = defaultImage;
            // �ָ���ԭʼλ��
            transform.position = defaultPosition;
        }
    }
}
