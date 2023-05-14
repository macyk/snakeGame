using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// manages all the cells
/// </summary>
public class GameGrid : MonoBehaviour
{
    public GridLayoutGroup  grid;
    public GridCell         spacePrefab;
    /// <summary>
    /// num of rows
    /// </summary>
    public int row;
    public int column;
    List<GridCell>                  _grids = new List<GridCell>();
    Dictionary<Vector2, GridCell>   _emptyGrids = new Dictionary<Vector2, GridCell>();

    /// <summary>
    /// generate the grids
    /// </summary>
    /// <returns></returns>
    public List<GridCell> GenerateGrids()
    {
        int x = 0;
        int y = 0;
        if (_grids.Count == 0)
        {
            if (spacePrefab != null && grid != null)
            {
            
                for (int i = 0; i < row * column; i++)
                {
                    GridCell space = Instantiate(spacePrefab);
                    space.name = x + ", " + y;
                    space.transform.SetParent(grid.transform);
                    space.SetUpWithPos(x, y, row, column);
                    _grids.Add(space);
                    _emptyGrids.Add(new Vector2(x, y), space);
                    x++;
                    if (x >= column)
                    {
                        y++;
                        x = 0;
                    }
                }
            }

            grid.constraintCount = row;
            grid.spacing = new Vector2(1, 1);
        }
        else
        {
            for (int i = 0; i < row * column; i++)
            {
                _grids[i].Reset();
            }
        }
        return _grids;
    }

    /// <summary>
    /// add a snake
    /// </summary>
    /// <param name="id"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    public GridCell PlaceASnake(int id, Color color)
    {
        GridCell startingPoint = GetARandomCell();
        startingPoint.FillCell(id, color);
        _emptyGrids.Remove(startingPoint.GetPos());
        return startingPoint;
    }

    GridCell GetARandomCell()
    {
        int randomIndex = Random.Range(0, _emptyGrids.Count);
        Vector2 startPos = _emptyGrids.Keys.ElementAt(randomIndex);
        GridCell cell = _emptyGrids[startPos];

        return cell;
    }

    public void CreateAnApple()
    {
        GridCell cell = GetARandomCell();
        cell.ShowApple();
    }

    /// <summary>
    /// get next cell based on it's pos
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public GridCell GetACell(Vector2 id)
    {
        if(_emptyGrids.ContainsKey(id))
        {
            GridCell cell = _emptyGrids[id];
            _emptyGrids.Remove(id);

            return cell;
        }

        return null;
    }

    /// <summary>
    /// when moved away
    /// </summary>
    public void ReleaseACell(GridCell cell)
    {
        Vector2 pos = cell.GetPos();
        if (!_emptyGrids.ContainsKey(pos))
        {
            _emptyGrids.Add(pos, cell);
        }
        cell.UnFill();
    }

    /// <summary>
    /// release all occupied cells
    /// </summary>
    /// <param name="cells"></param>
    public void SnakeDead(List<GridCell> cells)
    {
        for(int i = 0; i< cells.Count; i++)
        {
            ReleaseACell(cells[i]);
        }
    }
}
