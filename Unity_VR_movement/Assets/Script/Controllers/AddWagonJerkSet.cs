using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AddWagonJerkSet : MonoBehaviour
{
    private GameObject prefabWagon;
    private GameObject instantiatedWagon;

    // Start is called before the first frame update
    void Start()
    {
        prefabWagon = Resources.Load<GameObject>("Prefabs/W1_wagon_position");
        instantiatedWagon = Instantiate(prefabWagon, gameObject.transform.position - new Vector3(0, 0.3F, 0), transform.rotation);
        transform.parent = instantiatedWagon.transform;
        instantiatedWagon.AddComponent<jerkSet>();
        transform.AddComponent<FixedJoint>(); //untested
        transform.GetComponent<FixedJoint>().connectedBody = instantiatedWagon.GetComponent<Rigidbody>(); //untested
        Debug.Log("Made wagon");
    }

    private void Update()
    {
        transform.position = instantiatedWagon.transform.position + new Vector3(0, 0.5f, 0);
    }

    void OnDestroy()
    {
        foreach (Transform child in instantiatedWagon.transform)
        {
            if (child.GetComponent<AddWagonJerkSet>() != null)
            {
                Debug.Log(child + "    " + child.transform.parent);
                child.transform.parent = null;
            }
        }
        Destroy(instantiatedWagon);
        Debug.Log("Unmade wagon");
    }
}
