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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (speedHandle.transform.localPosition.x > 0.1f)
        {
            if (wagonBody.velocity.magnitude < maxSpeed)
            {
                if (forwardSpeed < maxSpeed)
                    forwardSpeed += 0.02f;
                if (forwardSpeed - backwardSpeed < 0)
                    previousVelocity = -transform.right * ((forwardSpeed - backwardSpeed) * (forwardSpeed - backwardSpeed));
                else
                    previousVelocity = transform.right * (forwardSpeed - backwardSpeed); //+ previousVelocity * (forwardSpeed - backwardSpeed)
                wagonBody.velocity = previousVelocity;
            }
        }
        else if (speedHandle.transform.localPosition.x < -0.1f)
        {
            if (wagonBody.velocity.magnitude < maxSpeed)
            {
                if (backwardSpeed < maxSpeed)
                    backwardSpeed += 0.02f;
                if (forwardSpeed - backwardSpeed < 0)
                    previousVelocity = -transform.right * (((forwardSpeed - backwardSpeed) * (forwardSpeed - backwardSpeed)))/2;
                else
                    previousVelocity = transform.right * (forwardSpeed - backwardSpeed);
                wagonBody.velocity = previousVelocity; //If not jerk, then try += instead
            }
        }
        else
        {
            previousVelocity = previousVelocity * 0.99f; //transform.right * (forwardSpeed - backwardSpeed);// * (forwardSpeed - backwardSpeed)) * 0.99f;
            wagonBody.velocity = previousVelocity; //Dette sætter det direkte. Det går ikke. I stedet tag højde for den nuværende speed, denne påvirkes med en faktor lig hvad der bliver tilføjet
        }
        if (forwardSpeed > 0)
            forwardSpeed -= 0.01f;
        if (backwardSpeed > 0)
            backwardSpeed -= 0.01f;


        if (turnHandle.transform.localPosition.x > 0.1f)
        {
            if (rightSpeed < 20)
                rightSpeed += 0.2f;
            transform.Rotate(new Vector3(0, (rightSpeed - leftSpeed) * 0.5f, 0) * Time.deltaTime);
        }
        else if (turnHandle.transform.localPosition.x < -0.1f)
        {
            if (leftSpeed < 20)
                leftSpeed += 0.2f;
            transform.Rotate(new Vector3(0, (rightSpeed - leftSpeed) * 0.5f, 0) * Time.deltaTime);
            Debug.Log(rightSpeed - leftSpeed);
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
            if (GetComponent<AddWagonJerkSet>() != null)
            {
                Debug.Log(child + "    " + child.transform.parent);
                child.transform.parent = null;
            }
        }
    }
}
