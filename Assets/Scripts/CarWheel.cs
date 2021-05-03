using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWheel : MonoBehaviour
{
    [SerializeField] private WheelCollider targetWheel;
    private Vector3 wheelPosition;
    private Quaternion wheelRotation;

    private void Awake() 
    {
        wheelRotation = new Quaternion();
        wheelPosition = new Vector3();
    }

    private void Update() 
    {
        targetWheel.GetWorldPose(out wheelPosition, out wheelRotation);
        transform.position = wheelPosition;
        transform.rotation = wheelRotation;
    }
}
