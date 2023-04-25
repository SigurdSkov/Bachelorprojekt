using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class GetUp : MonoBehaviour
{
    GameObject rightCube, leftCube;
    Transform rightHandControllerTransform, leftHandControllerTransform;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().constraints = 0;
        //Instantiate two boxes
        rightCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        leftCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        rightHandControllerTransform = transform.GetChild(0).GetChild(0).GetChild(2).transform;
        rightHandControllerTransform = transform.GetChild(0).GetChild(0).GetChild(1).transform;
        rightCube.transform.SetParent(rightHandControllerTransform);
        leftCube.transform.SetParent(rightHandControllerTransform);
        rightCube.transform.position = Vector3.zero;
        leftCube.transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        Destroy(leftCube);
        Destroy(rightCube);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}
