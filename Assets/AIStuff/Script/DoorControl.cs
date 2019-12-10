using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorControl : MonoBehaviour {
    [HideInInspector]
    public static DoorControl Main;
    public NavMeshObstacle Obstacle;
    public Collider RayCastCol;
    public Animator Anim;
    public AIWork OpenWork;
    public LayerMask DoorRayCastLayer;

    public void Awake()
    {
        Main = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        RayCastCol.enabled = false;
        Obstacle.enabled = true;
        Anim.SetBool("Play", true);
    }
}
