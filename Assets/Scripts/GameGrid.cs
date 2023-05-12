using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGrid : MonoBehaviour
{
    public GridLayoutGroup  grid;
    public GameObject       spacePrefab;
    /// <summary>
    /// num of rows
    /// </summary>
    public int row;
    public int column;

    // Start is called before the first frame update
    void Start()
    {
        if(spacePrefab != null && grid != null)
        {
            for (int i = 0; i < row*column; i++)
            {
                GameObject space = Instantiate(spacePrefab);
                space.transform.SetParent(grid.transform);
            }

            grid.constraintCount = row;
            grid.spacing = new Vector2(1, 1);
        }
        
    }
}
