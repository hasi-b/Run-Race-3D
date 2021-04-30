using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{

    public bool finish,failed,start;
    private InGame ig;

    public static GameManager instance;
    private GameObject[] runner;
    List<RankingSystem> arrayToBeSorted = new List<RankingSystem>();
    List<RankingSystem> sortingRank = new List<RankingSystem>();

    public int pass;

    public string firstPlace, SecondPlace, thirdPlace;


    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;

        runner = GameObject.FindGameObjectsWithTag("Runner");

        ig = FindObjectOfType<InGame>();
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

        RealTimeLeaderboard();





    }


    void RealTimeLeaderboard()
    {
        sortingRank = arrayToBeSorted;
        sortingRank = sortingRank.OrderBy(x => x.rank).ToList();

        ig.a = sortingRank[0].name;
        ig.b = sortingRank[1].name;
        ig.c = sortingRank[2].name;
    }


    void CalculatingRank()
    {
        arrayToBeSorted = arrayToBeSorted.OrderBy( x => x.counter).ToList();

      /* switch (arrayToBeSorted.Count)
        {
            case 3:
                arrayToBeSorted[0].rank = 3;
                arrayToBeSorted[1].rank = 2;
                arrayToBeSorted[2].rank = 1;
                break;
            case 2:
                arrayToBeSorted[0].rank = 3;
                arrayToBeSorted[1].rank = 2;
                break;            
            case 1:
                arrayToBeSorted[0].rank = 1;
                if(firstPlace == "")
                {
                    firstPlace = arrayToBeSorted[0].name;
                }
                break;

        } 

       /* arrayToBeSorted[0].rank = 3;
        arrayToBeSorted[0].rank = 2;
        arrayToBeSorted[0].rank = 1;
        */

         for(int i=0,j=runner.Length;i< runner.Length; i++)
        {
            arrayToBeSorted[i].rank = j;
            j--;

            if(arrayToBeSorted[0].rank == 1 && firstPlace == "")
            {
                firstPlace = arrayToBeSorted[0].name;
                GameUI.instance.OpenLB();
            }




        } 
         





         

         if(pass == runner.Length )
        {
            pass = 0;
            arrayToBeSorted = arrayToBeSorted.OrderBy(x => x.counter).ToList();

            foreach(RankingSystem rs in arrayToBeSorted)
            {
                if(rs.rank == arrayToBeSorted.Count)
                {

                    rs.gameObject.SetActive(false);

                    Debug.Log("Number of player remaining: "+ arrayToBeSorted.Count);

                    
                    if (rs.gameObject.name == "Player")
                    {
                        GameUI.instance.OpenLB();
                        failed = true;
                    }

                    if (thirdPlace == "")
                    {
                        thirdPlace = rs.gameObject.name;
                        ig.thirdplaceImg.color = Color.red;
                    }
                    else if(SecondPlace == "")
                    {
                        SecondPlace = rs.gameObject.name;
                        ig.SecondPlaceImg.color = Color.red;
                    }
                    



                }
            }
            
            runner = GameObject.FindGameObjectsWithTag("Runner");
            arrayToBeSorted.Clear();

            for (int i = 0; i < runner.Length; i++)
            {
                arrayToBeSorted.Add(runner[i].GetComponent<RankingSystem>());

            }
            Debug.Log("Runner Length: " + runner.Length);
            Debug.Log("ArrayCount: " + arrayToBeSorted.Count);


           if(runner.Length < 2)
            {
                finish = true;
            }
            

        } 


    }
}
