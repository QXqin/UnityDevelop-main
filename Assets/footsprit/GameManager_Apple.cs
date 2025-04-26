using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // ���������ΪҪ��Text
namespace AppleGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public GameObject winPanel;
        public GameObject losePanel;
        public Text timerText; // ��������ʱ��UI
        public Text winInfoText; // ������ʤ����������ʾʣ����Ϣ���ı�

        private int totalApples;
        private int totalStones;
        private int currentApples;
        private int currentStones;

        private float timeLimit = 60f; // ��ʱ��
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
            // ���������С�;���
            winInfoText.fontSize = 100; // �����ֺ�
            winInfoText.alignment = TextAnchor.MiddleCenter; // ˮƽ�ʹ�ֱ����

            // ͨ��������� RectTransform ��ê���λ�ã���ѡ��
            RectTransform textRect = winInfoText.GetComponent<RectTransform>();
            textRect.anchorMin = new Vector2(0.5f, 0.5f); // ê�����
            textRect.anchorMax = new Vector2(0.5f, 0.5f);
            textRect.anchoredPosition = Vector2.zero; // λ�ù���
            if (winPanel != null)
            {
                winPanel.SetActive(true);

                if (winInfoText != null)
                {
                    int timeLeft = Mathf.CeilToInt(currentTime);
                    winInfoText.text = $"ʣ��ʱ�䣺{timeLeft}s\n\nʣ��ʯ�ӣ�{currentStones}";
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