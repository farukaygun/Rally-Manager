using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//struct pilotSpecs
//{
//    /*
//     * Zemin sürtünme katsayýlarý için deðiþkenler.
//     * Extremum noktasý sürtünmenin maksimum olduðu noktadýr.
//     * Asymptote noktasý sürtünmenin dengelendiði noktadýr.
//     * https://docs.unity3d.com/Manual/class-WheelCollider.html
//     */
//    public float extremumSlip;
//    public float extremumValue;
//    public float asymptoteSlip;
//    public float asymptoteValue;
//    /*
//     * stiffness, yukarýdaki deðiþkenlerin etkisinin çarpanýdýr. 
//     * Default olarak 1'dir. 2 olarak deðiþtirilirse yukarýdaki deðerlerin etkisi 2 katýna çýkar. 
//     * Genellikle runtime sýrasýnda deðiþtirilir.
//     */
//    public float stiffness;

//    // Constructor
//    public pilotSpecs(float extremumSlip, float extremumValue, float asymptoteSlip, float asymptoteValue, float stiffness)
//    {
//        this.extremumSlip = extremumSlip;
//        this.extremumValue = extremumValue;
//        this.asymptoteSlip = asymptoteSlip;
//        this.asymptoteValue = asymptoteValue;
//        this.stiffness = stiffness;
//    }
//}

public class PilotController : MonoBehaviour
{
    private WheelFrictionCurve _sidewaysFriction;
    private WheelCollider wheelCollider;

    pilotSpecs ps;

    void Start()
    {
        wheelCollider = gameObject.GetComponent<WheelCollider>();
        ps = new pilotSpecs(10f, 10f, 10f, 10f, 10f);
    }

    // Pilotun özelliklerini araç özelliklerine çevir
    public void CalculatePilotSpecs()
    {
        float newExtremumSlip = ps.extremumSlip * 0.002f; // 0-1 arasý
        float newExtremumValue = ps.extremumValue * 0.002f; // 0-1 arasý
        float newAsymtoteSlip = ps.asymptoteSlip * 0.002f; // 0-1 arasý
        float newAsymtoteValue = ps.asymptoteValue * 0.002f; // 0.75 sabit þimdilik
        float newStiffness = ps.stiffness * 0.002f; // 0-1 arasý

        _sidewaysFriction.extremumSlip = wheelCollider.sidewaysFriction.extremumSlip + newExtremumSlip;
        _sidewaysFriction.extremumValue = wheelCollider.sidewaysFriction.extremumValue + newExtremumValue;
        _sidewaysFriction.asymptoteSlip = wheelCollider.sidewaysFriction.asymptoteSlip + newAsymtoteSlip;
        _sidewaysFriction.asymptoteValue = wheelCollider.sidewaysFriction.asymptoteValue + newAsymtoteValue;
        _sidewaysFriction.stiffness = wheelCollider.sidewaysFriction.stiffness + newStiffness;

        wheelCollider.sidewaysFriction = _sidewaysFriction;
    }
}
