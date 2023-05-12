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

public class Jumpmovestrafe : MonoBehaviour
{
    // Start is called before the first frame update
    float StandardHeight = 5f;
    Transform CameraTransform;
    Rigidbody body;
    float currentHeight = 0;
    [SerializeField]
    float speed = 1F;
    bool pressed = false;
    private bool right = true;
    Vector3 moveDirection;
    Vector3 forward;
    private bool moving = false;

    private UnityEngine.XR.InputDevice rightHandDevice, leftHandDevice;
    private void Start()
    {
        CameraTransform = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).transform;
        body = gameObject.GetComponent<Rigidbody>();
        body.drag = 1;
        body.mass = 2;
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
            moveDirection = CameraTransform.right;
            moving = true;
            if (!right)
                moveDirection = moveDirection * -1;
            moveDirection.y = 0;
            body.AddForce(moveDirection * speed, ForceMode.Impulse);
            Debug.Log(this.GetType().ToString() + ": Force added");
        }
        else if (moving == true)
        {
            if (right)
                right = false;
            else
                right = true;
            moving = false;
        }

        pressed = false;
    }

    void SetStandardHeight()
    {
        if (leftHandDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out pressed) && pressed) //(rightHandDevice.IsPressed(InputHelpers.Button.PrimaryButton, pressed))
        {
            StandardHeight = currentHeight; //localposition, because absolute breaks when you hit a hill
        }
    }
    private void OnDestroy()
    {
        body.mass = 1;
        body.drag = 0;
    }
}


