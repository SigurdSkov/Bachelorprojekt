using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class velocitySet : MonoBehaviour
{
    Rigidbody wagonBody;
    GameObject speedHandle;
    GameObject turnHandle;
    float speed = 1f;

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
        if (speedHandle.transform.localPosition.x > 0.1f)
        {
            wagonBody.velocity += transform.right*speed;//new Vector3(1,0,0);
            //wagonBody.velocity.Set(10,0,0); //virker ikke //speedHandle.transform.Translate(new Vector3(-0.1F, 0,0)); virker
        }
        else if (speedHandle.transform.localPosition.x < -0.1f)
        {
            wagonBody.velocity -= transform.right*speed;//= new Vector3(-1, 0, 0);
            //wagonBody.velocity.Set(10, 0, 0); //virker ikke //speedHandle.transform.Translate(new Vector3(0.1F, 0,0)); virker
        }


        if (turnHandle.transform.localPosition.x > 0.1f)
        {
            //transform.transform.Rotate(vector3 eulers, new Quaternion(0,0,0,0));
            transform.Rotate(new Vector3(0, 20, 0) * Time.deltaTime);
        }
        else if (turnHandle.transform.localPosition.x < -0.1f)
        {
            transform.Rotate(new Vector3(0, -20, 0) * Time.deltaTime);
        }
    }
}
