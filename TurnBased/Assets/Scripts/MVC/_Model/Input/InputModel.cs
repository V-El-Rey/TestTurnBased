using UnityEngine;

public class InputModel : IInputModel
{
    public Vector3 mousePosition { get; set; }
    public Vector2 gridCellHitCoordinates { get; set; }

    public void ClearModel()
    {
        mousePosition = Vector3.zero;
    }
}
