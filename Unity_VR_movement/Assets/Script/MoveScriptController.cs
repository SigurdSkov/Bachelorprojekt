using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MoveScriptSpace
{
    public class MoveScriptController : MonoBehaviour
    {
        //[SerializeField] Didn't work with monobehaviour
        //List<Component> moveScripts = new List<Component>();
        MonoScript[] moveScripts;
        // Start is called before the first frame update
        public MonoScript[] getMoveScripts() { return moveScripts; }
        void Start()
        {
            moveScripts = Resources.LoadAll<MonoScript>("Assets/Scripts/Controllers");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
