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
    string relativePath = "/Script/Controllers/";

    string folderPath = "";

    void Start()
    {
        //DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
        //FileInfo[] fileInfo = directoryInfo.GetFiles("*.cs");
        folderPath = Application.dataPath + relativePath;
        //Debug.Log(folderPath);
        foreach (string filePath in Directory.GetFiles(folderPath, "*.cs"))
        {
            //Debug.Log(filePath);
            int AssetsIndex = filePath.IndexOf("Assets");
            string relativeFilePath = filePath.Substring(AssetsIndex);
            MonoScript script = AssetDatabase.LoadAssetAtPath<MonoScript>(relativeFilePath);
            if (script != null)
            {
                //Debug.Log(script.name);
                scripts.Add(script);
                //UnityEngine.Component scriptComponent = GetComponent(script.name);
                //components.Add(scriptComponent);
            }
            else
            {
                Debug.LogWarning("Could not load script asset: " + relativeFilePath);
            }
        }

        Character = transform.parent.parent.parent.parent.parent.GameObject(); //Har nok �ndret sig
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void HandleInputData(int inputData)
    {
        RemoveActiveScript();
        switch (inputData)//Inds�t parametre, serialize dem. S�ledes kan speed s�ttes lidt mere interessant. Fremtiden ville byde disse f�r en separat menu... Men det er der ikke tid til
        {
            case 0:
                ActivateSquatWalk(); break;//Added more speed
            case 1:
                ActivateSquatMoveDiagonal(); break; //Doesn't work //Works now
            case 2:
                ActivateSquatStrafe(); break; //Doesn't work //Works now
            case 3:
                ActivateSquatRetreat(); break;
            case 4:
                ActivateSquatJump(); break; //Debug.Log("SquatJump");/**/ break;
            case 5:
                ActivateSquatJumpUp(); break; //Doesn't work //Works now
            case 6:
                ActivateSquatJumpSlide(); break; //head bob and slide - Mere slide, mindre masse & drag //Works now
            case 7:
                ActivateSquatJumpHeadbob(); break; //Doesn't work //Works now
            case 8:
                ActivateJumpMoveForward(); break;
            case 9:
                ActivateJumpMoveDiagonal(); break; //Diagonal virker ikke, man bev�ger sig i y-aksen //Works now
            case 10:
                ActivateJumpMoveStrafe(); break; //Doesn't work //Works now
            case 11:
                ActivateJumpMoveBack(); break;
            case 12:
                ActivateJumpJumpForward(); break;
            case 13:
                ActivateJumpJumpStraightUp(); break;
            case 14:
                ActivateJumpJumpDiagonal(); break; //Hopper, men ikke diagonalt
            default:
                break;
        }
    }

    public void RemoveActiveScript()
    {
        foreach (UnityEngine.Component component in Character.GetComponents<UnityEngine.Component>())
        {
            //Debug.Log(component.GetType().Name);
            foreach (MonoScript script in scripts)
            {
                if (script.name == component.GetType().Name)
                { 
                    //Debug.Log("Will remove: " + component);
                    Destroy(component);
                    break;
                }
            }
        }
    }

    //0
    public void ActivateSquatWalk()
    {
        Debug.Log("SquatWalk");
        Character.AddComponent<Squatmove>();
    }

    //1
    public void ActivateSquatMoveDiagonal()
    {
        Debug.Log("SquatDiagonal");
        Character.AddComponent<Squatmovediagonal>();
    }
    
    //2
    public void ActivateSquatStrafe() 
    {
        Debug.Log("SquatStrafe");
        Character.AddComponent<SquatmoveStrafe>();
    }

    //3
    public void ActivateSquatRetreat()
    {
        Debug.Log("SquatRetreat");
        Character.AddComponent<SquatmoveBack>();
    }

    //4
    public void ActivateSquatJump()
    {
        Debug.Log("ActivateJump");
        Character.AddComponent<Squatjump>();
    }

    //5
    public void ActivateSquatJumpUp()
    {
        Debug.Log("ActivateJumpStraightUp");
        Character.AddComponent<SquatjumpUp>();
    }

    //6
    public void ActivateSquatJumpSlide()
    {
        Debug.Log("ActivateJumpSlide");
        Character.AddComponent<Squatjumpslide>();
        //slide (less friction)
        //Retain highest high
    }

    //7
    public void ActivateSquatJumpHeadbob()
    {
        Debug.Log("ActivateSquatJumpHeadbob");
        Character.AddComponent<Squatjumpheadbob>();
    }

    //8
    public void ActivateJumpMoveForward()
    {
        Debug.Log("ActivateJumpMove");
        Character.AddComponent<Jumpmove>();
    }

    //9
    public void ActivateJumpMoveDiagonal()
    {
        Debug.Log("ActivateJumpDiagonal");
        Character.AddComponent<Jumpmovediag>();
    }

    //10
    public void ActivateJumpMoveStrafe()
    {
        Debug.Log("ActivateJumpStrafe");
        Character.AddComponent<Jumpmovestrafe>();
    }

    //11
    public void ActivateJumpMoveBack()
    {
        Debug.Log("ActivateJumpMoveBack");
        Character.AddComponent<Jumpmoveback>();
    }

    //12
    public void ActivateJumpJumpForward()
    {
        Debug.Log("ActivateJumpJumpForward");
        Character.AddComponent<Jumpjump>();
    }

    //13
    public void ActivateJumpJumpStraightUp()
    {
        Debug.Log("ActivateJumpJumpStraightUp");
        Character.AddComponent<Jumpjumpup>();
    }

    //14
    public void ActivateJumpJumpDiagonal()
    {
        Debug.Log("ActivateJumpJumpDiagonal");
        Character.AddComponent<Jumpjumpdiagonal>();
    }

    //En for hvert script... Derefter test scripts... Derefter lav nogle flere scripts
}
