using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifetime;
    private Vector3 direction = Vector3.forward;

    private void Start()
    {
        StartCoroutine(DestroyBullet());
    }

    private void Update()
    {
        transform.Translate(direction.normalized * _speed * Time.deltaTime);
    }

    private void AddCrater()
    {
    }

    public void ChangeDirection(Vector3 newDir)
    {
        direction = newDir;
    }

    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(_lifetime);
        Destroy(gameObject);
    }
}
