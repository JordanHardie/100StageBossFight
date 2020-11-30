using UnityEngine;

#region Enums
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
#endregion

public class GameManager : Singleton<GameManager>
{
    public GameState gameState;
    public Difficulty difficulty;
    public int lives;

    void Update()
    {
        if (lives < 1)
        {
            Death();
        }
    }

    // SHI-NE SCUM!
    void Death()
    {
        UI_Manager.Instance.Death();
        gameState = GameState.GAMEOVER;
        GameEvents.ReportGameStateChange(gameState);
    }

    void Hit(bool _hit)
    {
        if(_hit && lives >= 1)
        {
            lives -= 1;
            UI_Manager.Instance.Hit();  
        }
    }

    // Quit game
    public void QuitGame()
    {
        Application.Quit();
    }

    #region Event listening
    void OnEnable()
    {
        GameEvents.OnHit += Hit;
    }

    void OnDisable()
    {
        GameEvents.OnHit -= Hit;
    }
    #endregion
}