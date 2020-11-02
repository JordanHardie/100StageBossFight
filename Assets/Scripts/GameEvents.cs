using System;

public static class GameEvents
{
    public static event Action<Difficulty> OnDifficultyChange = null;
    public static event Action<GameState> OnGameStateChange = null;
    public static event Action<bool> OnGraze = null;

    public static void ReportDifficultyChanged(Difficulty _difficulty)
    {
        OnDifficultyChange?.Invoke(_difficulty);
    }

    public static void ReportGameStateChange(GameState _gameState)
    {
        OnGameStateChange?.Invoke(_gameState);
    }

    public static void ReportGrazeChange(bool _graze)
    {
        OnGraze?.Invoke(_graze);
    }
}