using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AddWagonPositionSet : MonoBehaviour
{
    private GameObject prefabWagon;
    private GameObject instantiatedWagon;

    // Start is called before the first frame update
    void Start()
    {
        prefabWagon = Resources.Load<GameObject>("Prefabs/W1_wagon_position");
        instantiatedWagon = Instantiate(prefabWagon, gameObject.transform.position - new Vector3(0,0.3F,0), transform.rotation);
        transform.parent = instantiatedWagon.transform;
        //instantiatedWagon.transform.parent = transform;
        instantiatedWagon.AddComponent<positionSet>();
        Debug.Log("Made wagon");
    }

    void OnDestroy()
    {
        foreach (Transform child in instantiatedWagon.transform)
        {
            if (child.GetComponent<AddWagonPositionSet>() != null)
            {
                Debug.Log(child + "    " + child.transform.parent);
                child.transform.parent = null;
            }
        }
        Destroy(instantiatedWagon);
        Debug.Log("Unmade wagon");
    }
}
