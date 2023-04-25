using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEditor;
using UnityEngine;

public class positionSet : MonoBehaviour
{
    GameObject speedHandle;
    GameObject turnHandle;
    float timer = 0;
    float sampleTime = 250;
    float turnTimer;
    float turnTimerSampleTime = 1000;
    bool turnAllowed = true;
    // Start is called before the first frame update
    void Start()
    {
        speedHandle = transform.GetChild(0).GetChild(0).gameObject;
        turnHandle = transform.GetChild(0).GetChild(4).gameObject; //Actually tracks the lever
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime * 1000;
        if (timer > sampleTime)
        {
            if (speedHandle.transform.localPosition.x > 0.1f)
            {
                transform.Translate(new Vector3(1F, 0, 0));
                speedHandle.transform.Translate(new Vector3(0.1F, 0,0));
            }
            else if (speedHandle.transform.localPosition.x < -0.1f)
            {
                transform.position = transform.position + new Vector3(-1F, 0, 0);
                speedHandle.transform.Translate(new Vector3(-0.1F, 0, 0));
            }
            timer = 0;


            if (turnHandle.transform.localPosition.x > 0.1f && turnAllowed == true) 
            {
                //transform.transform.Rotate(vector3 eulers, new Quaternion(0,0,0,0));
                transform.eulerAngles = transform.localEulerAngles + new Vector3(0, 36, 0);
                turnAllowed = false;
            }
            else if (turnHandle.transform.localPosition.x < -0.1f && turnAllowed == true)
            {
                transform.eulerAngles = transform.localEulerAngles + new Vector3(0, -36, 0);
                turnAllowed = false;
            }
        }
        if (turnAllowed == false)
        {
            turnTimer += Time.deltaTime * 1000;
            if (turnTimer > turnTimerSampleTime)
            {
                turnTimer = 0;
                turnAllowed = true;
            }
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
