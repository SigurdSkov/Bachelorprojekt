using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class TreePlanterScript : MonoBehaviour
{
    [SerializeField]
    private GameObject tree0;
    [SerializeField]
    private GameObject tree1;
    [SerializeField]
    private GameObject tree2;
    [SerializeField]
    private GameObject tree3;
    [SerializeField]
    private GameObject tree4;
    [SerializeField]
    private GameObject tree5;
    [SerializeField]
    private GameObject tree6;
    [SerializeField]
    private GameObject tree7;
    [SerializeField]
    private GameObject tree8;
    [SerializeField]
    private GameObject tree9;
    [SerializeField]
    private GameObject tree10;
    [SerializeField]
    private GameObject tree11;
    [SerializeField]
    private GameObject tree12;
    [SerializeField]
    private GameObject tree13;
    [SerializeField]
    private GameObject tree14;
    [SerializeField]
    private GameObject tree15;
    [SerializeField]
    private GameObject tree16;
    [SerializeField]
    private GameObject tree17;
    [SerializeField]
    private GameObject tree18;
    [SerializeField]
    private GameObject tree19;
    [SerializeField]
    private GameObject tree20;
    [SerializeField]
    private GameObject tree21;
    [SerializeField]
    private GameObject tree22;
    [SerializeField]
    private GameObject tree23;
    [SerializeField]
    private GameObject tree24;
    [SerializeField]
    private GameObject tree25;
    [SerializeField]
    private GameObject tree26;
    [SerializeField]
    private GameObject tree27;
    [SerializeField]
    private GameObject tree28;
    [SerializeField]
    private GameObject tree29;
    [SerializeField]
    private GameObject tree30;
    [SerializeField]
    private GameObject tree31;
    [SerializeField]
    private GameObject tree32;
    [SerializeField]
    private GameObject tree33;
    [SerializeField]
    private GameObject tree34;
    [SerializeField]
    private GameObject tree35;
    [SerializeField]
    private GameObject tree36;
    [SerializeField]
    private GameObject tree37;
    [SerializeField]
    private GameObject tree38;
    [SerializeField]
    private GameObject tree39;
    [SerializeField]
    private GameObject tree40;
    [SerializeField]
    private GameObject tree41;
    [SerializeField]
    private GameObject tree42;
    [SerializeField]
    private GameObject tree43;
    [SerializeField]
    private GameObject tree44;
    [SerializeField]
    private GameObject tree45;

    private List<GameObject> listOfTrees;
    private List<GameObject> listOfInstantiatedTrees;

    public GameObject treePrefab;
    public int numTrees = 1000;
    public float radius = 100f;
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> listOfTrees = new List<GameObject>();
        List<GameObject> listOfInstantiatedTrees = new List<GameObject>();


        System.Type type = typeof(TreePlanterScript); // Replace YourClass with the name of your class
        FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (FieldInfo field in fields)
        {
            if (field.FieldType == typeof(GameObject))
            {
                GameObject gameObject = (GameObject)field.GetValue(this);
                listOfTrees.Add(gameObject);
            }
        }
        float mapSize = 2000f; // 2 square kilometers
        int rows = Mathf.FloorToInt(mapSize / (2f * radius)); // Number of rows
        int cols = Mathf.FloorToInt(mapSize / (2f * radius)); // Number of columns
        float startX = -mapSize / 2f + radius; // Starting X position
        float startZ = -mapSize / 2f + radius; // Starting Z position
        int row, col;

        for (row = 0; row < rows; row++)
        {
            for (col = 0; col < cols; col++)
            {
                if (!((col == 4 && row == 4) || (col == 4 && row == 5) || (col == 5 && row == 4) || (col == 5 && row == 5)))
                {
                    //Debug.Log("Column: " + col + "   " + "Row: " + row);
                    Vector3 centerPos = new Vector3(startX + col * 2f * radius, 0f, startZ + row * 2f * radius);
                    for (int i = 0; i < numTrees; i++)
                    {
                        Vector3 randomPos = centerPos + Random.insideUnitSphere * radius;
                        randomPos.y = 0f;

                        // Check if the position is clear
                        Collider[] colliders = Physics.OverlapSphere(randomPos, 10f, layerMask);
                        if (colliders.Length == 0)
                        {
                            // Spawn the tree
                            listOfInstantiatedTrees.Add(Instantiate(listOfTrees[Random.Range(0, listOfTrees.Count)], randomPos, Quaternion.identity));
                        }
                        else
                        {
                            i--; // Try again
                        }
                    }
                }
            }
        }
        foreach (GameObject instantiatedTree in listOfInstantiatedTrees)
        {
            instantiatedTree.GetComponent<Collider>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
