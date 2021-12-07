using System.Collections.Generic;
using UnityEngine;

public class WaterChunk : Chunk
{
    public void GenerateChunk(Vector2 mapSize, Vector2 cellSize)
    {
        Renderer renderer = GetComponent<Renderer>();
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.sharedMesh = GenerateMesh(mapSize, cellSize);
    }

    public Mesh GenerateMesh(Vector2 mapSize, Vector2 cellSize)
    {
        Mesh mapMesh = new Mesh();

        int width = (int)mapSize.x;
        float halfWidth = width / 2f;
        int height = (int)mapSize.y;
        float halfHeight = height / 2f;

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for (int y = 0; y <= height; y++)
        {
            for (int x = 0; x <= width; x++)
            {
                int num = (width + 1) * y + x;

                Vector3 vertex = new Vector3();
                vertex.x = (-halfWidth + x) * cellSize.x;
                vertex.y = 0f;
                vertex.z = (-halfHeight + y) * cellSize.y;
                vertices.Add(vertex);

                Vector2 uv = new Vector2(x / (float)width, y / (float)height);
                uvs.Add(uv);

                if ((x + 1) % (width + 1) != 0 && (y + 1) % (height + 1) != 0)
                {
                    triangles.Add(num + 1);
                    triangles.Add(num);
                    triangles.Add(num + width + 1);
                    triangles.Add(num + 1);
                    triangles.Add(num + width + 1);
                    triangles.Add(num + width + 2);
                }
            }
        }

        mapMesh.vertices = vertices.ToArray();
        mapMesh.triangles = triangles.ToArray();
        mapMesh.uv = uvs.ToArray();
        mapMesh.normals = CalculateNormals(mapMesh);

        return mapMesh;
    }
}
