using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //�������
    private Animator ani;
    //�������
    private Rigidbody2D rBody;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        rBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //��ȡˮƽ��
        float horizontal = Input.GetAxisRaw("Horizontal");
        //��ȡ��ֱ��
        float vertical = Input.GetAxisRaw("Vertical");
        //���������
        if(horizontal!=0)
        {
            ani.SetFloat("Horizontal", horizontal);
            ani.SetFloat("Vertical", 0);
        }
        //�����ϻ���
        if (vertical != 0)
        {
            ani.SetFloat("Vertical", vertical);
            ani.SetFloat("Horizontal", 0);
        }
        //�л��ܲ�
        Vector2 dir = new Vector2(horizontal, vertical);
        ani.SetFloat("Speed", dir.magnitude);
        //���÷����ƶ�
        rBody.velocity = dir * 0.9f;

    }
}
