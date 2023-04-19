using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLocomotion : MonoBehaviour
{
    private GameObject locomotionSystem;

    // Start is called before the first frame update
    void Start()
    {
        locomotionSystem = gameObject.transform.GetChild(0).GetChild(1).gameObject;
        locomotionSystem.SetActive(true);
    }
    private void OnDestroy()
    {
        locomotionSystem.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
