using System.Collections;
using System.Collections.Generic;
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

    public void Setup(KeyCode upKey, KeyCode leftKey, KeyCode rightKey,
        KeyCode downKey, Color color, int id)
    {
        _upKey = upKey;
        _leftKey = leftKey;
        _rightKey = rightKey;
        _downKey = downKey;
        _color = color;
        _id = id;
        //up
        _directions.Add(new Vector2(0, 1));
        //right
        _directions.Add(new Vector2(1, 0));
        //down
        _directions.Add(new Vector2(0, -1));
        //left
        _directions.Add(new Vector2(-1, 0));

        _currentDirection = _directions[Random.Range(0, _directions.Count)];
    }

    public void StartMoving(GameGrid gameGrid)
    {
        gameGrid.PlaceASnake(_id, _color);
    }
}
