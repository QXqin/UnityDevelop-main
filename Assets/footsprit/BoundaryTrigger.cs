using UnityEngine;

// 边界触发脚本：销毁超出边界的苹果和石头，并通知 GameManager
public class BoundaryTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 判断是否为石头或苹果
        bool isStone = other.CompareTag("Stone");
        bool isApple = (other.GetComponent<AppleController>() != null);

        if (isStone || isApple)
        {
            // 通知 GameManager 对应物体被销毁
            if (isStone)
            {
                AppleGame.GameManager.Instance.StoneDestroyed();
            }
            else if (isApple)
            {
                AppleGame.GameManager.Instance.AppleDestroyed();
            }
            // 销毁物体
            Destroy(other.gameObject);
        }
    }
}
