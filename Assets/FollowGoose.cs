using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGoose : MonoBehaviour
{
    public Camera maincam;
    public Transform goose;
    private float lerp;
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
            DoLerp();
            maincam.orthographicSize = (float)Mathf.Lerp(maincam.orthographicSize, 3, lerp); ;
        }

        else if (Input.GetKey(KeyCode.X))
        {
            DoLerp();
            maincam.orthographicSize = (float)Mathf.Lerp(maincam.orthographicSize, 8, lerp); ;
        }

        else
        {
            DoLerp();
            maincam.orthographicSize = (float)Mathf.Lerp(maincam.orthographicSize, 5, lerp); ;
        }
    }

    public void DoLerp()
    {
        lerp += Time.deltaTime * 2;
    }
}
