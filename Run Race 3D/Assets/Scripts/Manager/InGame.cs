using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InGame : MonoBehaviour
{
    public Image thirdplaceImg, SecondPlaceImg;
    public TextMeshProUGUI[] namesText;
    public string a, b, c;

    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        namesText[0].text = a;
        namesText[1].text = b;
        namesText[2].text = c;
    }
}
