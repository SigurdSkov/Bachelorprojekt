using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class accelerationSet : MonoBehaviour
{
    Rigidbody wagonBody;
    GameObject speedHandle;
    GameObject turnHandle;
    float forwardSpeed;
    float backwardSpeed;
    float rightSpeed;
    float leftSpeed;

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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (speedHandle.transform.localEulerAngles.z < 320f && speedHandle.transform.localEulerAngles.z > 310f)
        {
            if (forwardSpeed < 5)
                forwardSpeed += 0.05f;
            wagonBody.velocity = transform.right * (forwardSpeed - backwardSpeed);//new Vector3(1,0,0);
            Debug.Log("Moving");
            //previousVelocity = wagonBody.velocity;
        }
        else if (speedHandle.transform.localEulerAngles.z > 40f && speedHandle.transform.localEulerAngles.z < 50f)
        {
            if (backwardSpeed < 5)
                backwardSpeed += 0.05f;
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


        if (turnHandle.transform.localEulerAngles.z < 320f && turnHandle.transform.localEulerAngles.z > 310f)
        {
            if (rightSpeed < 20)
                rightSpeed += 0.2f;
            transform.Rotate(new Vector3(0, (rightSpeed - leftSpeed) * 0.5f, 0) * Time.deltaTime);
        }
        else if (turnHandle.transform.localEulerAngles.z > 40f && turnHandle.transform.localEulerAngles.z < 50f)
        {
            if (leftSpeed < 20)
                leftSpeed += 0.2f;
            transform.Rotate(new Vector3(0, (rightSpeed-leftSpeed)*0.5f, 0) * Time.deltaTime);
        }
        else
        {
            transform.Rotate(new Vector3(0, (rightSpeed - leftSpeed) * 0.5f, 0) * Time.deltaTime);
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
            if (GetComponent<AddWagonAccelerationSet>() != null)
            {
                Debug.Log(child + "    " + child.transform.parent);
                child.transform.parent = null;
            }
        }
    }
}
