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

    public static CommonCellEdges DetectCommonEdge(this CellLineView current, CellLineView previous)
    {
        if (previous.transform.position.x == current.transform.position.x + 1)
        {
            return CommonCellEdges.Right;
        }
        if (previous.transform.position.x == current.transform.position.x - 1)
        {
            return CommonCellEdges.Left;
        }
        if (previous.transform.position.z == current.transform.position.z + 1)
        {
            return CommonCellEdges.Up;
        }
        if (previous.transform.position.z == current.transform.position.z - 1)
        {
            return CommonCellEdges.Bottom;
        }
        if(previous.transform.position.x == current.transform.position.x + 1 && previous.transform.position.z == current.transform.position.z + 1)
        {
            return CommonCellEdges.None;
        }
        return CommonCellEdges.None;
    }
}
