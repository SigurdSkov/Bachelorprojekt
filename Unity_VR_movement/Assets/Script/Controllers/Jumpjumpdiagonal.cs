using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.EventSystems;
using JumpingButtonMap;
using Unity.VisualScripting;

public class Jumpjumpdiagonal : MonoBehaviour
{
    // Start is called before the first frame update
    float StandardHeight = 5f;
    Transform CameraTransform;
    Rigidbody body;
    float previousHeight = 0;
    float currentHeight = 0;
    [SerializeField]
    float speed = 2F;
    bool pressed = false;

    private bool right = true;
    Vector3 moveDirection;
    Vector3 forward;
    Vector3 up;
    private bool moving = false;

    private UnityEngine.XR.InputDevice rightHandDevice, leftHandDevice;
    private void Start()
    {
        CameraTransform = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).transform;
        body = gameObject.GetComponent<Rigidbody>();
        body.drag = 0.5F;
        body.mass = 5;
    }

    private void Update()
    {
        Jump();
    }

    void Jump()
    {
        rightHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        leftHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        currentHeight = CameraTransform.localPosition.y;

        SetStandardHeight();

        if (currentHeight > StandardHeight + 0.05)
        {
            forward = CameraTransform.forward;
            moveDirection = CameraTransform.right;
            up = CameraTransform.up;

            moving = true;
            if (!right)
                moveDirection = moveDirection * -1;
            body.AddForce(moveDirection * speed + forward * speed * 1.5F + up * speed, ForceMode.Impulse);
            Debug.Log(this.GetType().ToString() + ": Force added");
        }
        else if (moving == true)
        {
            if (right)
                right = false;
            else
                right = true;
            moving = false;
            Debug.Log("Going right: " + right);
        }

        pressed = false;
    }

    void SetStandardHeight()
    {
        if (leftHandDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out pressed) && pressed) //(rightHandDevice.IsPressed(InputHelpers.Button.PrimaryButton, pressed))
        {
            StandardHeight = currentHeight; //localposition, because absolute breaks when you hit a hill
            Debug.Log("Button input detected: " + StandardHeight);
        }
    }
}


