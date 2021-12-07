using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapGenerator : MonoBehaviour
{
    [Header("Generator")]
    [SerializeField] private int _seed;
    [SerializeField] private Vector2 _size;
    [SerializeField] private int _octaves;
    [SerializeField] private float _scale;
    [SerializeField] private float _persistance;
    [SerializeField] private float _lacunarity;
    [SerializeField] private Vector2 _offset;

    [Header("Ground")]
    [SerializeField] private float _maxHeight;
    [SerializeField] private AnimationCurve _heightIntensity;
    [SerializeField] private Vector2 _groundCellCount;
    [SerializeField] private Vector2 _groundCellSize;

    [Header("Water")]
    [SerializeField] private Vector2 _waterCellCount;
    [SerializeField] private Vector2 _waterCellSize;

    [Header("Shader")]
    [SerializeField] private Material _material;
    [SerializeField] private LevelData _levelData;

    private void Awake()
    {
        UpdateShaderValues();
    }

    public void RandomizeSeed()
    {
        _seed = Random.Range(int.MinValue, int.MaxValue);
    }

    public void GenerateMap()
    {
        foreach(GroundChunk chunk in GetAllGroundChunks())
        {
            Vector2 offset = CalculateOffset(chunk.transform) + _offset;
            chunk.GenerateChunk(_size, offset, _octaves, _scale, _persistance, _lacunarity, _seed, _groundCellCount, _groundCellSize, _heightIntensity, _maxHeight);
        }

        foreach(WaterChunk chunk in GetAllWaterChunks())
        {
            chunk.GenerateChunk(_waterCellCount, _waterCellSize);
        }

        UpdateShaderValues();
    }

    public void GenerateRandomMap()
    {
        Random.InitState(_seed);
        _scale = Random.Range(10f, 999f);
        _persistance = Random.Range(0.01f, 1f);
        _lacunarity = Random.Range(1f, 5f);
        _offset = new Vector2(Random.Range(-9999, 9999), Random.Range(-9999, 9999));

        GenerateMap();
    }

    public Vector2 CalculateOffset(Transform chunk)
    {
        float xOffset = (chunk.position.x / (_groundCellCount.x * _groundCellSize.x)) * (_size.x - 1);
        float yOffset = (chunk.position.z / (_groundCellCount.y * _groundCellSize.y)) * (_size.y - 1);
        Vector2 offset = new Vector2(xOffset, yOffset);
        return offset;
    }

    public GroundChunk[] GetAllGroundChunks()
    {
        List<GroundChunk> chunks = new List<GroundChunk>();
        foreach(Transform child in transform)
        {
            if(child.GetComponent<GroundChunk>())
            {
                chunks.Add(child.GetComponent<GroundChunk>());
            }
        }
        return chunks.ToArray();
    }

    public WaterChunk[] GetAllWaterChunks()
    {
        List<WaterChunk> chunks = new List<WaterChunk>();
        foreach (Transform child in transform)
        {
            if (child.GetComponent<WaterChunk>())
            {
                chunks.Add(child.GetComponent<WaterChunk>());
            }
        }
        return chunks.ToArray();
    }

    public void UpdateShaderValues()
    {
        _material.SetInt("ColorsCount", _levelData.Colors.Length);
        _material.SetFloat("MaxHeight", _maxHeight);
        _material.SetColorArray("LayersColors", _levelData.Colors);
        _material.SetFloatArray("LayersHeights", _levelData.Heights);
    }
}
