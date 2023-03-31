using Microsoft.Unity.VisualStudio.Editor;
using MoveScriptSpace;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
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
    // Start is called before the first frame update
    //List<UnityEngine.Component> scriptList;
    List<MonoScript> scripts = new List<MonoScript>();

    //MonoScript[] MoveScriptsRef;// = transform.parent.parent.parent.parent.parent.parent.GetComponent<MoveScriptController>().getMoveScripts
    GameObject Character;
    MonoScript activeScript;

    [SerializeField]
    string folderPath = "Script/Controllers/";

    void Start()
    {
        //DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
        //FileInfo[] fileInfo = directoryInfo.GetFiles("*.cs");

        foreach (string filePath in Directory.GetFiles(folderPath, "*.cs"))
        {
            MonoScript script = AssetDatabase.LoadAssetAtPath<MonoScript>(filePath);
            if (script != null)
            {
                //Debug.Log(script.name);
                scripts.Add(script);
                //UnityEngine.Component scriptComponent = GetComponent(script.name);
                //components.Add(scriptComponent);
            }
            else
            {
                Debug.LogWarning("Could not load script asset: " + filePath);
            }
        }

        Character = transform.parent.parent.parent.parent.parent.GameObject(); //Har nok ændret sig
        //ActivateSquatWalk();
        //Debug.Log(Character);
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
                ActivateSquatWalk(); break;//Debug.Log("SquatWalk");/*ActivateSquatWalk();*/ break;
            case 1:
                Debug.Log("SquatStrafe"); break;
            case 2:
                Debug.Log("SquatRetreat"); break;
            case 3:
                ActivateJump(); break; //Debug.Log("SquatJump");/**/ break;
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
        Debug.Log("SquatWalk");
        foreach (UnityEngine.Component component in Character.GetComponents<UnityEngine.Component>())
        {
            Debug.Log(component.GetType().Name);
            foreach (MonoScript script in scripts)
            {
                if (script.name == component.GetType().Name)
                { /*Debug.Log("Will remove: " + component);*/ 
                    Destroy(component); 
                    break; 
                }
            }
        }
        Character.AddComponent<Squatmove>();
    }

    public void ActivateJump()
    {
        Destroy(Character.GetComponent<Squatmove>());
        Character.AddComponent<Squatjump>();
    }

    //En for hvert script... Derefter test scripts... Derefter lav nogle flere scripts
}
