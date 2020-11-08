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

    //Start in the menu by defualt,
    //However since this script is accross multiple scences,
    //It will be set to menu everytime. Will probably fix later somehow.
    void Start()
    {
        gameState = GameState.MENU;
        GameEvents.ReportGameStateChange(gameState);
        GameEvents.ReportDifficultyChanged(difficulty);
    }

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