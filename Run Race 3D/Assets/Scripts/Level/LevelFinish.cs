using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelFinish : MonoBehaviour
{
    public TextMeshProUGUI[] rankText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rankText[0].text = GameManager.instance.firstPlace;
        rankText[1].text = GameManager.instance.SecondPlace;
        rankText[2].text = GameManager.instance.thirdPlace;
    }
}
