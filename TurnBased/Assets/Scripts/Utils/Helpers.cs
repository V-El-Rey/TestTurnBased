using UnityEngine;

public static class Helpers
{
    private static Matrix4x4 _isometricMatrix = Matrix4x4.Rotate(Quaternion.Euler(0,45,0));
    public static Vector3 ToIsometric(this Vector3 input) => _isometricMatrix.MultiplyPoint3x4(input);
}
