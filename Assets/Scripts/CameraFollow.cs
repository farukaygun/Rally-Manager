using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float followHeight = 7f;
    [SerializeField] private float followDistance = 6f;
    [SerializeField] private float followHeightSpeed = 0.9f;
    
    private Transform Player;
    
    private float targetHeight;
    private float currentHeight;
    private float currentRotation;

    private void Awake() {
        Player = GameObject.FindWithTag("Player").transform;
    }

    private void Update() {
        targetHeight = Player.position.y + followHeight;
        currentRotation = transform.eulerAngles.y;
        // kamera yüksekliği için smooth geçiş
        // lerp verilen zamanda smooth geçiş sağlar
        currentHeight = Mathf.Lerp(transform.position.y, targetHeight, followHeightSpeed * Time.deltaTime);
        // kamera dönüş
        Quaternion euler = Quaternion.Euler(0f, currentRotation, 0f);
        Vector3 targetPosition = Player.position - (euler * Vector3.forward) * followDistance;
        targetPosition.y = currentHeight;
        transform.position = targetPosition;
        transform.LookAt(Player); // kamera her zaman oyuncuya doğru bakacak
    }
}
