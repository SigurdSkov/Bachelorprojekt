using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armswing : MonoBehaviour
{
    Transform CameraTransform;
    GameObject rS0, rS1, lS0, lS1;
    float speed = 1;
    Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();

        rS0 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        rS1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        lS0 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        lS1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        CameraTransform = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).transform;

        rS0.transform.SetParent(CameraTransform);
        rS1.transform.SetParent(CameraTransform);
        lS0.transform.SetParent(CameraTransform);
        lS1.transform.SetParent(CameraTransform);

        rS0.transform.position = new Vector3(0.3f,    0,0.6f);
        rS1.transform.position = new Vector3(0.3f,-0.6f,   0);
        lS0.transform.position = new Vector3(0.3f, 0, 0.6f);
        lS1.transform.position = new Vector3(0.3f, -0.6f, 0);
    }

    // Update is called once per frame
    void Update()
    {
//        if((rS0.collision && lS1.collision) || (rS1.collision && lS0.collision))
//        {
//            Vector3 forward = CameraTransform.forward;
//            forward.y = 0;
//            body.AddForce(forward * speed, ForceMode.Impulse);
//        }
    }
}
