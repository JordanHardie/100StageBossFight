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

    void Hit(bool IsHit)
    {
        if(IsHit)
        {
            gameState = GameState.GAMEOVER;
            GameEvents.ReportGameStateChange(gameState);
        }
    }

    void OnEnable()
    {
        GameEvents.OnHit += Hit;
    }
}