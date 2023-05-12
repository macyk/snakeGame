using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    KeyCode _upKey;
    KeyCode _leftKey;
    KeyCode _rightKey;
    KeyCode _downKey;
    /// <summary>
    /// the length of the snake starts with 1
    /// </summary>
    int     _length = 1;

    public void Setup(KeyCode upKey, KeyCode leftKey, KeyCode rightKey,
        KeyCode downKey)
    {
        _upKey = upKey;
        _leftKey = leftKey;
        _rightKey = rightKey;
        _downKey = downKey;
    }
}
