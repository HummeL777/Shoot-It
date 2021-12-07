using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIHandler))]
public class AIMovement : MonoBehaviour
{
    [SerializeField] private AIHandler _Ai;

    private void Awake()
    {
        _Ai = GetComponent<AIHandler>();
    }

    private void Update()
    {
        foreach (Transform propeller in _Ai.Plane.Propellers)
        {
            propeller.Rotate(Vector3.forward, _Ai.Plane.Rps * 360f * Time.deltaTime);
        }

        Vector3 forwardMovement = Vector3.zero;
        Vector3 sideMovement = Vector3.zero;

        float angleToRotate = 0f;
        float planeRotation = -(_Ai.Plane.transform.rotation.eulerAngles.z - 180f);

        if (_Ai.Target && !_Ai.TargetInSight)
        {
            if (_Ai.Target.position.x > transform.position.x && planeRotation > 90f)
            {
                angleToRotate = -_Ai.Plane.RotationSpeed;
            }
            else if (_Ai.Target.position.x < transform.position.x && planeRotation < -90f)
            {
                angleToRotate = _Ai.Plane.RotationSpeed;
            }
            else
            {
                angleToRotate = Mathf.Sign(planeRotation) * _Ai.Plane.RotationSpeed * _Ai.Plane.Normalization;
            }
        }

        _Ai.Plane.transform.Rotate(_Ai.Plane.transform.forward, angleToRotate * Time.deltaTime);
        sideMovement.x = Mathf.Sin(planeRotation * Mathf.Deg2Rad);

        Vector3 movement = (forwardMovement + sideMovement) * _Ai.Plane.Speed * Time.deltaTime;
        transform.Translate(movement, Space.World);
    }
}
