using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelHealth : MonoBehaviour
{
    private WheelFrictionCurve _sidewaysFriction;
    private WheelCollider wheelCollider;

    // Tekerlek saðlýðý
    [SerializeField] float wheelHealth = 100;

    private void Start()
    {
        wheelCollider = gameObject.GetComponent<WheelCollider>();
    }

    // Tekerlek aþýnmasýný hesaplýyor.
    public void WheelsHealthCalculate()
    {
        float currentSpeed = 2 * Mathf.PI * wheelCollider.radius * wheelCollider.rpm * 60 / 10000;
        /*
         * Anlýk hýzýn, zemin katsayýna oranýnýn binde biri tekerlek saðlýðýna etki ediyor.
         * Buzlu zeminin extremumValue'su daha yüksek olduðundan anlýk hýzla çarpmak yerine anlýk hýza bölerek,
         * Buzlu zeminde daha az aþýnma olurken, asfalt zeminde daha fazla oluyor.
         * Binde birini alarak tekerlek aþýnmasýnýn kabul edilebilir seviyelerde olmasýný saðladýk.
         */
        float wheelErosion = (currentSpeed / wheelCollider.sidewaysFriction.extremumValue) / 1000;

        wheelHealth -= wheelErosion;

        /*
         * Araç saða dönüyorsa aracýn solundaki tekerler wheelEresion'un yarýsýnýn,
         * Tekerlerin dönüþ açýsýnýn %10'uyla çarpýmý kadar daha aþýnýyor.
         * Ayný durum tam tersi için de geçerli.
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
