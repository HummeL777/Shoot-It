using System;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    public Plane Plane;

    public Action TakeoffFinished;

    private void Awake()
    {
        UpdatePlane();
    }

    public void UpdatePlane()
    {
        Plane = transform.Find("Plane").GetComponent<Plane>();
    }
}
