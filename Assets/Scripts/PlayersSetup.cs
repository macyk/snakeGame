using System.Collections.Generic;
using UnityEngine;

public class PlayersSetup : MonoBehaviour
{
    public int      numOfPlayers = 1;
    public Snake    snakePrefab;
    public GameGrid gameGrid;
    List<Snake>     _allPlayers = new List<Snake>();
    bool            _started;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i< numOfPlayers; i++)
        {
            CreateASnake(KeyCode.W, KeyCode.A, KeyCode.D,
                KeyCode.S,
                Color.red, i);
        }
        if(gameGrid != null)
        {
            gameGrid.GenerateGrids();
        }
    }

    void CreateASnake(KeyCode upKey, KeyCode leftKey, KeyCode rightKey,
        KeyCode downKey, Color color, int id)
    {
        if(snakePrefab != null)
        {
            Snake s = Instantiate(snakePrefab);
            s.Setup(upKey, leftKey, rightKey, downKey, color, id);
            _allPlayers.Add(s);
        }
    }

    void Update()
    {
        if(!_started && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    void StartGame()
    {
        if(gameGrid == null)
        {
            Debug.LogError("no game grid");
        }
        for (int i = 0; i < _allPlayers.Count; i++)
        {
            _allPlayers[i].StartMoving(gameGrid);
        }
    }
}
