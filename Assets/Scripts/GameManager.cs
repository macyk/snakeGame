using UnityEngine;

/// <summary>
/// manage the flow of the game
/// </summary>
public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// the grid of the map
    /// </summary>
    public  GameGrid        gameGrid;
    /// <summary>
    /// manages all players
    /// </summary>
    public  PlayerManager   playerManager;

    bool    _started;

    void Start()
    {
        if(gameGrid != null)
        {
            gameGrid.GenerateGrids();
        }
    }

    void Update()
    {
        if (!_started && Input.GetKeyDown(KeyCode.Space))
        {
            _started = true;
            StartGame();
        }
    }

    void StartGame()
    {
        if (gameGrid == null)
        {
            Debug.LogError("no game grid");
        }
        else
        {
            if (playerManager != null)
            {
                playerManager.StartPlayers(gameGrid);
            }

            gameGrid.CreateAnApple();
        }
    }

    /// <summary>
    /// game ends
    /// </summary>
    /// <param name="deadSnake">the snake who died</param>
    public void GameOver(Snake deadSnake)
    {
        _started = false;
        Debug.Log("game over : "+ deadSnake.GetID());
    }
}
