using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    private void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update(){

        // Rotate orientation
        Vector3 view = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = view.normalized;

        // Rotate player object
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 input = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(input != Vector3.zero)
            playerObj.forward = Vector3.Slerp(playerObj.forward, input.normalized, Time.deltaTime * rotationSpeed);
    }
}
