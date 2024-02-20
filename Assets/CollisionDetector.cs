
using UnityEngine;
using System.Collections.Generic; // Import the namespace for dictionaries

public class CollisionDetector : MonoBehaviour
{
    public GameObject[] wall;
    Dictionary<string, GameObject> mainWall = new Dictionary<string, GameObject>();
    Dictionary<string, bool> completed = new Dictionary<string, bool>();

    void Start()
    {
        mainWall["Red"] = wall[0];
        mainWall["Green"] = wall[1];
        mainWall["Blue"] = wall[2];

        completed["Red"] = false;
        completed["Green"] = false;
        completed["Blue"] = false;
    }

    public bool getCompleted()
    {
        return completed[gameObject.tag];
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(gameObject.tag))
        {
            Destroy(mainWall[gameObject.tag]);
            Destroy(other.gameObject);
            completed[gameObject.tag] = true;
        }
    }

}
