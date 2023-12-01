using UnityEngine;

[CreateAssetMenu(fileName = "GridConfiguration", menuName = "Configs/Grid", order = 1)]
public class GridConfiguration : ScriptableObject
{
    public int width;
    public int height;

    public Material highLightedMaterial;
    public Material regularMaterial;
    public Material selectedCellMaterial;
    public Material possibleCellMaterial;

    public LayerMask gridHitLayerMask;
}
