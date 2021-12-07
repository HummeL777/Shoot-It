using UnityEngine;
using UnityEditor.Animations;

public class Plane : MonoBehaviour
{
    [Header("Features")]
    public float Rps;
    public float Speed;
    public float RotationSpeed;
    public float Normalization;

    [Header("Parts")]
    public Transform[] Propellers;
    public Weapon[] Guns;

    [Header("Animations")]
    public AnimatorController _planeAnimator;
}
