using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Vector2 _levelSize;
    [SerializeField] private GameObject _groundChunkPrefab;
    [SerializeField] private GameObject _waterChunkPrefab;
    [SerializeField] private string _groundChunkName;
    [SerializeField] private string _waterChunkName;

    [SerializeField] private Dictionary<Vector2, Transform> _groundChunks = new Dictionary<Vector2, Transform>();
    [SerializeField] private Dictionary<Vector2, Transform> _waterChunks = new Dictionary<Vector2, Transform>();

    public void GenerateLevel()
    {
        Renderer chunkRenderer = _groundChunkPrefab.GetComponent<Renderer>();
        Vector2 chunkSize = new Vector2(chunkRenderer.bounds.size.x, chunkRenderer.bounds.size.z);

        DestroyAllChunks();

        float startXPos = (-(_levelSize.x - 1) / 2) * chunkSize.x;
        float startYPos = -chunkSize.y/2f;
        for (int y = 0; y < _levelSize.y; y++)
        {
            for (int x = 0; x < _levelSize.x; x++)
            {
                Vector3 spawnPos = new Vector3(startXPos + chunkSize.x * x, 0f, startYPos + chunkSize.y * y);
                CreateGroundChunk(spawnPos);
                CreateWaterChunk(spawnPos);
            }
        }
    }

    public void DestroyAllChunks()
    {
        var tempList = transform.Cast<Transform>().ToList();
        foreach (Transform child in tempList)
        {
            if (child.name.Contains(_groundChunkName))
            {
                DestroyImmediate(child.gameObject);
            }
            else if( child.name.Contains(_waterChunkName))
            {
                DestroyImmediate(child.gameObject);
            }
        }
        _groundChunks.Clear();
        _waterChunks.Clear();
    }

    public void CreateGroundChunk(Vector3 pos)
    {
        Transform createdChunk = Instantiate(_groundChunkPrefab, pos, Quaternion.identity, transform).transform;

        int chunksCount = _groundChunks.Count;
        createdChunk.name = _groundChunkName + (chunksCount);

        int yIndex = chunksCount / (int)_levelSize.x;
        int xIndex = chunksCount - (yIndex * (int)_levelSize.x);
        _groundChunks.Add(new Vector2(xIndex, yIndex), createdChunk);
    }

    public void CreateWaterChunk(Vector3 pos)
    {
        Transform createdChunk = Instantiate(_waterChunkPrefab, pos, Quaternion.identity, transform).transform;

        int chunksCount = _waterChunks.Count;
        createdChunk.name = _waterChunkName + (chunksCount);

        int yIndex = chunksCount / (int)_levelSize.x;
        int xIndex = chunksCount - (yIndex * (int)_levelSize.x);
        _waterChunks.Add(new Vector2(xIndex, yIndex), createdChunk);
    }
}
