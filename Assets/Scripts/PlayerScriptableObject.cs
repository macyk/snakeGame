using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// we can create player info from here
/// </summary>
public class PlayerScriptableObject : ScriptableObject
{
    public InputActionAsset inputActionAsset;
    public Color            playerColor     = Color.red;

}
