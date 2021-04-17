using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector]
    public GameObject[] checkPoints;

    public int currentCheckPoint = 1;



    void Awake()
    {
        checkPoints = GameObject.FindGameObjectsWithTag("CheckPoint");
        currentCheckPoint = 1;
    }

    // Update is called once per frame
    private void Start()
    {
        foreach(GameObject cp in checkPoints)
        {
            cp.AddComponent<CurrentCheckpoint>();
            cp.GetComponent<CurrentCheckpoint>().currentCheckNumber = currentCheckPoint;
            cp.name = "CheckPoint" + currentCheckPoint;
            currentCheckPoint++;


        }
    }
    void Update()
    {
        
    }
}
