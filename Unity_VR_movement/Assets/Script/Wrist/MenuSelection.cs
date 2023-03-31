using Microsoft.Unity.VisualStudio.Editor;
using MoveScriptSpace;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;
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

    //MonoScript[] MoveScriptsRef;// = transform.parent.parent.parent.parent.parent.parent.GetComponent<MoveScriptController>().getMoveScripts
    GameObject Character;
    MonoScript activeScript;

    void Start()
    {
        ButtonList = new List<GameObject>
        {
            this.transform.parent.gameObject
        };
        BorderRef = this.transform.parent.parent.gameObject.GetComponent<UnityEngine.UI.Image>();

        Character = transform.parent.parent.parent.parent.parent.parent.GameObject(); //Har nok ændret sig
        //ActivateSquatWalk();
    }

    // Update is called once per frame
    void Update()
    {
        //rotation = transform.parent.parent.parent.parent.localEulerAngles.z;
        ////rotation = transform.parent.parent.parent.parent.rotation.z;
        //// Set the transparency of the object based on its rotation
        //if (rotation > 150 && rotation < 300)
        //{
        //    BorderRef.color = new UnityEngine.Color(1, 1, 1, (float)Math.Cos(rotation/225));
            
        //    //This function linearly maps the input angle from 150 to 225 to the output gradient from 0 to 1, and from 225 to 300 to the output gradient from 1 to 0. The max and min functions ensure that the output value is clamped between 0 and 1, so that the function always returns a valid gradient value.
        //    //-Courtesy of ChatGPT
        //}
        //else { BorderRef.color = BorderColorInActive; }
    }

    //[SerializeField]
    //private MonoScript JumpingScript;

    public void HandleInputData(int inputData)
    {
        switch (inputData)
        {
            case 0:
                Debug.Log("SquatWalk");/*ActivateSquatWalk();*/ break;
            case 1:
                Debug.Log("SquatJump");/*ActivateJump();*/ break;
            case 2:
                Debug.Log("SquatStrafe"); break;
            case 3:
                Debug.Log("SquatRetreat"); break;
            case 4:
                Debug.Log("SquatJumpStraightUp"); break;
            case 5:
                Debug.Log("Implement SquatJumpSquat"); break; //head bob and slide
            case 6:
                Debug.Log("Implement JumpMove"); break;
            case 7:
                Debug.Log("Implement JumpMoveDiagonal"); break;
            case 8:
                Debug.Log("Implement JumpMoveStrafe"); break;
            case 9:
                Debug.Log("Implement JumpMoveBack"); break;
            case 10:
                Debug.Log("Implement JumpJumpForward"); break;
            case 11:
                Debug.Log("Implement JumpJumpStraightUp"); break;
            case 12:
                Debug.Log("Implement JumpJumpDiagonal"); break;
            default:
                break;
        }
    }

    public void ActivateSquatWalk()
    {
        Destroy(Character.GetComponent<MonoScript>());
        Character.AddComponent<Squatjump>();
    }

    public void ActivateJump()
    {
        //MonoScript JumpingScript;// = transform.parent.parent.parent.parent.parent.parent.GetComponent<Squatjump>();

        //foreach (MonoScript script in MoveScriptsRef)
        //{
        //    if (script == GetComponent<Squatjump>()) { JumpingScript = script; }
        //    Destroy(script);
        //}

        //Destroys some script... How do I know which one?
        Destroy(Character.GetComponent<MonoScript>()); 
        Character.AddComponent<Squatjump>();
    }
}
