using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGrid : MonoBehaviour
{
    public GridLayoutGroup  grid;
    public Image            spacePrefab;
    /// <summary>
    /// num of rows
    /// </summary>
    public int row;
    public int column;
    List<Image> _grids = new List<Image>();
    Dictionary<Vector2, Image> _emptyGrids = new Dictionary<Vector2, Image>();

    public List<Image> GenerateGrids()
    {
        int x = 0;
        int y = 0;
        if(spacePrefab != null && grid != null)
        {
            for (int i = 0; i < row*column; i++)
            {
                Image space = Instantiate(spacePrefab);
                space.name = x + ", " + y;
                space.transform.SetParent(grid.transform);
                _grids.Add(space);
                _emptyGrids.Add(new Vector2(x, y), space);
                x++;
                if(x>= column)
                {
                    y++;
                    x = 0;
                }
            }

            grid.constraintCount = row;
            grid.spacing = new Vector2(1, 1);
        }
        return _grids;
    }
}
