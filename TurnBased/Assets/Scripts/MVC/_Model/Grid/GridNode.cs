public class GridNode
{
    public int x;
    public int y;

    public int gCost;
    public int hCost;
    public int fCost;
    public UnitView unit;
    public PlayerUnitModel playerUnitModel;
    public bool isSelected;
    public bool isOccupied;

    public GridNode previousPathNode;

    public GridNode(int x, int y)
    {
        this.x = x;
        this.y = y;
        isOccupied = false;
        isSelected = false;
    }
}
