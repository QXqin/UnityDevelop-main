using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("音源 & 播放控制")]
    public AudioSource theMusic;
    public bool startPlaying;

    [HideInInspector]
    public bool spawningFinished = false;

    [Header("滚动控制")]
    public BeatScroller theBS;

    [Header("UI & 统计")]
    public Text scoreText, multiText;
    public GameObject resultsScreen;
    public Text percentHitText, normalsText, goodsText, perfectsText, missesText, rankText, finalScoreText;

    [Header("分数&连击")]
    public int currentScore, scorePerNote = 100, scorePerGoodNote = 125, scorePerPerfectNote = 150;
    public int currentMultiplier, multiplierTracker;
    public int[] multiplierThresholds;

    [Header("命中统计")]
    public float totalNotes, normalHits, goodHits, perfecHits, missedHits;

    [Header("屏幕震动")]
    public CameraShake camShake;



    private void Awake() => instance = this;

    void Start()
    {
        scoreText.text = "Score: 0";
        currentMultiplier = 1;
        totalNotes = 0;
        spawningFinished = false;
        resultsScreen.SetActive(false);
    }

    void Update()
    {
        if (!startPlaying && Input.anyKeyDown)
        {
            startPlaying = true;
            theBS.hasStarted = true;

            // 确保不循环
            theMusic.loop = false;
            theMusic.Play();

            Debug.Log("GameManager: startPlaying = true, music started.");

            // 启动结束检测协程
            StartCoroutine(CheckForEnd());
        }
    }

    private IEnumerator CheckForEnd()
    {
        Debug.Log("CheckForEnd: waiting for spawningFinished...");
        yield return new WaitUntil(() => spawningFinished);
        Debug.Log("CheckForEnd: spawningFinished == true");

        // 等所有带 Note 标签的物体消失
        while (GameObject.FindGameObjectsWithTag("Note").Length > 0)
        {
            //Debug.Log($"CheckForEnd: notes remaining = {GameObject.FindGameObjectsWithTag("Note").Length}");
            yield return new WaitForSeconds(0.5f);
        }
        //Debug.Log("CheckForEnd: all notes cleared");

        // 停音乐并显示结果
        if (theMusic.isPlaying) theMusic.Stop();
        ShowResults();
    }

    private void ShowResults()
    {
<<<<<<< Updated upstream
=======
      
        GameStatus.MarkCompleted(3);
        SceneManager.LoadScene("main");

        resultsScreen.SetActive(true);
>>>>>>> Stashed changes
        Debug.Log("ShowResults: displaying result screen");
        resultsScreen.SetActive(true);

        normalsText.text = normalHits.ToString();
        goodsText.text = goodHits.ToString();
        perfectsText.text = perfecHits.ToString();
        missesText.text = missedHits.ToString();

        float hitCount = normalHits + goodHits + perfecHits;
        float percent = totalNotes > 0 ? (hitCount / totalNotes) * 100f : 0f;
        percentHitText.text = percent.ToString("F1") + "%";

        string rank = "F";
        if (percent > 40) rank = "D";
        if (percent > 55) rank = "C";
        if (percent > 70) rank = "B";
        if (percent > 85) rank = "A";
        if (percent > 95) rank = "S";
        if (percent == 100) rank = "SS";
        if (percent == 100 && hitCount == totalNotes) rank = "SSS";
        rankText.text = rank;
        finalScoreText.text = currentScore.ToString();
    }


    public void NormalHit() { currentScore += scorePerNote * currentMultiplier; NoteHit(); normalHits++; StartShake(); }
    public void GoodHit() { currentScore += scorePerGoodNote * currentMultiplier; NoteHit(); goodHits++; StartShake(); }
    public void PerfectHit() { currentScore += scorePerPerfectNote * currentMultiplier; NoteHit(); perfecHits++; StartShake(); }
    public void NoteMissed() { currentMultiplier = 1; multiplierTracker = 0; multiText.text = "Multiplier: x" + currentMultiplier; missedHits++; }

    private void NoteHit()
    {
        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;
            if (multiplierTracker >= multiplierThresholds[currentMultiplier - 1])
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }
        multiText.text = "Multiplier: x" + currentMultiplier;
        scoreText.text = "Score: " + currentScore;
    }

    private void StartShake()
    {
        if (camShake != null)
            StartCoroutine(camShake.Shake());
    }
}
