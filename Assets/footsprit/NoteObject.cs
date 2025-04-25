using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    private bool hasBeenPressed;

    public KeyCode keyToPress;
    public GameObject hitEffect, goodEffect, missEffect, perfectEffect;

    [Header("超出底部自动销毁的 Y 阈值")]
    public float despawnY = -7f;

    // 新增：缓存本次判定使用的 Activator 的 Y 坐标
    private float activatorY;

    void Update()
    {
        // 1) 出界销毁
        if (transform.position.y < despawnY)
        {
            Destroy(gameObject);
            return;
        }

        // 2) 只有在可按下的状态下才判定
        if (Input.GetKeyDown(keyToPress) && canBePressed)
        {
            // 3) 计算与判定区的绝对距离
            float distance = Mathf.Abs(transform.position.y - activatorY);

            // 4) 标记并隐藏自身，避免重复判定
            hasBeenPressed = true;
            canBePressed = false;
            gameObject.SetActive(false);

            // 5) 按距离从小到大判定
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
            // 进入判定区
            canBePressed = true;
            // 缓存判定区的 Y
            activatorY = other.transform.position.y;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Activator"))
        {
            canBePressed = false;

            // 如果在离开时还没按过，就算 Miss
            if (!hasBeenPressed)
            {
                GameManager.instance.NoteMissed();
                Instantiate(missEffect, transform.position, missEffect.transform.rotation);
            }
        }
    }
}
