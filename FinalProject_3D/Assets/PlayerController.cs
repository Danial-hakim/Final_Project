using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;

    public float turnSmoothTime = 0.1f;

    float turnSmoothVelocity;

    public GameObject cameraObject;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        handleCameraZoom();
    }

    public void handleCameraZoom()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cameraObject.transform.LookAt(cursorPos);

            // Zoom in on the cursor position
            cameraObject.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView = 20;
        }
        else if(Input.GetMouseButton(1))
        {
            Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cameraObject.transform.LookAt(cursorPos);

            // Zoom out on the cursor position
            cameraObject.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView = 60;
        }
        else
        {
            cameraObject.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView = 40;
            cameraObject.transform.LookAt(transform.position + new Vector3(0, 5, 5));
        }
    }
}
