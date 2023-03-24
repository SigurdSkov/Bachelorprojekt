using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using JumpingButtonMap;
using Unity.VisualScripting;

public class JumpMove : MonoBehaviour
{
    // Start is called before the first frame update
    float StandardHeight = 1.8f;
    Transform CameraTransform;
    Rigidbody body;
    float previousHeight = 0;
    float currentHeight = 0;
    [SerializeField]
    float speed;


    //XRNode controllerLeft = XRNode.LeftHand;
    //InputActionReference buttonXRef;
    //InputAction buttonX;
    //InputAction buttonX; //.CallbackContext

    bool pressed = false;

    void Start()
    {
        CameraTransform = this.gameObject.transform.GetChild(0).transform;
        body = this.gameObject.GetComponent<Rigidbody>();
        //buttonX = GetComponent<InputActionAsset>().FindAction("XR/PrimaryButton");
        //buttonX.performed += ;
    }

    // Update is called once per frame
    void Update()
    {
        //pressed = buttonX.ReadValue<bool>();
        currentHeight = CameraTransform.position.y;
        //if (Input.GetButtonDown("PrimaryButton" + controllerLeft))
        if (pressed)
        {
            Debug.Log("Button input detected");
            StandardHeight = CameraTransform.position.y;
            Debug.Log(StandardHeight);
        }
        if (CameraTransform.position.y > StandardHeight + 0.05)
        {
            body.AddForce(CameraTransform.forward*(currentHeight/StandardHeight)*speed, ForceMode.Force);
        }
        //previousHeight = CameraTransform.position.y;
    }
}
