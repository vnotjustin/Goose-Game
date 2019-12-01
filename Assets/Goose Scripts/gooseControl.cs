using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gooseControl : MonoBehaviour
{
    public static GameObject goose;
    public Camera cam;
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    private float defaultY = .5f;
    public float movespeed;
    public GameObject neck;
    Animator m_Animator;
    private Vector3 targetPos;
    public Transform target;
    public LayerMask ground;
    public static bool isBend;

    // Start is called before the first frame update
    void Start()
    {
        goose = this.gameObject;
        targetPos = transform.position;
        Cursor.visible = true;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        m_Animator = GetComponentInChildren<Animator>();
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
                if (Physics.Raycast(mouseRay, out mouseHit, rcDist, ground))
            {
                targetPos = mouseHit.point;
                targetPos.y = defaultY;
                
                Vector3 mDir = mouseHit.point - transform.position;
                Quaternion mRot = Quaternion.LookRotation(mDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, mRot, 4 * Time.deltaTime);
            }

            if ((Vector3)transform.position != targetPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, movespeed  * Time.deltaTime);
                m_Animator.SetBool("isWalking", true);
            }

        }
        else
        {
            m_Animator.SetBool("isWalking", false);
        }



        if (Input.GetKey(KeyCode.LeftShift))
        {
            movespeed = 7;
            m_Animator.SetBool("isSprint", true);
        }
        else
        {
            movespeed = 4;
            m_Animator.SetBool("isSprint", false);
        }


        if (Input.GetKey(KeyCode.Q))
        {
            m_Animator.SetBool("isBend", true);
            isBend = true;
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            m_Animator.SetBool("isBend", false);
            isBend = false;
        }

    }




}
