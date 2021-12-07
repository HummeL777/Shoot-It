using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    [SerializeField] private Vector3 _viewOffset;
    [SerializeField] private float _viewDist;
    [SerializeField] private float _speed;

    private Transform _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * -_speed * Time.deltaTime);
        foreach(Transform chunk in transform)
        {
            if(Vector3.Distance(_player.position, chunk.position + _viewOffset) < _viewDist)
            {
                chunk.gameObject.SetActive(true);
            }
            else
            {
                chunk.gameObject.SetActive(false);
            }
        }
    }
}
