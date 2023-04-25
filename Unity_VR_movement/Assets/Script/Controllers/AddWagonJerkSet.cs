using System.Collections;
using System.Collections.Generic;
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
        instantiatedWagon.AddComponent<AddWagonJerkSet>();
        Debug.Log("Made wagon");
    }

    void OnDestroy()
    {
        Destroy(instantiatedWagon);
        Debug.Log("Unmade wagon");
    }
}
