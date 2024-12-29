using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class FirstPersonCamera : NetworkBehaviour
{
    public Transform player;
    public float mouseSensitivity = 2f, cameraVerticalRotation = 0f;
    // Start is called before the first frame update
    public override void OnNetworkSpawn()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if(!IsLocalPlayer)
        {
            GetComponent<Camera>().enabled = false;
            GetComponent<AudioListener>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsLocalPlayer) return;
        
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -50f, 70f);

        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

        player.Rotate(Vector3.up * inputX);
    }
}
