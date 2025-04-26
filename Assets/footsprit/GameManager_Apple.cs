using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // 加这个，因为要用Text
namespace AppleGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public GameObject winPanel;
        public GameObject losePanel;
        public Text timerText; // 新增：计时器UI
        public Text winInfoText; // 新增：胜利界面里显示剩余信息的文本

        private int totalApples;
        private int totalStones;
        private int currentApples;
        private int currentStones;

        private float timeLimit = 60f; // 总时间
        private float currentTime;
        private bool timerRunning = true;
        private bool gameEnded = false;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        void Start()
        {
            totalApples = GameObject.FindGameObjectsWithTag("Apple").Length;
            totalStones = GameObject.FindGameObjectsWithTag("Stone").Length;
            currentApples = totalApples;
            currentStones = totalStones;

            currentTime = timeLimit;
            timerRunning = true;
            gameEnded = false;

            winPanel.SetActive(false);
            losePanel.SetActive(false);
        }

        void Update()
        {
            if (timerRunning && !gameEnded)
            {
                currentTime -= Time.deltaTime;
                if (timerText != null)
                {
                    timerText.text = Mathf.CeilToInt(currentTime).ToString() + "s";
                }

                if (currentTime <= 0)
                {
                    currentTime = 0;
                    timerRunning = false;
                    GameOver();
                }
            }
        }

        public void AppleDestroyed()
        {
            currentApples--;
            if (currentApples <= 0)
            {
                Victory();
                Victory_end();
            }
        }

        public void StoneDestroyed()
        {
            currentStones--;


            if (currentStones <= 0 && currentApples > 0)
            {
                GameOver();
            }
        }

        public void Victory()
        {
            // 调整字体大小和居中
            winInfoText.fontSize = 100; // 设置字号
            winInfoText.alignment = TextAnchor.MiddleCenter; // 水平和垂直居中

            // 通过代码控制 RectTransform 的锚点和位置（可选）
            RectTransform textRect = winInfoText.GetComponent<RectTransform>();
            textRect.anchorMin = new Vector2(0.5f, 0.5f); // 锚点居中
            textRect.anchorMax = new Vector2(0.5f, 0.5f);
            textRect.anchoredPosition = Vector2.zero; // 位置归零
            if (winPanel != null)
            {
                winPanel.SetActive(true);

                if (winInfoText != null)
                {
                    int timeLeft = Mathf.CeilToInt(currentTime);
                    winInfoText.text = $"剩余时间：{timeLeft}s\n\n剩余石子：{currentStones}";
                }
            }
            timerRunning = false;
            gameEnded = true;

        }

        public void GameOver()
        {
            if (losePanel != null)
            {
                losePanel.SetActive(true);

                var canvas = losePanel.GetComponentInParent<Canvas>();
                if (canvas != null)
                {
                    canvas.overrideSorting = true;
                    canvas.sortingOrder = 100;
                }
                losePanel.transform.SetAsLastSibling();
            }
            timerRunning = false;
            gameEnded = true;

            SceneManager.LoadScene("main");
            winPanel.SetActive(false);
            losePanel.SetActive(false);
        }

        private void Victory_end()
        {
            Invoke("DelayedLoadMainScene", 3f);
            GameStatus.MarkCompleted(1);
            //winPanel.SetActive(false);
            //losePanel.SetActive(false);
            SceneManager.LoadScene("main");
        }

    }
}