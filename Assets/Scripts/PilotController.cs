using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//struct pilotSpecs
//{
//    /*
//     * Zemin s�rt�nme katsay�lar� i�in de�i�kenler.
//     * Extremum noktas� s�rt�nmenin maksimum oldu�u noktad�r.
//     * Asymptote noktas� s�rt�nmenin dengelendi�i noktad�r.
//     * https://docs.unity3d.com/Manual/class-WheelCollider.html
//     */
//    public float extremumSlip;
//    public float extremumValue;
//    public float asymptoteSlip;
//    public float asymptoteValue;
//    /*
//     * stiffness, yukar�daki de�i�kenlerin etkisinin �arpan�d�r. 
//     * Default olarak 1'dir. 2 olarak de�i�tirilirse yukar�daki de�erlerin etkisi 2 kat�na ��kar. 
//     * Genellikle runtime s�ras�nda de�i�tirilir.
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

    // Pilotun �zelliklerini ara� özelliklerine �evir
    public void CalculatePilotSpecs()
    {
        float newExtremumSlip = ps.extremumSlip * 0.002f; // 0-1 arası
        float newExtremumValue = ps.extremumValue * 0.002f; // 0-1 aras�
        float newAsymtoteSlip = ps.asymptoteSlip * 0.002f; // 0-1 aras�
        float newAsymtoteValue = ps.asymptoteValue * 0.002f; // 0.75 sabit �imdilik
        float newStiffness = ps.stiffness * 0.002f; // 0-1 aras�

        _sidewaysFriction.extremumSlip = wheelCollider.sidewaysFriction.extremumSlip + newExtremumSlip;
        _sidewaysFriction.extremumValue = wheelCollider.sidewaysFriction.extremumValue + newExtremumValue;
        _sidewaysFriction.asymptoteSlip = wheelCollider.sidewaysFriction.asymptoteSlip + newAsymtoteSlip;
        _sidewaysFriction.asymptoteValue = wheelCollider.sidewaysFriction.asymptoteValue + newAsymtoteValue;
        _sidewaysFriction.stiffness = wheelCollider.sidewaysFriction.stiffness + newStiffness;

        wheelCollider.sidewaysFriction = _sidewaysFriction;
    }
}
