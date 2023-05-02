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
        turnHandle = transform.GetChild(1).GetChild(0).gameObject; //Actually tracks the lever
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime * 1000;
        if (timer > sampleTime)
        {
            if (speedHandle.transform.localEulerAngles.z < 320f && speedHandle.transform.localEulerAngles.z > 310f)
            {
                transform.Translate(new Vector3(1F, 0, 0));
                //speedHandle.transform.Translate(new Vector3(0.1F, 0,0));
            }
            else if (speedHandle.transform.localEulerAngles.z > 40f && speedHandle.transform.localEulerAngles.z < 50f)
            {
                //transform.position = transform.position + new Vector3(-1F, 0, 0);
                transform.Translate(new Vector3(-1F, 0, 0));
                //speedHandle.transform.Translate(new Vector3(-0.1F, 0, 0));
            }
            timer = 0;


            if (turnAllowed == true && turnHandle.transform.localEulerAngles.z < 320f && turnHandle.transform.localEulerAngles.z > 310f) 
            {
                //transform.transform.Rotate(vector3 eulers, new Quaternion(0,0,0,0));
                transform.eulerAngles = transform.localEulerAngles + new Vector3(0, 36, 0);
                turnAllowed = false;
            }
            else if (turnAllowed == true && turnHandle.transform.localEulerAngles.z > 40f && turnHandle.transform.localEulerAngles.z < 50f)
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
