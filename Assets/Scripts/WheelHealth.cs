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

  private void Awake()
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
        Global.wheelHealthLF -= wheelErosion * (wheelCollider.steerAngle * 0.05f);
        Global.wheelHealthLB -= wheelErosion * (wheelCollider.steerAngle * 0.05f);
      }
    }
    else if (wheelCollider.steerAngle < 0)
    {
      if (gameObject.name == "tireFrontR" || gameObject.name == "tireBackR")
      {
        Global.wheelHealthRB -= wheelErosion * (-1 * wheelCollider.steerAngle * 0.05f);
        Global.wheelHealthRF -= wheelErosion * (-1 * wheelCollider.steerAngle * 0.05f);
      }
    }
  }

  public void EngineHealthCalculate()
  {
    float engineErosion = (CarEngine.currentSpeed / wheelCollider.sidewaysFriction.extremumValue) / 10000;

    Global.engineHealth -= engineErosion;
    CarComponentErosion("engine");
  }

  public void TurboHealthCalculate()
  {
    float turboErosion = (CarEngine.currentSpeed / wheelCollider.sidewaysFriction.extremumValue) / 9950;

    Global.turboHealth -= turboErosion;
    CarComponentErosion("turbo");
  }

  public void TransmissionCalculate()
  {
    float transmissionErosion = (CarEngine.currentSpeed / wheelCollider.sidewaysFriction.extremumValue) / 9850;

    Global.transmissionHealth -= transmissionErosion;
    CarComponentErosion("transmission");
  }

  public void FuelHealthCalculator()
  {
    float fuelErosion = (CarEngine.currentSpeed / wheelCollider.sidewaysFriction.extremumValue) / 10100;

    Global.fuelHealth -= fuelErosion;
    CarComponentErosion("fuel");
  }

  void CarComponentErosion(string componentName)
  {
    RaycastHit hit;
    if (Physics.Raycast(transform.position, Vector3.down, out hit))
    {
      if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Icy"))
      {
        if (componentName == "engine")
        {
          Global.engineHealth -= CarEngine.currentSpeed / 19650;
        }
        else if (componentName == "turbo")
        {
          Global.turboHealth -= CarEngine.currentSpeed / 19700;
        }
        else if (componentName == "transmission")
        {
          Global.transmissionHealth -= CarEngine.currentSpeed / 19600;
        }
        else if (componentName == "fuel")
        {
          Global.fuelHealth -= CarEngine.currentSpeed / 19700;
        }
      }
      else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Asphalt"))
      {
        if (componentName == "engine")
        {
          Global.engineHealth -= CarEngine.currentSpeed / 20000;
        }
        else if (componentName == "turbo")
        {
          Global.turboHealth -= CarEngine.currentSpeed / 20000;
        }
        else if (componentName == "transmission")
        {
          Global.transmissionHealth -= CarEngine.currentSpeed / 20000;
        }
        else if (componentName == "fuel")
        {
          Global.fuelHealth -= CarEngine.currentSpeed / 20000;
        }
      }
      else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Soil"))
      {
        if (componentName == "engine")
        {
          Global.engineHealth -= CarEngine.currentSpeed / 19800;
        }
        else if (componentName == "turbo")
        {
          Global.turboHealth -= CarEngine.currentSpeed / 19600;
        }
        else if (componentName == "transmission")
        {
          Global.transmissionHealth -= CarEngine.currentSpeed / 19800;
        }
        else if (componentName == "fuel")
        {
          Global.fuelHealth -= CarEngine.currentSpeed / 19800;
        }
      }
      else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Gravel"))
      {
        if (componentName == "engine")
        {
          Global.engineHealth -= CarEngine.currentSpeed / 19400;
        }
        else if (componentName == "turbo")
        {
          Global.turboHealth -= CarEngine.currentSpeed / 19400;
        }
        else if (componentName == "transmission")
        {
          Global.transmissionHealth -= CarEngine.currentSpeed / 19450;
        }
        else if (componentName == "fuel")
        {
          Global.fuelHealth -= CarEngine.currentSpeed / 19500;
        }
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
