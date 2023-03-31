using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MenuVisibility : MonoBehaviour
{
    // Start is called before the first frame update
    float rotation = 0;
    Transform controllerRotation;

    //UnityEngine.UI.Image BorderRef;

    //UnityEngine.Color BorderColorActive = new UnityEngine.Color(1, 1, 1, 1);
    UnityEngine.Color BorderColorInActive = new UnityEngine.Color(1, 1, 1, 0);

    bool active = false;

    void Start()
    {
        controllerRotation = transform.parent;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        rotation = controllerRotation.localEulerAngles.z;//transform.parent.parent.parent.parent.localEulerAngles.z;
        //Debug.Log(transform.localEulerAngles.z); Altid 0?
        //rotation = transform.parent.parent.parent.parent.rotation.z;
        // Set the transparency of the object based on its rotation
        if ((rotation > 130 && rotation < 280)) //|| rotation > 0 && rotation < 90)
        {
            if (!active)
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
            active = true;

            //gameObject.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new UnityEngine.Color(1, 1, 1, (float)Math.Cos(rotation / 130));
            //gameObject.transform.GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new UnityEngine.Color(1, 1, 1, (float)Math.Cos(rotation / 130));

            //            gameObject.GetComponent<UnityEngine.UI.Image>.color = //new UnityEngine.Color(1, 1, 1, (float)Math.Cos(rotation / 225));

            //This function linearly maps the input angle from 150 to 225 to the output gradient from 0 to 1, and from 225 to 300 to the output gradient from 1 to 0. The max and min functions ensure that the output value is clamped between 0 and 1, so that the function always returns a valid gradient value.
            //-Courtesy of ChatGPT
        }
        else 
        {
            if (active)
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
            active = false;
        }
    }
}
