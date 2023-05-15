using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
            CreateASnake(playerInfos[i].inputActionAsset,
                playerInfos[i].playerColor, i);
        }
    }

    void CreateASnake(InputActionAsset inputActionAsset, Color color, int id)
    {
        if(snakePrefab != null)
        {
            Snake s = Instantiate(snakePrefab);
            s.Setup(inputActionAsset, color, id);
            _allPlayers.Add(s);
        }
    }

    /// <summary>
    /// remove the snake from the list
    /// </summary>
    /// <param name="snake"></param>
    void OnSnakeDead(Snake snake)
    {
        if(_allPlayers.Contains(snake))
        {
            _allPlayers.Remove(snake);
        }
        if(_allPlayers.Count == 0 && GameManager.Instance)
        {
            GameManager.Instance.GameOver();
            StopMoving();
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
                OnSnakeDead(_allPlayers[i]);
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
