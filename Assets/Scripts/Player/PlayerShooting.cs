using UnityEngine;

[RequireComponent(typeof(PlayerHandler))]
public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private PlayerHandler _handler;

    private void Awake()
    {
        _handler = GetComponent<PlayerHandler>();
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            foreach(Weapon gun in _handler.Plane.Guns)
            {
                gun.Shoot();
            }
        }
    }
}
