using System.Collections;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [Header("四个下落点 (对应四列)")]
    public Transform[] spawnPoints;      // 必须和 notePrefabs 同长度
    [Header("四个 Note 预制体 (与下落点一一对应)")]
    public GameObject[] notePrefabs;     // 一一对应
    [Header("生成间隔")]
    public float minInterval = 0.3f;
    public float maxInterval = 1f;
    [Header("Note 生成容器")]
    public Transform noteHolder;

    private AudioSource musicSource;

    IEnumerator Start()
    {
        // 等待 GameManager 实例化完
        yield return new WaitUntil(() => GameManager.instance != null);
        musicSource = GameManager.instance.theMusic;

        // 确保音源不循环
        musicSource.loop = false;

        // 等待玩家触发开始
        yield return new WaitUntil(() => GameManager.instance.startPlaying);

        // 开始不断生成
        while (musicSource.isPlaying)
        {
            SpawnOneNote();
            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
        }

        // 歌曲结束：标记并打印日志
        Debug.Log("NoteSpawner: finished spawning all notes.");
        GameManager.instance.spawningFinished = true;
    }

    private void SpawnOneNote()
    {
        if (spawnPoints.Length != notePrefabs.Length || spawnPoints.Length == 0)
        {
            Debug.LogError("NoteSpawner：spawnPoints 和 notePrefabs 长度不匹配或为 0！");
            return;
        }

        int idx = Random.Range(0, spawnPoints.Length);
        Vector3 pos = spawnPoints[idx].position;
        pos.z = 0f;

        // 实例化并保留 Prefab 自带旋转
        GameObject note = Instantiate(
            notePrefabs[idx],
            pos,
            notePrefabs[idx].transform.rotation,
            noteHolder
        );

        // 给实例打标签，方便后续查找
        note.tag = "Note";

        // 累加统计
        GameManager.instance.totalNotes++;

        // 确保可见性
        var sr = note.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.enabled = true;
            sr.sortingLayerName = "Default";
            sr.sortingOrder = 100;
        }
    }
}
