using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{

    private GameObject[] runner;
    List<RankingSystem> arrayToBeSorted = new List<RankingSystem>();
    // Start is called before the first frame update

    private void Awake()
    {
        runner = GameObject.FindGameObjectsWithTag("Runner");
    }



    void Start()
    {


        for (int i = 0; i < runner.Length; i++)
        {
            arrayToBeSorted.Add(runner[i].GetComponent<RankingSystem>());
            
        }
    }

    // Update is called once per frame
    void Update()
    {

        CalculatingRank();


    }


    void CalculatingRank()
    {
        arrayToBeSorted = arrayToBeSorted.OrderBy( x => x.counter).ToList();
       /* arrayToBeSorted[0].rank = 3;
        arrayToBeSorted[0].rank = 2;
        arrayToBeSorted[0].rank = 1;
        */

         for(int i=0,j=runner.Length;i< runner.Length; i++)
        {
            arrayToBeSorted[i].rank = j;
            j--;
        } 

    }
}
