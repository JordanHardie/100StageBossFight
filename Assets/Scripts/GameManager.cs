public enum GameState
{
    TITLE,
    MENU,
    INGAME,
    PAUSED,
    GAMEOVER
}

public enum Difficulty
{
    EASY,
    MEDIUM,
    HARD,
    INSANE,
    HEAVEN
}

public class GameManager : Singleton<GameManager>
{
    public GameState gameState;
    public Difficulty difficulty;

    void Start()
    {
        gameState = GameState.TITLE;
        GameEvents.ReportGameStateChange(gameState);
        GameEvents.ReportDifficultyChanged(difficulty);
    }
}