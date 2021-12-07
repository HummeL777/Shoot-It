using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHandler : MonoBehaviour
{
    public Plane Plane;
    public Transform Target;
    public bool TargetInSight;

    private void Awake()
    {
        UpdatePlane();
    }

    public void UpdatePlane()
    {
        Plane = transform.Find("Plane").GetComponent<Plane>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Target = other.transform;
        TargetInSight = true;
    }

    private void OnTriggerExit(Collider other)
    {
        TargetInSight = false;
    }
}
