using System.Linq;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MapGenerator generator = (MapGenerator)target;

        if (GUILayout.Button("Generate Map"))
        {
            generator.GenerateMap();
        }

        if (GUILayout.Button("Generate Random Map"))
        {
            generator.GenerateRandomMap();
        }

        if (GUILayout.Button("Randomize Seed"))
        {
            generator.RandomizeSeed();
        }
    }
}
