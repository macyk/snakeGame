using UnityEngine;
using UnityEngine.UI;

public class GridCell : MonoBehaviour
{
    public Image cellImage;
    /// <summary>
    /// -1 when no snake on it
    /// </summary>
    int _playerID = -1;

    public void FillCell(int playerID, Color color)
    {
        _playerID = playerID;
        if(cellImage != null)
        {
            cellImage.color = color;
        }
    }
}
