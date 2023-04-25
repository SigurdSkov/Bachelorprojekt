using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skateboard : MonoBehaviour
{
    Transform CameraTransform;
    Rigidbody body;
    float speed = 0f;
    float turnSpeed = 0;
    float leanForward;
    float leanSide;

    // Start is called before the first frame update
    void Start()
    {
        CameraTransform = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).transform;
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        leanForward = CameraTransform.localPosition.x;
        leanSide = CameraTransform.localPosition.z;

        if (leanForward > 0.1F || leanForward < -0.1F)
            speed = leanForward;
        else
            speed = 0;

        if (leanSide > 0.1F || leanSide < -0.1F)
            turnSpeed = leanSide;
        else
            turnSpeed = 0;

        body.velocity += CameraTransform.forward * speed * Time.deltaTime;
        transform.Rotate(new Vector3(0, turnSpeed, 0) * Time.deltaTime);
    }
}
