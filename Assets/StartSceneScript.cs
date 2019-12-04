using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneScript : MonoBehaviour
{
    public Button[] buttons;
    public GameObject[] dots;
    public Text[] texts;
    public bool button0; public bool button1;
    public bool thetitle; public bool thebuttons;
    public GameObject title;
    // Start is called before the first frame update
    void Start()
    {
        if (button0)
        {
            buttons[0].onClick.AddListener(StartGame);
        }

        if(button1)
        {
            buttons[1].onClick.AddListener(QuitGame);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void StartGame()
    {
        Debug.Log("Game Starts");
        SceneManager.LoadScene("Deeemo");

    }
    void QuitGame()
    {
        Debug.Log("Game Quits");
        Application.Quit();
    }

    public void OnMouseOver()
    {
        if(button0)
        {
            dots[0].GetComponent<SpriteRenderer>().enabled = true;
           
           
            
        }

        if(button1)
        {
            dots[1].GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void OnMouseExit()
    {
        if (button0)
        {
            
            
            dots[0].GetComponent<SpriteRenderer>().enabled = false;
            
        }

        if (button1)
        {
            dots[1].GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
