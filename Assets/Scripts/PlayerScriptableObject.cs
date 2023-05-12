using UnityEngine;

/// <summary>
/// we can create player info from here
/// </summary>
public class PlayerScriptableObject : ScriptableObject
{
    public KeyCode  upKey = KeyCode.W;
    public KeyCode  downKey = KeyCode.S;
    public KeyCode  leftKey = KeyCode.A;
    public KeyCode  rightKey = KeyCode.D;
    public Color    playerColor = Color.red;

}
