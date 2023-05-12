using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersSetup : MonoBehaviour
{
    public PlayerScriptableObject[]         playerInfos;
    public Snake    snakePrefab;
    public GameGrid gameGrid;
    List<Snake>     _allPlayers             = new List<Snake>();
    bool            _started;
    TimeManager     _timeManager;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i< playerInfos.Length; i++)
        {
            CreateASnake(playerInfos[i].upKey, playerInfos[i].leftKey, playerInfos[i].rightKey,
                playerInfos[i].downKey,
                playerInfos[i].playerColor, i);
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
            _started = true;
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
        if(_timeManager == null)
        {
            _timeManager = gameObject.AddComponent<TimeManager>();
        }
        TimeManager.OnTimesUp.AddListener(MoveSnakes);

        gameGrid.CreateAnApple();
    }

    void MoveSnakes()
    {
        for (int i = 0; i < _allPlayers.Count; i++)
        {
            _allPlayers[i].MoveNext();
        }
    }

    void OnDestroy()
    {
        if (_timeManager != null)
        {
            TimeManager.OnTimesUp.RemoveAllListeners();
        }
    }
}
