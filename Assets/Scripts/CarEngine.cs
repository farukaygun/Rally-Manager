using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour
{
    public Transform path;
    public float maxSteerAngle = 45f;

    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public WheelCollider wheelRL;
    public WheelCollider wheelRR;
    
    public float maxMotorTorque = 80f;
    public float maxBrakeTorque = 150f;
    public float currentSpeed;
    public float maxSpeed = 100f;
    public Vector3 centerOfMass;
    public Texture2D textureNormal;
    public Texture2D textureBreaking;
    public Renderer carRenderer;
    public bool isBraking = false;


    List<Transform> nodes;
    int currentNode = 0;

    void Start() 
    {
        // tekerlek dönüşü için
        GetComponent<Rigidbody>().centerOfMass = centerOfMass;

        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++) {
            if (pathTransforms[i] != path.transform) {
                nodes.Add(pathTransforms[i]);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate() 
    {
        ApplySteer(); // tekerlek sağ sol dönüş
        Drive(); // Sürmeye başla
        CheckWaypointDistance(); // Yolu kontrol et
        Breaking(); // Fren yap -manuel-
    }

    void ApplySteer()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        // magnitude vektörün uzunluğunu verir.
        // tekerlek açılarını hesaplar
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;

        ////Teker 25 dereceden daha fazla dönmüşse fren yap
        //if (wheelFL.steerAngle > 25)
        //{
        //    isBraking = true;
        //}
        //else isBraking = false;

        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 10f)
        {
            isBraking = true;
        }
    }

    void Drive() 
    {
        // fizikte lastik hızı formülü
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;

        // aracın hızı maksimuma ulaşmamışsa
        if (currentSpeed < maxSpeed && !isBraking) {
            // motor torkunu maksimum olarak ayarla
            wheelFL.motorTorque = maxMotorTorque;
            wheelFR.motorTorque = maxMotorTorque;
        } else {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
        }
    }

    void CheckWaypointDistance() 
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 0.5f) {
            // eğer son node'a gelindiyse
            if(currentNode == nodes.Count - 1) {
                // ilk nodu'u hedef olarak ayarla
                currentNode = 0;
            } else {
                // sonraki node'a geç
                currentNode++;
            }
        }
    }

    private void Breaking() 
    {
        if (isBraking)
        {
            carRenderer.material.mainTexture = textureBreaking;
            wheelRL.brakeTorque = maxBrakeTorque;
            wheelRR.brakeTorque = maxBrakeTorque;
        }
        else
        {
            carRenderer.material.mainTexture = textureNormal;
            wheelRL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;
        }
    }
}
