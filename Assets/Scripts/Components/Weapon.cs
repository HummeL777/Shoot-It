using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _horizontalDispersion;
    [SerializeField] private float _verticalDispersion;
    [SerializeField] private ParticleSystem _shootParticle;
    [SerializeField] private AudioClip _shootSound;
    private bool _readyToFire = true;

    public void Shoot()
    {
        if(_readyToFire)
        {
            Vector3 spawnRot = transform.rotation.eulerAngles;
            spawnRot.y = Random.Range(-_horizontalDispersion, _horizontalDispersion);
            spawnRot.x = Random.Range(-_verticalDispersion, _verticalDispersion);

            Bullet createdBullet = Instantiate(_projectile, transform.position, Quaternion.Euler(spawnRot)).GetComponent<Bullet>();
            createdBullet.ChangeDirection(transform.forward);
            Instantiate(_shootParticle, transform);
            AudioSource.PlayClipAtPoint(_shootSound, transform.position);
            _readyToFire = false;
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(_cooldown);
        _readyToFire = true;
    }
}
