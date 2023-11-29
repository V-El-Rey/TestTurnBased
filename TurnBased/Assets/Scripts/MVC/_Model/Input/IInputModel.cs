using UnityEngine;

public interface IInputModel : IStateModel
{
     Vector3 mousePosition { get; set; }
     Vector2 gridCellHitCoordinates {get; set;}
     LayerMask gridLayerMask { get; set; }
}
