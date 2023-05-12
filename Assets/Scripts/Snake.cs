using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Snake : MonoBehaviour
{
    KeyCode _upKey;
    KeyCode _leftKey;
    KeyCode _rightKey;
    KeyCode _downKey;
    Color   _color;
    /// <summary>
    /// the length of the snake starts with 1
    /// </summary>
    int     _length = 1;
    int     _id;
    List<Vector2>   _directions = new List<Vector2>();
    Vector2         _currentDirection;
    /// <summary>
    /// snake head pos
    /// </summary>
    Vector2         _headPos;
    List<GridCell>  _cells      = new List<GridCell>();
    GameGrid        _gameGrid;

    public void Setup(KeyCode upKey, KeyCode leftKey, KeyCode rightKey,
        KeyCode downKey, Color color, int id)
    {
        _upKey = upKey;
        _leftKey = leftKey;
        _rightKey = rightKey;
        _downKey = downKey;
        _color = color;
        _id = id;
        //down
        _directions.Add(new Vector2(0, 1));
        //right
        _directions.Add(new Vector2(1, 0));
        //up
        _directions.Add(new Vector2(0, -1));
        //left
        _directions.Add(new Vector2(-1, 0));
    }

    void Update()
    {
        if(Input.GetKeyDown(_downKey))
        {
            _currentDirection = new Vector2(0, 1);
        }
        if(Input.GetKeyDown(_leftKey))
        {
            _currentDirection = new Vector2(-1, 0);
        }
        if (Input.GetKeyDown(_upKey))
        {
            _currentDirection = new Vector2(0, -1);
        }
        if (Input.GetKeyDown(_rightKey))
        {
            _currentDirection = new Vector2(1, 0);
        }
    }

    public void StartMoving(GameGrid gameGrid)
    {
        _gameGrid = gameGrid;
        GridCell cell= gameGrid.PlaceASnake(_id, _color);
        _headPos = cell.GetPos();
        _currentDirection = cell.GetRandomDirection();
        _cells.Add(cell);
    }

    public void MoveNext()
    {
        if(_gameGrid == null)
        {
            Debug.LogError("no grid");
        }
        GridCell cell = _gameGrid.GetACell(_headPos + _currentDirection);
        if(cell == null)
        {
            Debug.Log("==== game over");
        }
        else
        {
            int extraLength = cell.FillCell(_id, Color.red);
            if(extraLength >-1)
            {
                //if we ate an apple, we keep the previous tail
                if(extraLength<1)
                {
                    GridCell tail = _cells.First();
                    tail.UnFill();

                    _gameGrid.ReleaseACell(tail);
                    _cells.Remove(tail);
                }
                else ///after we eat an apple, we create a new one
                {
                    _gameGrid.CreateAnApple();
                }
                
                _cells.Add(cell);
                _headPos = cell.GetPos();
            }
        }
    }
}
