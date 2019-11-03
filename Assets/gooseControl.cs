using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gooseControl : MonoBehaviour
{
    public Camera cam;
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    private float defaultY = .5f;
    public float movespeed;
    public GameObject neck;

    private Vector3 targetPos;
    public Transform target;
    private Transform neckU;
    private Transform neckD;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = transform.position;
        Cursor.visible = true;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        //neckD.rotation = Quaternion.Euler(110, 0, 0);
        //neckU.rotation = Quaternion.Euler(30, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float rcDist = 1000f;
        Debug.DrawRay(mouseRay.origin, mouseRay.direction * rcDist, Color.red);
        RaycastHit mouseHit = new RaycastHit();

        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(mouseRay, out mouseHit, rcDist))
            {
                targetPos = mouseHit.point;
                targetPos.y = defaultY;
                
                //Vector3 mDir = mouseHit.point - transform.position;
                //Quaternion mRot = Quaternion.LookRotation(mDir);
                //transform.rotation = Quaternion.Slerp(transform.rotation, mRot, 2 * Time.deltaTime);
            }

            if ((Vector3)transform.position != targetPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, movespeed  * Time.deltaTime);
            }

        }


        if (Input.GetKey(KeyCode.LeftShift))
        {
            movespeed = 7;
        }
        else
        {
            movespeed = 4;
        }


        if (Input.GetKey(KeyCode.Q))
        {
            Debug.Log("PRESSING");
            neck.transform.rotation = Quaternion.Lerp(neck.transform.rotation, Quaternion.Euler(110, 0, 0), Time.deltaTime * 3);
        }
        else
        {
            neck.transform.rotation = Quaternion.Lerp(neck.transform.rotation, Quaternion.Euler(30, 0, 0), Time.deltaTime * 3);
        }

    }
}
