using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIHandler))]
public class AIShooting : MonoBehaviour
{
    [SerializeField] private AIHandler _Ai;

    private void Awake()
    {
        _Ai = GetComponent<AIHandler>();
    }

    private void Update()
    {
        if (_Ai.TargetInSight)
        {
            foreach (Weapon gun in _Ai.Plane.Guns)
            {
                gun.Shoot();
            }
        }
    }
}