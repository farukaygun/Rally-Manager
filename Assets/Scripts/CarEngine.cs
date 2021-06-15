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
  public static float maxBrakeTorque = 50f;
  [SerializeField] public static float currentSpeed;
  public static float maxSpeed = 100f;
  public Vector3 centerOfMass;
  public Texture2D textureNormal;
  public Texture2D textureBreaking;
  public Renderer carRenderer;
  public bool isBraking = false;
  List<Transform> nodes;
  int currentNode = 0;

  void Start()
  {
    path = GameObject.Find("Path").transform;

    // tekerlek dönüşü için
    GetComponent<Rigidbody>().centerOfMass = centerOfMass;

    Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
    nodes = new List<Transform>();

    for (int i = 0; i < pathTransforms.Length; i++)
    {
      if (pathTransforms[i] != path.transform)
      {
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
                             // CurveBraking(DrivingTypeController.drivingTypeBrakeDistance, DrivingTypeController.drivingTypeBrakeSpeedLimit); // self-brake
    Brake();
    Breaking();
  }

  void ApplySteer()
  {
    Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
    // magnitude vektörün uzunluğunu verir.
    // tekerlek açılarını hesaplar
    float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
    wheelFL.steerAngle = newSteer;
    wheelFR.steerAngle = newSteer;
  }

  void Drive()
  {
    // fizikte lastik hızı formülü
    currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;

    // aracın hızı maksimuma ulaşmamışsa
    if (currentSpeed < maxSpeed && !isBraking)
    {
      // motor torkunu maksimum olarak ayarla
      wheelFL.motorTorque = maxMotorTorque;
      wheelFR.motorTorque = maxMotorTorque;
    }
    else
    {
      wheelFL.motorTorque = 0;
      wheelFR.motorTorque = 0;
    }
  }

  void CheckWaypointDistance()
  {
    if (Vector3.Distance(transform.position, nodes[currentNode].position) < 0.5f)
    {
      // eğer son node'a gelindiyse
      if (currentNode == nodes.Count - 1)
      {
        // ilk nodu'u hedef olarak ayarla
        currentNode = 0;
      }
      else
      {
        // sonraki node'a geç
        currentNode++;
      }
    }
  }

  void Brake()
  {
    if (nodes[currentNode].tag == "Curve")
      isBraking = true;

    else isBraking = false;
  }

  void Breaking()
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

  void CurveBraking(float drivingTypeBrakeDistance, float drivingTypeSpeedLimit)
  {
    // playerLayer =3, groundLayer = 6, 
    // 7, 8, 9 yol tipleri
    int layerMask = ~(1 << 3 | 1 << 6 | 1 << 7 | 1 << 8 | 1 << 9);

    /*
     * drivingTypeBrakeDistance ve drivingTypeBrakeDistance/2 arasında yol dışındaki gizli küplere raycast ışını çarpıyorsa
     * ve aracın hızı drivingTypeSpeedLimit'den fazlaysa fren yap.
     * değilse fren yapma
     */
    RaycastHit hit;
    if (Physics.Raycast(transform.position, transform.forward, out hit, drivingTypeBrakeDistance, layerMask)
        && !Physics.Raycast(transform.position, transform.forward, out hit, drivingTypeBrakeDistance / 2, layerMask) == false
        && currentSpeed > drivingTypeSpeedLimit)
    {
      Debug.DrawRay(transform.position, transform.forward, Color.red, drivingTypeBrakeDistance);
      isBraking = true;
    }
    else isBraking = false;
  }
}
