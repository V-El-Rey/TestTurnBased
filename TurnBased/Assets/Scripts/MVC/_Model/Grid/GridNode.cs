using System;

public class GridNode
{
    public int x;
    public int y;

    public int gCost;
    public int hCost;
    public int fCost;

    public GridNode previousPathNode;

    public GridNode(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return $"{x} : {y}";
    }
}
