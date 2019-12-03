using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGoose : MonoBehaviour
{
    public Camera maincam;
    public Transform goose;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        maincam.transform.position = new Vector3(goose.position.x - 11, 16, goose.position.z - 11);

        if (Input.GetKey(KeyCode.Z))
        {
            maincam.orthographicSize = (float)Mathf.Lerp(maincam.orthographicSize, 3, Time.deltaTime * 2); ;
        }

        else if (Input.GetKey(KeyCode.X))
        {
            maincam.orthographicSize = (float)Mathf.Lerp(maincam.orthographicSize, 8, Time.deltaTime * 2); ;
        }

        else
        {
            maincam.orthographicSize = (float)Mathf.Lerp(maincam.orthographicSize, 5, Time.deltaTime * 2); 
        }
    }


}
