using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public Vector3[] CalculateNormals(Mesh mesh)
    {
        Vector3[] vertexNormals = new Vector3[mesh.vertices.Length];
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;
        int triangelsCount = triangles.Length / 3;

        for (int i = 0; i < triangelsCount; i++)
        {
            int vertexIndex = i * 3;

            int aVertex = triangles[vertexIndex];
            int bVertex = triangles[vertexIndex + 1];
            int cVertex = triangles[vertexIndex + 2];

            Vector3 aPoint = vertices[aVertex];
            Vector3 bPoint = vertices[bVertex];
            Vector3 cPoint = vertices[cVertex];
            Vector3 abSide = bPoint - aPoint;
            Vector3 acSide = cPoint - aPoint;

            Vector3 normal = Vector3.Cross(abSide, acSide);

            vertexNormals[aVertex] += normal;
            vertexNormals[bVertex] += normal;
            vertexNormals[cVertex] += normal;
        }

        for (int i = 0; i < vertexNormals.Length; i++)
        {
            vertexNormals[i] = vertexNormals[i].normalized;
        }

        return vertexNormals;
    }
}
