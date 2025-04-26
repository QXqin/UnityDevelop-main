public static class GameStatus
{
    public const int GameCount = 3;               // һ������С��Ϸ
    public static bool[] CompletedGames           // ÿ����Ϸ����ɱ�־
        = new bool[GameCount];

    /// <summary>
    /// ��ǵ� n ����Ϸ����ɣ�n �� 1 ��ʼ��
    /// </summary>
    public static void MarkCompleted(int gameId)
    {
        if (gameId >= 1 && gameId <= GameCount)
            CompletedGames[gameId - 1] = true;
    }

    /// <summary>
    /// ��ѯ�� n ����Ϸ�Ƿ����
    /// </summary>
    public static bool IsCompleted(int gameId)
    {
        return gameId >= 1
            && gameId <= GameCount
            && CompletedGames[gameId - 1];
    }
}
