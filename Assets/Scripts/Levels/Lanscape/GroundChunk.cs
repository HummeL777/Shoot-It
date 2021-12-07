using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GroundChunk : Chunk
{
    public void GenerateChunk(Vector2 size, Vector2 offset, int octaves, float scale, float persistance, float lacunarity, int seed, Vector2 mapSize, Vector2 cellSize, AnimationCurve heightIntensity, float maxHeight)
    {
        Renderer renderer = GetComponent<Renderer>();
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.sharedMesh = GenerateMesh(GenerateNoiseMap(size, offset, octaves, scale, persistance, lacunarity, seed), mapSize, cellSize, heightIntensity, maxHeight);
    }

    public float[,] GenerateNoiseMap(Vector2 size, Vector2 offset, int octaves, float scale, float persistance, float lacunarity, int seed)
    {
        int width = (int)size.x;
        float halfWidth = width / 2f;
        int height = (int)size.y;
        float halfHeight = height / 2f;

        float[,] noiseMap = new float[width, height];

        if (scale <= 0)
        {
           scale = 0.001f;
        }

        //different octaves have different offset
        Random.InitState(seed);
        Vector2[] offsets = new Vector2[octaves];
        for (int z = 0; z < octaves; z++)
        {
            int xOffset = Random.Range(-9999, 9999); //Random.Range(int.MinValue, int.MaxValue);
            int yOffset = Random.Range(-9999, 9999); //Random.Range(int.MinValue, int.MaxValue);
            offsets[z] = new Vector2(xOffset, yOffset);
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float intensity = 0f;

                for (int z = 0; z < octaves; z++)
                {
                    float xSample = ((x - halfWidth) + offsets[z].x + offset.x) / scale * Mathf.Pow(lacunarity, z);
                    float ySample = ((y - halfHeight) + offsets[z].y + offset.y) / scale * Mathf.Pow(lacunarity, z);

                    intensity += Mathf.PerlinNoise(xSample, ySample) * Mathf.Pow(persistance, z);
                }

                float maxIntensity = (1 - Mathf.Pow(persistance, octaves)) / (1 - persistance);
                noiseMap[x, y] = Mathf.InverseLerp(0, maxIntensity, intensity);
            }
        }

        return noiseMap;
    }

    public Mesh GenerateMesh(float[,] heightMap, Vector2 mapSize, Vector2 cellSize, AnimationCurve heightIntensity, float maxHeight)
    {
        Mesh mapMesh = new Mesh();

        int width = (int)mapSize.x;
        float halfWidth = width / 2f;
        int height = (int)mapSize.y;
        float halfHeight = height / 2f;

        float xScale = (heightMap.GetLength(0) - 1) / (float)width;
        float yScale = (heightMap.GetLength(1) - 1) / (float)height;

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        List<int> borderIDs = new List<int>();

        for (int y = 0; y <= height; y++)
        {
            for (int x = 0; x <= width; x++)
            {
                int num = (width + 1) * y + x;

                if (x == 0 || y == 0 || (x + 1) % (width + 1) == 0 || (y + 1) % (height + 1) == 0)
                {
                    borderIDs.Add(num);
                }

                Vector3 vertex = new Vector3();
                vertex.x = (-halfWidth + x) * cellSize.x;
                vertex.y = heightIntensity.Evaluate(heightMap[(int)(x * xScale), (int)(y * yScale)]) * maxHeight;
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
        List<Vector3> normals = CalculateNormals(mapMesh).OfType<Vector3>().ToList();

        int deleted = 0;
        foreach(int id in borderIDs)
        {
            int deltaId = id - deleted;
            vertices.RemoveAt(deltaId);
            uvs.RemoveAt(deltaId);
            normals.RemoveAt(deltaId);
            deleted++;
        }

        triangles.Clear();
        for (int y = 0; y <= height - 2; y++)
        {
            for (int x = 0; x <= width - 2; x++)
            {
                int num = (width - 1) * y + x;
                if ((x + 1) % (width - 1) != 0 && (y + 1) % (height - 1) != 0)
                {
                    triangles.Add(num + 1);
                    triangles.Add(num);
                    triangles.Add(num + width - 1);
                    triangles.Add(num + 1);
                    triangles.Add(num + width - 1);
                    triangles.Add(num + width);
                }
            }
        }

        mapMesh = new Mesh();
        mapMesh.vertices = vertices.ToArray();
        mapMesh.triangles = triangles.ToArray();
        mapMesh.uv = uvs.ToArray();
        mapMesh.normals = normals.ToArray();

        return mapMesh;
    }
}