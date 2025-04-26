using UnityEngine;

// �߽紥���ű������ٳ����߽��ƻ����ʯͷ����֪ͨ GameManager
public class BoundaryTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // �ж��Ƿ�Ϊʯͷ��ƻ��
        bool isStone = other.CompareTag("Stone");
        bool isApple = (other.GetComponent<AppleController>() != null);

        if (isStone || isApple)
        {
            // ֪ͨ GameManager ��Ӧ���屻����
            if (isStone)
            {
                AppleGame.GameManager.Instance.StoneDestroyed();
            }
            else if (isApple)
            {
                AppleGame.GameManager.Instance.AppleDestroyed();
            }
            // ��������
            Destroy(other.gameObject);
        }
    }
}
