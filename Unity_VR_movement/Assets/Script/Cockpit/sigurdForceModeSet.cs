using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sigurdForceModeSet : MonoBehaviour
{
    Rigidbody wagonBody;
    GameObject speedHandle;
    GameObject turnHandle;
    float baseSpeed = 90f;
    float speed = 0f;
    float turnSpeed = 0f;
    float rightSpeed = 0f;
    float leftSpeed = 0f;
    float maxSpeed = 5f;

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
        turnHandle = transform.GetChild(1).GetChild(0).gameObject;

        angleDivisor = 45 - triggerAngleBackward;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (speedHandle.transform.localEulerAngles.z <= triggerAngleForward && speedHandle.transform.localEulerAngles.z >= 310f)
        {
            if (wagonBody.velocity.magnitude < maxSpeed)
            {
                speed = ((speedHandle.transform.localEulerAngles.z - triggerAngleForward) * -1) / angleDivisor;
                wagonBody.AddForce(transform.right * baseSpeed * speed, ForceMode.Acceleration);
                Debug.Log("Moving with speed: " + wagonBody.velocity.magnitude);
            }
        }
        else if (speedHandle.transform.localEulerAngles.z >= triggerAngleBackward && speedHandle.transform.localEulerAngles.z <= 50f)
        {
            speed = (speedHandle.transform.localEulerAngles.z - triggerAngleBackward) / angleDivisor;
            if (wagonBody.velocity.magnitude < maxSpeed)
            {
                wagonBody.AddForce(-transform.right * baseSpeed * speed, ForceMode.Acceleration);
            }
        }


        if (turnHandle.transform.localEulerAngles.z <= triggerAngleForward && turnHandle.transform.localEulerAngles.z >= 310f)
        {
            if (rightSpeed < 20)
                rightSpeed += 0.2f;
            turnSpeed = (turnHandle.transform.localEulerAngles.z - triggerAngleForward) * -1 / angleDivisor;
            transform.Rotate(new Vector3(0, (rightSpeed - leftSpeed) * turnSpeed, 0) * Time.deltaTime);
        }
        else if (turnHandle.transform.localEulerAngles.z >= triggerAngleBackward && turnHandle.transform.localEulerAngles.z <= 50f)
        {
            if (leftSpeed < 20)
                leftSpeed += 0.2f;
            turnSpeed = (turnHandle.transform.localEulerAngles.z - triggerAngleForward) / angleDivisor;
            transform.Rotate(new Vector3(0, (rightSpeed - leftSpeed) * turnSpeed, 0) * Time.deltaTime);
        }
        else
        {
            transform.Rotate(new Vector3(0, (rightSpeed - leftSpeed), 0) * Time.deltaTime);
        }
        if (rightSpeed > 0)
            rightSpeed -= 0.1f;
        if (leftSpeed > 0)
            leftSpeed -= 0.1f;
    }
    private void OnDestroy()
    {
        foreach (Transform child in transform)
        {
            if (GetComponent<AddWagonJerkSet>() != null)
            {
                Debug.Log(child + "    " + child.transform.parent);
                child.transform.parent = null;
            }
        }
    }
}
