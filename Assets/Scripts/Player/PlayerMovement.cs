using UnityEngine;

[RequireComponent(typeof(PlayerHandler))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerHandler _handler;
    [SerializeField] private float _maxForwardOffset;
    [SerializeField] private float _maxSideOffset;
    [SerializeField] private float _maxRollAngle;

    private void Awake()
    {
        _handler = GetComponent<PlayerHandler>();
    }

    private void Update()
    {
        foreach (Transform propeller in _handler.Plane.Propellers)
        {
            propeller.Rotate(Vector3.forward, _handler.Plane.Rps * 360f * Time.deltaTime);
        }

        Vector3 forwardMovement = Vector3.zero;
        Vector3 sideMovement = Vector3.zero;
        
        if (Input.GetKey(KeyCode.W))
        {
            forwardMovement.z = 1f;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            forwardMovement.z = -1f;
        }

        float angleToRotate = 0f;
        float planeRotation = _handler.Plane.transform.rotation.eulerAngles.z - 180f;

        Debug.Log(planeRotation);
        if (Input.GetKey(KeyCode.D) && !IsRolledOver(planeRotation))
        {
            angleToRotate = -_handler.Plane.RotationSpeed;
        }
        else if (Input.GetKey(KeyCode.A) && !IsRolledOver(planeRotation))
        {
            angleToRotate = _handler.Plane.RotationSpeed;
        }
        else
        {
            angleToRotate = Mathf.Sign(planeRotation) * _handler.Plane.RotationSpeed * _handler.Plane.Normalization;
        }

        _handler.Plane.transform.Rotate(_handler.Plane.transform.forward, angleToRotate * Time.deltaTime);
        sideMovement.x = Mathf.Sin(planeRotation * Mathf.Deg2Rad);

        Vector3 movement = (forwardMovement + sideMovement) * _handler.Plane.Speed * Time.deltaTime;
        transform.Translate(movement, Space.World);

        transform.position = (Vector3.forward * Mathf.Clamp(transform.localPosition.z, 0f, _maxForwardOffset)) + (Vector3.right * Mathf.Clamp(transform.localPosition.x, -_maxSideOffset, _maxSideOffset));
    }

    public bool IsRolledOver(float angle)
    {
        return (Mathf.Abs(angle) < 180f - _maxRollAngle);
    }
}
