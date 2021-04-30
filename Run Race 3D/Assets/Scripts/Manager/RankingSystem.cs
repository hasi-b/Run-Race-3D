using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingSystem : MonoBehaviour
{
    // Start is called before the first frame update

    public int currentCheckpoint=1, lapNumber;

    private Vector3 checkpoint;
    public int rank;
    public float distance,counter;
    


    void Start()
    {
        currentCheckpoint = 1;
        checkpoint = GameObject.Find("CheckPoint" + currentCheckpoint).transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateDistance();

        print( gameObject.name+" " + rank);





    }

    void CalculateDistance()
    {
        distance = Vector3.Distance(transform.position,checkpoint);
        counter = lapNumber * 1000 + currentCheckpoint * 100 + distance;

    }

    private void OnTriggerEnter(Collider target)
    {
        if(target.tag == "CheckPoint")
        {
            Debug.Log("CCCCCCCCHECK");
            //currentCheckpoint = GameObject.Find(target.gameObject.name).GetComponent<CurrentCheckpoint>().currentCheckNumber;
            currentCheckpoint = target.GetComponent<CurrentCheckpoint>().currentCheckNumber;
            checkpoint = GameObject.Find("CheckPoint" + currentCheckpoint).transform.position;
        }

        if(target.tag == "Finish")
        {
            lapNumber++;
            GameManager.instance.pass++;
        }
    }
}
