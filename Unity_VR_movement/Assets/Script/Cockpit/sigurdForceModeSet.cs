using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sigurdForceModeSet : MonoBehaviour
{
    Rigidbody wagonBody;
    GameObject speedHandle;
    GameObject turnHandle;
    float speed = 100f;
    float rightSpeed = 0f;
    float leftSpeed = 0f;
    float maxSpeed = 5f;

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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (speedHandle.transform.localPosition.x > 0.1f)
        {
            if (wagonBody.velocity.magnitude < maxSpeed)
            {
                wagonBody.AddForce(transform.right * speed, ForceMode.Acceleration);
                Debug.Log("Moving with speed: " + wagonBody.velocity.magnitude);
            }
        }
        else if (speedHandle.transform.localPosition.x < -0.1f)
        {
            if (wagonBody.velocity.magnitude < maxSpeed)
            {
                wagonBody.AddForce(-transform.right * speed, ForceMode.Acceleration);
            }
        }


        if (turnHandle.transform.localPosition.x > 0.1f)
        {
            if (rightSpeed < 20)
                rightSpeed += 0.2f;
            transform.Rotate(new Vector3(0, (rightSpeed - leftSpeed), 0) * Time.deltaTime);
        }
        else if (turnHandle.transform.localPosition.x < -0.1f)
        {
            if (leftSpeed < 20)
                leftSpeed += 0.2f;
            transform.Rotate(new Vector3(0, (rightSpeed - leftSpeed), 0) * Time.deltaTime);
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
