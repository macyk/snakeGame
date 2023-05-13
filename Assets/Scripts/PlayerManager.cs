using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerScriptableObject[]         playerInfos;
    public Snake    snakePrefab;
    List<Snake>     _allPlayers             = new List<Snake>();
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

    public void StartPlayers(GameGrid gameGrid)
    {
        for (int i = 0; i < _allPlayers.Count; i++)
        {
            _allPlayers[i].StartMoving(gameGrid);
        }
        if (_timeManager == null)
        {
            _timeManager = gameObject.AddComponent<TimeManager>();
        }
        _timeManager.Restart();
        TimeManager.OnTimesUp.AddListener(MoveSnakes);
    }

    public void MoveSnakes()
    {
        for (int i = 0; i < _allPlayers.Count; i++)
        {
            //if one snake cannot move, we stop
            if(!_allPlayers[i].MoveNext())
            {
                StopMoving();
            }
        }
    }

    void StopMoving()
    {
        if (_timeManager != null)
        {
            _timeManager.Pause(true);
            TimeManager.OnTimesUp.RemoveAllListeners();
        }
    }

    void OnDestroy()
    {
        StopMoving();
    }
}
