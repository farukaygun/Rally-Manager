/*
 * Lastik tipi dropdown'dan seçilen deðeri alýnýp
 * Road içerisindeki children'larýn layer'ýna uygulanýyor.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelTypeController : MonoBehaviour
{
    public Dropdown wheelTypeDropdown;
    public GameObject road;

    private WheelFrictionCurve _sidewaysFriction;
    private WheelCollider wheelCollider;

    private void Start()
    {
        wheelCollider = gameObject.GetComponent<WheelCollider>();
    }

    public void SetDropdownValue()
    {
        // yumuþak
        if (wheelTypeDropdown.value == 0)
        {
            // icy
            if (road.layer == 8)
            {
                CalculateWheelSpecs(-10f, -10f, -10f, -10f, -10f);
            }
            // asphalt
            else if (road.layer == 9)
            {
                CalculateWheelSpecs(10f, 10f, 10f, 10f, 10f);
            }
        }
        // orta
        else if (wheelTypeDropdown.value == 1)
        {
            if (road.layer == 8)
            {
                CalculateWheelSpecs(0f, 0f, 0f, 0f, 0f);
            }
            else if (road.layer == 9)
            {
                CalculateWheelSpecs(0f, 0f, 0f, 0f, 0f);
            }
        }
        // sert
        else if (wheelTypeDropdown.value == 2)
        {
            if (road.layer == 8)
            {
                CalculateWheelSpecs(10f, 10f, 10f, 10f, 10f);
            }
            else if (road.layer == 9)
            {
                CalculateWheelSpecs(-10f, -10f, -10f, -10f, -10f);
            }
        }
    }

    void CalculateWheelSpecs(float extremumSlip, float extremumValue, float asymptoteSlip, float asymptoteValue, float stiffness)
    {
        float newExtremumSlip = extremumSlip * 0.002f;
        float newExtremumValue = extremumValue * 0.002f;
        float newAsymptoteSlip = asymptoteSlip * 0.002f;
        float newAsymptoteValue = asymptoteValue * 0.002f;
        float newStiffness = stiffness * 0.002f;

        _sidewaysFriction.extremumSlip = wheelCollider.sidewaysFriction.extremumSlip + newExtremumSlip;
        _sidewaysFriction.extremumValue = wheelCollider.sidewaysFriction.extremumValue + newExtremumValue;
        _sidewaysFriction.asymptoteSlip = wheelCollider.sidewaysFriction.asymptoteSlip + newAsymptoteSlip;
        _sidewaysFriction.asymptoteValue = wheelCollider.sidewaysFriction.asymptoteValue + newAsymptoteValue;
        _sidewaysFriction.stiffness = wheelCollider.sidewaysFriction.stiffness + newStiffness;

        wheelCollider.sidewaysFriction = _sidewaysFriction;
    }
}
