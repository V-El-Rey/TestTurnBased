using UnityEngine;

public class InputModel : IInputModel
{
    public Vector3 mousePosition { get; set; }
    public Vector2 gridCellHitCoordinates { get; set; }
    public LayerMask gridLayerMask { get; set; }

    public InputModel(LayerMask hitLayerMask)
    {
        gridLayerMask = hitLayerMask;
    }

    public void ClearModel()
    {
        mousePosition = Vector3.zero;
    }
}
