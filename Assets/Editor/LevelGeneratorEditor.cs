using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelGenerator))]
public class LevelGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        LevelGenerator generator = (LevelGenerator)target;

        if(GUILayout.Button("Generate Level"))
        {
            generator.GenerateLevel();
        }
    }
}
