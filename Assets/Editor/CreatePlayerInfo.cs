using UnityEditor;
using UnityEngine;

public class CreatePlayerInfo
{
    [MenuItem ("Assets/Create/Player Info")]
    public static void CreateAPlayerInfo()
    {
        PlayerScriptableObject playerScriptableObject = ScriptableObject.CreateInstance
            <PlayerScriptableObject>();

        string uniqueName = AssetDatabase.GenerateUniqueAssetPath("Assets/ScriptableObjects/player.asset");
        AssetDatabase.CreateAsset(playerScriptableObject, uniqueName);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = playerScriptableObject;
    }
}
