using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelHealth : MonoBehaviour
{
    private WheelFrictionCurve _sidewaysFriction;
    private WheelCollider wheelCollider;

    // Tekerlek sa�l���
    [SerializeField] float wheelHealth = 100;
    [SerializeField] float engineHealth = 100;
    [SerializeField] float turboHealth = 100;
    [SerializeField] float transmissionHealth = 100;
    [SerializeField] float fuelHealth = 100;

    public Toggle wheelRepairToggle;
    public Toggle engineRepairToggle;
    public Toggle turboRepairToggle;
    public Toggle transmissionRepairToggle;
    public Toggle fuelRepairToggle;
    public Button repairButton;

    private void Start()
    {
        wheelCollider = gameObject.GetComponent<WheelCollider>();
    }

    float CurrentSpeed()
    {
        return 2 * Mathf.PI * wheelCollider.radius * wheelCollider.rpm * 60 / 10000;
    }

    // Tekerlek a��nmas�n� hesapl�yor.
    public void WheelsHealthCalculate()
    {
        /*
         * Anl�k h�z�n, zemin katsay�na oran�n�n binde biri tekerlek sa�l���na etki ediyor.
         * Buzlu zeminin extremumValue'su daha y�ksek oldu�undan anl�k h�zla �arpmak yerine anl�k h�za b�lerek,
         * Buzlu zeminde daha az a��nma olurken, asfalt zeminde daha fazla oluyor.
         * Binde birini alarak tekerlek a��nmas�n�n kabul edilebilir seviyelerde olmas�n� sa�lad�k.
         */
        float wheelErosion = (CarEngine.currentSpeed / wheelCollider.sidewaysFriction.extremumValue) / 1000;

        wheelHealth -= wheelErosion;

        /*
         * Ara� sa�a d�n�yorsa arac�n solundaki tekerler wheelEresion'un yar�s�n�n,
         * Tekerlerin d�n�� a��s�n�n %10'uyla �arp�m� kadar daha a��n�yor.
         * Ayn� durum tam tersi i�in de ge�erli.
         */
        if (wheelCollider.steerAngle > 0)
        {
            if (gameObject.name == "tireFrontL" || gameObject.name == "tireBackL")
            {
                wheelHealth -= wheelErosion * (wheelCollider.steerAngle * 0.05f);
            }
        }
        else if (wheelCollider.steerAngle < 0)
        {
            if (gameObject.name == "tireFrontR" || gameObject.name == "tireBackR")
            {
                wheelHealth -= wheelErosion * (-1 * wheelCollider.steerAngle * 0.05f);
            }
        }
    }

    public void EngineHealthCalculate()
    {
        float engineErosion = (CarEngine.currentSpeed / wheelCollider.sidewaysFriction.extremumValue) / 1000;

        engineHealth -= engineErosion;
        CarComponentErosion(engineHealth);
    }

    public void TurboHealthCalculate()
    {
        float turboErosion = (CarEngine.currentSpeed / wheelCollider.sidewaysFriction.extremumValue) / 1000;

        turboHealth -= turboErosion;
        CarComponentErosion(turboHealth);
    }

    public void TransmissionCalculate()
    {
        float transmissionErosion = (CarEngine.currentSpeed / wheelCollider.sidewaysFriction.extremumValue) / 1000;

        transmissionHealth -= transmissionErosion;
        CarComponentErosion(transmissionHealth);
    }

    public void FuelHealthCalculator()
    {
        float fuelErosion = (CarEngine.currentSpeed / wheelCollider.sidewaysFriction.extremumValue) / 1000;

        fuelHealth -= fuelErosion;
        CarComponentErosion(fuelHealth);
    }

    void CarComponentErosion(float componentName)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Icy"))
            {
                componentName -= 15;
            }
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Asphalt"))
            {
                componentName -= CarEngine.currentSpeed / 2000;
            }
        }
    }

    public void FixCarComponents()
    {
        if (wheelRepairToggle.isOn == true)
        {
            wheelHealth = 100;
        }
        if (engineRepairToggle.isOn == true)
        {
            engineHealth = 100;
        }
        if (turboRepairToggle.isOn == true)
        {
            turboHealth = 100;
        }
        if (transmissionRepairToggle.isOn == true)
        {
            transmissionHealth = 100;
        }
        if (fuelRepairToggle.isOn == true)
        {
            fuelHealth = 100;
        }
    }
}
