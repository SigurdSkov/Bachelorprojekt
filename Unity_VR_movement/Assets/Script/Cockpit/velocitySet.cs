using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class velocitySet : MonoBehaviour
{
    Rigidbody wagonBody;
    GameObject speedHandle;
    GameObject turnHandle;
    float speed = 5f;

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
            wagonBody.velocity = transform.right*speed;//new Vector3(1,0,0);
        }
        else if (speedHandle.transform.localEulerAngles.z > 40f && speedHandle.transform.localEulerAngles.z < 50f)
        {
            wagonBody.velocity = -transform.right*speed;//= new Vector3(-1, 0, 0);
        }


        if (turnHandle.transform.localEulerAngles.z < 320f && turnHandle.transform.localEulerAngles.z > 310f)
        {
            //transform.transform.Rotate(vector3 eulers, new Quaternion(0,0,0,0));
            transform.Rotate(new Vector3(0, 20, 0) * Time.deltaTime);
        }
        else if (turnHandle.transform.localEulerAngles.z > 40f && turnHandle.transform.localEulerAngles.z < 50f)
        {
            transform.Rotate(new Vector3(0, -20, 0) * Time.deltaTime);
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
