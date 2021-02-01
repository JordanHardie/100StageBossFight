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

    void Death()
    {
        UI_Manager.Instance.Game_over(0);
        GameEvents.ReportGameStateChange(GameState.GAMEOVER);
    }

    void Hit(bool _hit)
    {
        if(_hit && lives > 1)
        {
            lives -= 1;
            UI_Manager.Instance.Hit();  
        }

        else
        {
            Death();
        }
    }

    void GameStateChanged(GameState _gameState)
    {
        gameState = _gameState;
    }

    void OnGraze(bool isGrazing)
    {
        if (isGrazing)
        {
            UI_Manager.Instance.Graze();
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
        GameEvents.OnGameStateChange += GameStateChanged;
        GameEvents.OnHit += Hit;
        GameEvents.OnGraze += OnGraze;
    }

    void OnDisable()
    {
        GameEvents.OnHit -= Hit;
        GameEvents.OnGraze -= OnGraze;
    }
    #endregion
}