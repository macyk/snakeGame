using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// keeps track of the player info
/// </summary>
public class Snake : MonoBehaviour
{
    Color   _color;
    int     _id;
    List<Vector2>   _directions = new List<Vector2>();
    Vector2         _currentDirection;
    /// <summary>
    /// snake head pos
    /// </summary>
    Vector2         _headPos;
    List<GridCell>  _cells      = new List<GridCell>();
    GameGrid        _gameGrid;
    bool            _alive;
    PlayerInput     _playerInput;
    const   string  UP          = "Up";
    const   string  DOWN        = "Down";
    const   string  LEFT        = "Left";
    const   string  RIGHT       = "Right";

    /// <summary>
    /// set up the snake with it's control, id and color
    /// </summary>
    /// <param name="inputActionAsset"></param>
    /// <param name="color"></param>
    /// <param name="id"></param>
    public void Setup(InputActionAsset inputActionAsset, Color color, int id)
    {
        if(_playerInput == null)
        {
            _playerInput = gameObject.AddComponent<PlayerInput>();
            _playerInput.notificationBehavior = PlayerNotifications.InvokeUnityEvents;
        }
        if(_playerInput != null)
        {
            _playerInput.actions = inputActionAsset;
            InputActionMap inputActionMap = _playerInput.actions.FindActionMap("Player");
            if(inputActionMap != null)
            {
                EnableAction(inputActionMap, UP);
                EnableAction(inputActionMap, DOWN);
                EnableAction(inputActionMap, LEFT);
                EnableAction(inputActionMap, RIGHT);
            }
        }
        _alive = true;
        _color = color;
        _id = id;
        //down
        _directions.Add(GameInfo.DOWN);
        //right
        _directions.Add(GameInfo.RIGHT);
        //up
        _directions.Add(GameInfo.UP);
        //left
        _directions.Add(GameInfo.LEFT);
    }

    /// <summary>
    /// enable action by name
    /// </summary>
    /// <param name="inputActionMap"></param>
    /// <param name="actionName"></param>
    void EnableAction(InputActionMap inputActionMap, string actionName)
    {
        InputAction upAction = inputActionMap.FindAction(actionName);
        if (upAction != null)
        {
            upAction.Enable();
            upAction.performed += OnActionTriggered;
        }
    }

    public int GetID()
    {
        return _id;
    }

    void OnActionTriggered(InputAction.CallbackContext context)
    {
        switch(context.action.name)
        {
            case UP:
                if (_cells.Count > 1
                    && Equals(_currentDirection, GameInfo.DOWN))
                {
                    break;
                }
                _currentDirection = GameInfo.UP;
                break;
            case DOWN:
                if (_cells.Count > 1
                    && Equals(_currentDirection, GameInfo.UP))
                {
                    break;
                }
                _currentDirection = GameInfo.DOWN;
                break;
            case LEFT:
                if (_cells.Count > 1
                    && Equals(_currentDirection, GameInfo.RIGHT))
                {
                    break;
                }
                _currentDirection = GameInfo.LEFT;
                break;
            case RIGHT:
                if (_cells.Count > 1
                    && Equals(_currentDirection, GameInfo.LEFT))
                {
                    break;
                }
                _currentDirection = GameInfo.RIGHT;
                break;
        }
    }

    void Update()
    {
        if(!_alive)
        {
            return;
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

    /// <summary>
    /// moves the snake
    /// </summary>
    /// <returns>true if we moved sucessfuly</returns>
    public bool MoveNext()
    {
        if (!_alive)
        {
            return false;
        }
        if (_gameGrid == null)
        {
            Debug.LogError("no grid");
        }
        GridCell cell = _gameGrid.GetACell(_headPos + _currentDirection);
        if(cell == null)
        {
            if(_gameGrid != null)
            {
                _gameGrid.SnakeDead(_cells);
            }
            return false;
        }
        else
        {
            int extraLength = cell.FillCell(_id, _color);
            if(extraLength >-1)
            {
                //if we ate an apple, we keep the previous tail
                if(extraLength<1)
                {
                    GridCell tail = _cells.First();

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

        return true;
    }

    void OnDestroy()
    {
        if (_playerInput != null)
        {
            _playerInput.onActionTriggered -= OnActionTriggered;
        }
    }
}
