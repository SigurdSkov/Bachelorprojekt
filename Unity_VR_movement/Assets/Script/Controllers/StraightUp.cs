using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightUp : MonoBehaviour
{
    Transform CameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        CameraTransform = gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = CameraTransform.up;
        direction.x = 0;
        direction.z = 0;
        transform.Translate(direction);
    }
}
