using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridCell : MonoBehaviour
{
    public Image    cellImage;
    public Image    appleImage;
    /// <summary>
    /// -1 when no snake on it
    /// </summary>
    int             _playerID = -1;
    float           _x;
    float           _y;
    List<Vector2>   _directions = new List<Vector2>();
    bool            _isApple;

    public void ShowApple()
    {
        if(appleImage != null)
        {
            appleImage.gameObject.SetActive(true);
        }
        _isApple = true;
    }

    void AteApple()
    {
        if (appleImage != null)
        {
            appleImage.gameObject.SetActive(false);
        }
        _isApple = false;
    }

    /// <summary>
    /// Fill the cell and check
    /// </summary>
    /// <param name="playerID"></param>
    /// <param name="color"></param>
    /// <returns>extra length, -1 means cell is already taken, 1 means it has an apple</returns>
    public int FillCell(int playerID, Color color)
    {
        if(_playerID == -1)
        {
            _playerID = playerID;
            if (cellImage != null)
            {
                cellImage.color = color;
            }

            if (_isApple)
            {
                AteApple();
                return 1;
            }
            return 0;
        }
        return -1;
    }

    public void UnFill()
    {
        _playerID = -1;
        if (cellImage != null)
        {
            cellImage.color = Color.white;
        }
    }

    public void SetUpWithPos(float x, float y, int row, int column)
    {
        _x = x;
        _y = y;
        if(x< column-1)
        {
            _directions.Add(GameInfo.RIGHT);
        }
        if(x > 0)
        {
            _directions.Add(GameInfo.LEFT);
        }
        if(y< row-1)
        {
            _directions.Add(GameInfo.DOWN);
        }
        if (y > 0)
        {
            _directions.Add(GameInfo.UP);
        }
    }

    public Vector2 GetPos()
    {
        return new Vector2(_x, _y);
    }

    public Vector2 GetRandomDirection()
    {
        return _directions[Random.Range(0, _directions.Count)];
    }

    /// <summary>
    /// reset to defalut state
    /// </summary>
    public void Reset()
    {
        UnFill();
        AteApple();
        _playerID = -1;
    }
}
