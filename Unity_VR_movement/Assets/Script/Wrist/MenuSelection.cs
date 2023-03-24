using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class MenuSelection : MonoBehaviour
{
    GameObject Border;
    List<GameObject> ButtonList;
    // Start is called before the first frame update
    float rotation = 0;
    float FadeFloat = 0;

    UnityEngine.UI.Image BorderRef;

    UnityEngine.Color BorderColorActive = new UnityEngine.Color(1,1,1,1);
    UnityEngine.Color BorderColorInActive = new UnityEngine.Color(1, 1, 1, 0);

    void Start()
    {
        ButtonList = new List<GameObject>
        {
            this.transform.parent.gameObject
        };
        BorderRef = this.transform.parent.parent.gameObject.GetComponent<UnityEngine.UI.Image>();
    }

    // Update is called once per frame
    void Update()
    {
        rotation = transform.parent.parent.parent.parent.localEulerAngles.z;
        //rotation = transform.parent.parent.parent.parent.rotation.z;
        // Set the transparency of the object based on its rotation
        if (rotation > 150 && rotation < 300)
        {
            BorderRef.color = new UnityEngine.Color(1, 1, 1, (float)Math.Cos(rotation/225));
            
            //This function linearly maps the input angle from 150 to 225 to the output gradient from 0 to 1, and from 225 to 300 to the output gradient from 1 to 0. The max and min functions ensure that the output value is clamped between 0 and 1, so that the function always returns a valid gradient value.
            //-Courtesy of ChatGPT
        }
        else { BorderRef.color = BorderColorInActive; }
    }

    //[SerializeField]
    //private MonoScript JumpingScript;

    public void ActivateJump()
    {
        MonoBehaviour JumpingScript = this.transform.parent.parent.parent.parent.parent.parent.GetComponent<Squatjump>();
        MonoBehaviour MovingScript = this.transform.parent.parent.parent.parent.parent.parent.GetComponent<Squatmove>();

        if ((JumpingScript).enabled == false)
        {
            (JumpingScript).enabled = true;
            MovingScript.enabled = false;
        }
        else
        {
            (JumpingScript).enabled = false;
            MovingScript.enabled = true;
        }
    }
}
