using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level_", menuName = "Level Data/Default", order = 1)]
public class LevelData : ScriptableObject
{
    public Color[] Colors;
    [Range(-1, 1)] public float[] Heights;
}
