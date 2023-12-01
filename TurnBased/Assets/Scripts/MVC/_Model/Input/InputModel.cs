using System;
using UnityEngine;

public class InputModel : IInputModel
{
    public Action onMouseClicked { get; set; }
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
        gridCellHitCoordinates = Vector2.zero;
        onMouseClicked = null;
    }
}
