using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    private bool hasBeenPressed;

    public KeyCode keyToPress;
    public GameObject hitEffect, goodEffect, missEffect, perfectEffect;

    [Header("�����ײ��Զ����ٵ� Y ��ֵ")]
    public float despawnY = -7f;

    // ���������汾���ж�ʹ�õ� Activator �� Y ����
    private float activatorY;

    void Update()
    {
        // 1) ��������
        if (transform.position.y < despawnY)
        {
            Destroy(gameObject);
            return;
        }

        // 2) ֻ���ڿɰ��µ�״̬�²��ж�
        if (Input.GetKeyDown(keyToPress) && canBePressed)
        {
            // 3) �������ж����ľ��Ծ���
            float distance = Mathf.Abs(transform.position.y - activatorY);

            // 4) ��ǲ��������������ظ��ж�
            hasBeenPressed = true;
            canBePressed = false;
            gameObject.SetActive(false);

            // 5) �������С�����ж�
            if (distance <= 0.05f)
            {
                // Perfect
                GameManager.instance.PerfectHit();
                Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
            }
            else if (distance <= 0.25f)
            {
                // Good
                GameManager.instance.GoodHit();
                Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
            }
            else
            {
                // Normal
                GameManager.instance.NormalHit();
                Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
            }
            

        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Activator"))
        {
            // �����ж���
            canBePressed = true;
            // �����ж����� Y
            activatorY = other.transform.position.y;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Activator"))
        {
            canBePressed = false;

            // ������뿪ʱ��û���������� Miss
            if (!hasBeenPressed)
            {
                GameManager.instance.NoteMissed();
                Instantiate(missEffect, transform.position, missEffect.transform.rotation);
            }
        }
    }
}
