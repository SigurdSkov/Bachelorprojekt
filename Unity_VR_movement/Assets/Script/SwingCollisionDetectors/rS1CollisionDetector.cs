using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rS1CollisionDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "controller")
            other.transform.parent.parent.parent.parent.GetComponent<Armswing>().rS1Collision = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "controller")
            other.transform.parent.parent.parent.parent.GetComponent<Armswing>().rS1Collision = false;
    }
}
