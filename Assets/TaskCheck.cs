using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TaskCheck : MonoBehaviour
{
    public bool getin = false;
    public bool gkwet = false;
    public bool stkey = false;
    public bool wehat = false;
    public bool ralake = false;
    public bool picnic = false;
 

   // when calling any of these booleans in your script use tc.(bool) when calling them. Example if(tc.getin == true).

    void Start()
    {

    }

    void Update()
    {
        if(getin && gkwet && stkey && wehat && ralake & picnic)
        {
            SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
        }
    }
}
