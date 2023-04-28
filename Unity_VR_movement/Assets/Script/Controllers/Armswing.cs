using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Armswing : MonoBehaviour
{
    Transform CameraTransform;
    GameObject rS0, rS1, lS0, lS1, rightHandCube, leftHandCube;
    Rigidbody body;
    float speed = 3.0f;
    bool rightFoot = false;
    bool leftFoot = false;

    float timeCounter;
    float timeOut = 1000;

    public bool rS0Collision { get; set; }
    public bool rS1Collision { get; set; }
    public bool lS0Collision { get; set; }
    public bool lS1Collision { get; set; }

    List<GameObject> spawnedElements = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();

        rS0 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        rS1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        lS0 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        lS1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        rightHandCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        leftHandCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        rightHandCube.transform.parent = transform.GetChild(0).GetChild(0).GetChild(1);
        leftHandCube.transform.parent = transform.GetChild(0).GetChild(0).GetChild(2);
        rightHandCube.tag = "controller";
        leftHandCube.tag = "controller";
        rightHandCube.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        leftHandCube.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        rightHandCube.transform.localPosition = Vector3.zero;
        leftHandCube.transform.localPosition = Vector3.zero;

        CameraTransform = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).transform;

        rS0.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        rS1.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        lS0.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        lS1.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

        rS0.GetComponent<SphereCollider>().isTrigger = true;
        rS1.GetComponent<SphereCollider>().isTrigger = true;
        lS0.GetComponent<SphereCollider>().isTrigger = true;
        lS1.GetComponent<SphereCollider>().isTrigger = true;

        rS0.AddComponent<rS0CollisionDetector>();
        rS1.AddComponent<rS1CollisionDetector>();
        lS0.AddComponent<lS0CollisionDetector>();
        lS1.AddComponent<lS1CollisionDetector>();

        spawnedElements.Add(rS0);
        spawnedElements.Add(rS1);
        spawnedElements.Add(lS0);
        spawnedElements.Add(lS1);
        spawnedElements.Add(rightHandCube);
        spawnedElements.Add(leftHandCube);
    }

    // Update is called once per frame
    void Update()
    {
        rS0.transform.localPosition = CameraTransform.position - new Vector3(0, CameraTransform.position.y, 0) + CameraTransform.forward * 0.3f + new Vector3(0, 1.45f, 0) + CameraTransform.right * 0.3f; // + /*transform.up **/  ;// + new Vector3(0.3f, 1.5f, 0.7f);
        rS1.transform.localPosition = CameraTransform.position - new Vector3(0, CameraTransform.position.y, 0) + CameraTransform.forward * 0.1f + new Vector3 (0, 1.05f, 0) + CameraTransform.right * 0.3f; //new Vector3(0.3f, 1f, 0.2f);
        lS0.transform.localPosition = CameraTransform.position - new Vector3(0, CameraTransform.position.y, 0) + CameraTransform.forward * 0.3f + new Vector3 (0,1.45f,0) + CameraTransform.right * -0.3f; //new Vector3(-0.3f, 1.5f, 0.7f);
        lS1.transform.localPosition = CameraTransform.position - new Vector3(0, CameraTransform.position.y, 0) + CameraTransform.forward * 0.1f + new Vector3 (0,1.05f,0) + CameraTransform.right * -0.3f; //new Vector3(-0.3f, 1f, 0.2f);

        if (rS0Collision && lS1Collision)
        {
            if (leftFoot)
            {
                Vector3 forward = CameraTransform.forward;
                forward.y = 0;
                body.AddForce(forward * speed, ForceMode.Impulse);
                Debug.Log(this.GetType().ToString() + ": Force added");
            }
            timeCounter = 0;
            rightFoot = true;
            leftFoot = false;
            //Debug.Log("Right foot activated: " + leftFoot);
        }
        else if (rS1Collision && lS0Collision)
        {
            Debug.Log("Left foot status: " + leftFoot);
            Debug.Log("Timer status: " + timeCounter);
            if (rightFoot)
            {
                Vector3 forward = CameraTransform.forward;
                forward.y = 0;
                body.AddForce(forward * speed, ForceMode.Impulse);
                Debug.Log(this.GetType().ToString() + ": Force added");
            }
            timeCounter = 0;
            leftFoot = true;
            rightFoot = false;
            //Debug.Log("Left foot activated: " + rightFoot);
        }


        timeCounter += Time.deltaTime * 1000;
        if (timeCounter > timeOut)
        {
            timeCounter = 0;
            rightFoot = false;
            leftFoot = false;
        }
    }
    private void OnDestroy()
    {
        foreach (GameObject obj in spawnedElements)
            Destroy(obj);
    }

}
