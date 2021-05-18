/*
 * Pilot tipi dropdown'da iyi orta kötü seçimi yapan script.
 * Release öncesi kaldırılacak.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelColliderFriction : MonoBehaviour
{
    public PilotController pilotController;
    public WheelHealth wheelHealth;
    public GroundController groundController;
    public WheelTypeController wheelTypeController;
    public PilotTypeController pilotTypeController;

    private WheelFrictionCurve _sidewaysFriction;
    private WheelCollider wheelCollider;

    private void Start()
    {
        wheelCollider = gameObject.GetComponent<WheelCollider>();
    }

    private void FixedUpdate()
    {
        CheckGround(); // Zemin türünü tespit ediyor.
        //pilotController.CalculatePilotSpecs();
        pilotTypeController.CalculatePilotSpecs();
        groundController.SetDropdownValue();
        wheelTypeController.SetDropdownValue();

        wheelHealth.WheelsHealthCalculate(); // Tekerlek aşınmasını hesaplayan fonksiyon
        wheelHealth.EngineHealthCalculate();
        wheelHealth.TurboHealthCalculate();
        wheelHealth.TransmissionCalculate();
        wheelHealth.FuelHealthCalculator();
    }

    // Zemin özellikleri, sürtünme katsayıları vs.
    void CheckGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            // _sidewaysFriction = wheelCollider.sidewaysFriction;
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Icy"))
            {
                /* 
                 * The sidewaysFriction is not expection a "float" but a "WheelFrictionCurve"
                 * Get the WheelFrictionCurve from sidewaysFriction and modify it , 
                 * then write it back to the sidewaysFriction.
                */
                _sidewaysFriction.extremumSlip = 1.5f;
                _sidewaysFriction.extremumValue = 1f;
                _sidewaysFriction.asymptoteSlip = 0.5f;
                _sidewaysFriction.asymptoteValue = 0.75f;
                _sidewaysFriction.stiffness = 0.9f;

                wheelCollider.sidewaysFriction = _sidewaysFriction;
            }
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Asphalt"))
            {
                _sidewaysFriction.extremumSlip = 0.3f;
                _sidewaysFriction.extremumValue = 1f;
                _sidewaysFriction.asymptoteSlip = 0.5f;
                _sidewaysFriction.asymptoteValue = 0.75f;
                _sidewaysFriction.stiffness = 1f;

                wheelCollider.sidewaysFriction = _sidewaysFriction;
            }
        }
    }
}
