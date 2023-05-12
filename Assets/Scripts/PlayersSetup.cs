using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersSetup : MonoBehaviour
{
    public int numOfPlayers = 1;
    public Snake snakePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void CreateASnake(KeyCode upKey, KeyCode leftKey, KeyCode rightKey,
        KeyCode downKey)
    {
        if(snakePrefab != null)
        {
            Snake s = Instantiate(snakePrefab);
            s.Setup(upKey, leftKey, rightKey, downKey);
        }
    }
}
