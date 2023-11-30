using UnityEngine;

public static class Helpers
{
    private static Matrix4x4 _isometricMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIsometric(this Vector3 input) => _isometricMatrix.MultiplyPoint3x4(input);

    public static Vector3 SelectCellYAxis(this Vector3 input)
    {
        return new Vector3(input.x, Mathf.Clamp(input.y + 0.25f, 0, 0.25f), input.z);
    }
    public static Vector3 DeselectCellYAxis(this Vector3 input)
    {
        return new Vector3(input.x, Mathf.Clamp(input.y - 0.25f, 0, 1), input.z);
    }

    public static CommonCellEdges DetectCommonEdge(Vector3 currentPos, Vector3 previousPos)
    {
        if(previousPos.x == currentPos.x + 1 && previousPos.z == currentPos.z + 1)
        {
            return CommonCellEdges.None;
        }
        if (previousPos.x == currentPos.x + 1)
        {
            return CommonCellEdges.Right;
        }
        if (previousPos.x == currentPos.x - 1)
        {
            return CommonCellEdges.Left;
        }
        if (previousPos.z == currentPos.z + 1)
        {
            return CommonCellEdges.Up;
        }
        if (previousPos.z == currentPos.z - 1)
        {
            return CommonCellEdges.Bottom;
        }
        return CommonCellEdges.None;
    }
}
