using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class velocitySet : MonoBehaviour
{
    Rigidbody wagonBody;
    GameObject speedHandle;
    GameObject turnHandle;
    float baseSpeed = 5f;
    float speed = 0f;
    float turnSpeed = 0.0f;
    float triggerAngleForward = 340;
    float triggerAngleBackward = 20;
    float angleDivisor;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<Rigidbody>();
        wagonBody = transform.GetComponent<Rigidbody>();

        //wagonBody.constraints = RigidbodyConstraints.FreezeRotationX & RigidbodyConstraints.FreezeRotationZ;
        wagonBody.constraints = RigidbodyConstraints.FreezeRotation;
        wagonBody.drag = 0;
        wagonBody.useGravity = true;
        //wagonBody.isKinematic = true;

        speedHandle = transform.GetChild(0).GetChild(0).gameObject;
        turnHandle = transform.GetChild(1).GetChild(0).gameObject; //Actually tracks the lever

        angleDivisor = 45 - triggerAngleBackward;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (speedHandle.transform.localEulerAngles.z <= triggerAngleForward && speedHandle.transform.localEulerAngles.z >= 310f) //315
        {
            speed = (speedHandle.transform.localEulerAngles.z - triggerAngleForward) * -1 / angleDivisor;
            wagonBody.velocity = transform.right * baseSpeed * speed;//new Vector3(1,0,0);
        }
        else if (speedHandle.transform.localEulerAngles.z >= triggerAngleBackward && speedHandle.transform.localEulerAngles.z <= 50f) //45
        {
            speed = (speedHandle.transform.localEulerAngles.z - triggerAngleBackward) / angleDivisor;
            wagonBody.velocity = -transform.right * baseSpeed * speed;//= new Vector3(-1, 0, 0);
        }


        if (turnHandle.transform.localEulerAngles.z <= triggerAngleForward && turnHandle.transform.localEulerAngles.z >= 310f)
        {
            turnSpeed = (turnHandle.transform.localEulerAngles.z - triggerAngleForward) * -1 / angleDivisor;
            transform.Rotate(new Vector3(0, 20 * turnSpeed, 0) * Time.deltaTime);
        }
        else if (turnHandle.transform.localEulerAngles.z >= triggerAngleBackward && turnHandle.transform.localEulerAngles.z <= 50f)
        {
            turnSpeed = (turnHandle.transform.localEulerAngles.z - triggerAngleBackward) / angleDivisor;
            transform.Rotate(new Vector3(0, -20 * turnSpeed, 0) * Time.deltaTime);
        }
    }

    private void OnDestroy()
    {
        foreach (Transform child in transform)
        {
            if (GetComponent<AddWagonVelocitySet>() != null)
            {
                Debug.Log(child + "    " + child.transform.parent);
                child.transform.parent = null;
            }
        }
    }
}
