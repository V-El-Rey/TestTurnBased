using System;
using UnityEngine;

public interface IInputModel : IStateModel
{
    Action onMouseClicked { get; set; }
    Vector3 mousePosition { get; set; }
    Vector2 gridCellHitCoordinates { get; set; }
    LayerMask gridLayerMask { get; set; }
}
