using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jerkSet : MonoBehaviour
{
    Rigidbody wagonBody;
    GameObject speedHandle;
    GameObject turnHandle;
    float forwardSpeed = 0f;
    float backwardSpeed = 0f;
    float rightSpeed = 0f;
    float leftSpeed = 0f;
    float maxSpeed = 5f;
    Vector3 previousVelocity;
    float speed = 0f;
    float turnSpeed = 0f;

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
        previousVelocity = transform.right;

        angleDivisor = 45 - triggerAngleBackward;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (speedHandle.transform.localEulerAngles.z <= triggerAngleForward && speedHandle.transform.localEulerAngles.z >= 310f)
        {
            speed = ((speedHandle.transform.localEulerAngles.z - triggerAngleForward) * -1) / angleDivisor;
            if (forwardSpeed < 5)
                forwardSpeed += 0.03f*speed;
            wagonBody.velocity = transform.right * (forwardSpeed - backwardSpeed);//new Vector3(1,0,0);
            Debug.Log("Moving");
            //previousVelocity = wagonBody.velocity;
        }
        else if (speedHandle.transform.localEulerAngles.z >= triggerAngleBackward && speedHandle.transform.localEulerAngles.z <= 50f)
        {
            speed = (speedHandle.transform.localEulerAngles.z - triggerAngleBackward) / angleDivisor;
            if (backwardSpeed < 5)
                backwardSpeed += 0.03f*speed;
            wagonBody.velocity = transform.right * (forwardSpeed - backwardSpeed);//= new Vector3(-1, 0, 0);
            //previousVelocity = wagonBody.velocity;
        }
        else
        {
            //previousVelocity = 0.9f * previousVelocity;
            wagonBody.velocity = transform.right * (forwardSpeed - backwardSpeed); //previousVelocity;
        }
        if (forwardSpeed > 0)
            forwardSpeed -= 0.01f;
        if (backwardSpeed > 0)
            backwardSpeed -= 0.01f;
        //        if (speedHandle.transform.localEulerAngles.z < 330f && speedHandle.transform.localEulerAngles.z > 310f)
        //        {
        //            if (wagonBody.velocity.magnitude < maxSpeed)
        //            {
        //                speed = (speedHandle.transform.localEulerAngles.z - 300) / 20;
        //                if (forwardSpeed < maxSpeed)
        //                    forwardSpeed += 0.05f*speed;
        //                if (forwardSpeed - backwardSpeed < 0)
        //                    previousVelocity = -transform.right * ((forwardSpeed - backwardSpeed) * (forwardSpeed - backwardSpeed));
        //                else
        //                    previousVelocity = transform.right * (forwardSpeed - backwardSpeed); //+ previousVelocity * (forwardSpeed - backwardSpeed)
        //                wagonBody.velocity = previousVelocity;
        //            }
        //        }
        //        else if (speedHandle.transform.localEulerAngles.z > 40f && speedHandle.transform.localEulerAngles.z < 50f)
        //        {
        //            if (wagonBody.velocity.magnitude < maxSpeed)
        //            {
        //                if (backwardSpeed < maxSpeed)
        //                    backwardSpeed += 0.05f*speed;
        //                if (forwardSpeed - backwardSpeed < 0)
        //                    previousVelocity = -transform.right * (((forwardSpeed - backwardSpeed) * (forwardSpeed - backwardSpeed)))/2;
        //                else
        //                    previousVelocity = transform.right * (forwardSpeed - backwardSpeed);
        //                wagonBody.velocity = previousVelocity; //If not jerk, then try += instead
        //            }
        //        }
        //        else
        //        {
        //            previousVelocity = previousVelocity * 0.99f; //transform.right * (forwardSpeed - backwardSpeed);// * (forwardSpeed - backwardSpeed)) * 0.99f;
        //            wagonBody.velocity = previousVelocity; //Dette sætter det direkte. Det går ikke. I stedet tag højde for den nuværende speed, denne påvirkes med en faktor lig hvad der bliver tilføjet
        //        }
        //        if (forwardSpeed > 0)
        //            forwardSpeed -= 0.01f;
        //        if (backwardSpeed > 0)
        //            backwardSpeed -= 0.01f;


        if (turnHandle.transform.localEulerAngles.z <= triggerAngleForward && turnHandle.transform.localEulerAngles.z >= 310f)
        {
            if (rightSpeed < 20)
                rightSpeed += 0.2f;
            turnSpeed = (turnHandle.transform.localEulerAngles.z - triggerAngleForward) * -1 / angleDivisor;
            transform.Rotate(new Vector3(0, (rightSpeed - leftSpeed) * 0.5f * turnSpeed, 0) * Time.deltaTime);
        }
        else if (turnHandle.transform.localEulerAngles.z >= 40f && turnHandle.transform.localEulerAngles.z <= 50f)
        {
            if (leftSpeed < 20)
                leftSpeed += 0.2f;
            turnSpeed = (turnHandle.transform.localEulerAngles.z - triggerAngleForward) / angleDivisor;
            transform.Rotate(new Vector3(0, (rightSpeed - leftSpeed) * 0.5f * turnSpeed, 0) * Time.deltaTime);
        }
        else
        {
            transform.Rotate(new Vector3(0, (rightSpeed - leftSpeed) * 0.4f, 0) * Time.deltaTime);
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
