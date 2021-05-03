using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelHealth : MonoBehaviour
{
    private WheelFrictionCurve _sidewaysFriction;
    private WheelCollider wheelCollider;

    // Tekerlek sa�l���
    [SerializeField] float wheelHealth = 100;

    private void Start()
    {
        wheelCollider = gameObject.GetComponent<WheelCollider>();
    }

    // Tekerlek a��nmas�n� hesapl�yor.
    public void WheelsHealthCalculate()
    {
        float currentSpeed = 2 * Mathf.PI * wheelCollider.radius * wheelCollider.rpm * 60 / 10000;
        /*
         * Anl�k h�z�n, zemin katsay�na oran�n�n binde biri tekerlek sa�l���na etki ediyor.
         * Buzlu zeminin extremumValue'su daha y�ksek oldu�undan anl�k h�zla �arpmak yerine anl�k h�za b�lerek,
         * Buzlu zeminde daha az a��nma olurken, asfalt zeminde daha fazla oluyor.
         * Binde birini alarak tekerlek a��nmas�n�n kabul edilebilir seviyelerde olmas�n� sa�lad�k.
         */
        float wheelErosion = (currentSpeed / wheelCollider.sidewaysFriction.extremumValue) / 1000;

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
                Debug.Log(wheelHealth);
            }
        }
        else if (wheelCollider.steerAngle < 0)
        {
            if (gameObject.name == "tireFrontR" || gameObject.name == "tireBackR")
            {
                wheelHealth -= wheelErosion * (-1 * wheelCollider.steerAngle * 0.05f);
                Debug.Log(wheelHealth);
            }
        }
    }
}
