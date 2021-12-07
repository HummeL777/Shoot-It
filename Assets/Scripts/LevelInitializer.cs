using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    [SerializeField] private PlayerHandler _playerPrefab;

    private Transform _spawnPoint;

    private void Awake()
    {
        _spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
        GameObject player = Instantiate(_playerPrefab, _spawnPoint.position, Quaternion.identity).gameObject;
    }
}
