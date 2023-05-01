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
    public Transform combatLookAt;
    public GameObject thirdPersonCamera;
    public GameObject combatCamera;
    public CameraStyle currentStyle;

    public enum CameraStyle
    {
        Basic,
        Combat
    }

    private void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update(){
        // Switch camera styles
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchCameraStyle(CameraStyle.Basic);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchCameraStyle(CameraStyle.Combat);

        // Rotate orientation
        Vector3 viewDirection = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDirection.normalized;

        // Rotate player object depending camera style
        if(currentStyle == CameraStyle.Basic)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 input = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if(input != Vector3.zero)
                playerObj.forward = Vector3.Slerp(playerObj.forward, input.normalized, Time.deltaTime * rotationSpeed);
        }
        else if(currentStyle == CameraStyle.Combat)
        {
            Vector3 directionToCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            orientation.forward = directionToCombatLookAt.normalized;

            playerObj.forward = directionToCombatLookAt.normalized;
        }
    }

    private void SwitchCameraStyle(CameraStyle style)
    {
        thirdPersonCamera.SetActive(false);
        combatCamera.SetActive(false);
        
        if(style == CameraStyle.Basic) thirdPersonCamera.SetActive(true);
        if(style == CameraStyle.Combat) combatCamera.SetActive(true);

        currentStyle = style;
    }
}
