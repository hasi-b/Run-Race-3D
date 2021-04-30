using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUI : MonoBehaviour
{
    // Start is called before the first frame update

    public static GameUI instance;
    public GameObject inGame, leaderboard;
    private Button nextlevel;

    public TextMeshProUGUI countText;

    private void Awake()
    {
        instance = this;
       
        countText.gameObject.SetActive(true);
        StartCoroutine(StartGame());
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.failed)
        {
            if (leaderboard.activeInHierarchy)
            {
                GameManager.instance.failed = false;
                Restart();
            }
        }
    }

     IEnumerator StartGame()
    {
        
        countText.text = 3.ToString();
        countText.color = Color.red;
        yield return new WaitForSeconds(1);
        countText.text = 2.ToString();
        countText.color = Color.yellow;
        yield return new WaitForSeconds(1);
        countText.text = 1.ToString();
        countText.color = Color.green;
        yield return new WaitForSeconds(1);

        countText.text = "GO";
        yield return new WaitForSeconds(.5f);

        GameManager.instance.start = true;
        countText.gameObject.SetActive(false);
        
        
    }





    public void OpenLB()
    {
        inGame.SetActive(false);
        leaderboard.SetActive(true);
    }


    private void Restart()
    {
        nextlevel = GameObject.Find("/GameUI/DashBoard/next").GetComponent<Button>();
        nextlevel.onClick.RemoveAllListeners();
        nextlevel.onClick.AddListener(() => Reload());
        nextlevel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Again";

    }


    private void Reload()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Nextlevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Exit()
    {
        
        SceneManager.LoadSceneAsync(0);
    }

}
