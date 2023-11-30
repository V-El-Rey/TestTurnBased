using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class PlayerUnitModel
{
    public string UnitName;
    public float Health;
    public float Attack;
    public int actionPoints;
    public GameObject prefab;
}
