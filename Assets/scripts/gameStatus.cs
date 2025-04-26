public static class GameStatus
{
    public const int GameCount = 3;               // 一共三个小游戏
    public static bool[] CompletedGames           // 每个游戏的完成标志
        = new bool[GameCount];

    /// <summary>
    /// 标记第 n 个游戏已完成（n 从 1 开始）
    /// </summary>
    public static void MarkCompleted(int gameId)
    {
        if (gameId >= 1 && gameId <= GameCount)
            CompletedGames[gameId - 1] = true;
    }

    /// <summary>
    /// 查询第 n 个游戏是否完成
    /// </summary>
    public static bool IsCompleted(int gameId)
    {
        return gameId >= 1
            && gameId <= GameCount
            && CompletedGames[gameId - 1];
    }
}
