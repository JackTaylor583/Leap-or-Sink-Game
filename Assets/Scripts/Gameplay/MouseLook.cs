using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;
    float xRotation = 0.0f;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Locks cursor to the window
    }

    void Update() // Update is called once per frame
    {
        // Gets mouse X & Y movement 
        float mouseX = Input.GetAxis("Mouse X") * StaticSettings.Sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * StaticSettings.Sensitivity * Time.deltaTime;

        // allows player to look up/down
        xRotation -= mouseY;
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Makes sure player cant look more then 90 degrees both up/down
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

        // Rotates player based on look direction
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
