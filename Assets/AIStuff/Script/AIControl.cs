using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{
    [HideInInspector]
    public static AIControl Main;
    public Rigidbody Rig;
    public float Speed;
    public NavMeshAgent Agent;
    public NavMeshPath Path;
    [Space]
    public GameObject Pivot;
    public GameObject RotationPivot;
    public float RotationSpeed;
    public bool RotationFinished;
    [HideInInspector]
    public bool SubRotationFinished;
    [Space]
    public PathObstacle CurrentObstacle;
    public PathObstaclePoint CurrentObstaclePoint;
    public Vector3 CurrentPointPosition;
    public float RayCastDistance = 10f;
    public LayerMask PathfindingRayLayer;
    [Space]
    public GameObject PresetTarget;
    public Vector3 MoveTarget;
    [HideInInspector]
    public Vector3 MT;
    public ActualObject CurrentObject;
    public float CurrentDelay;

    public void Awake()
    {
        Main = this;
        Path = new NavMeshPath();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (RotationPivot && !RotationFinished)
        {
            float T = PathObstacle.AbsAngle(RotationPivot.transform.eulerAngles.y);
            float O = PathObstacle.AbsAngle(Pivot.transform.eulerAngles.y);
            if (Mathf.Abs(T - O) <= RotationSpeed * Time.deltaTime
                || Mathf.Abs(T + 360 - O) <= RotationSpeed * Time.deltaTime
                || Mathf.Abs(T - O - 360) <= RotationSpeed * Time.deltaTime)
            {
                RotationFinished = true;
                Pivot.transform.eulerAngles = RotationPivot.transform.eulerAngles;
                SubRotationFinished = true;
            }
            else
            {
                float a = O;
                a += PathObstacle.RotateDirection(O, T) * RotationSpeed * Time.deltaTime;
                Vector3 b = Pivot.transform.eulerAngles;
                Pivot.transform.eulerAngles = new Vector3(b.x, a, b.z);
                SubRotationFinished = false;
            }
        }

        if (CurrentDelay > 0)
        {
            CurrentDelay -= Time.deltaTime;
            SetSpeed(new Vector3());
            return;
        }

        PathFindingUpdate_Alter();
        PathFindingMovement();
    }

    public void ReachMoveTarget()
    {
        if (CurrentObject)
        {
            SetSpeed(new Vector3());
            SetDelay(CurrentObject.PutDownDelay);
        }
    }

    public void PickUpObject(ActualObject AO)
    {
        CurrentObject = AO;
        SetMoveTarget(AO.OriPosition);
    }

    public void SetMoveTarget(Vector3 Value)
    {
        MoveTarget = new Vector3(Value.x, GetPosition().y, Value.z);
    }

    public void SetDelay(float Value)
    {
        CurrentDelay = Value;
    }

    public void Heard(string Key)
    {
        Debug.Log(Key);
        PresetTarget = gooseControl.goose;
    }

    public void PathFindingUpdate()
    {
        MT = MoveTarget;
        if (PresetTarget)
            MT = PresetTarget.transform.position;
        MT = new Vector3(MT.x, GetPosition().y, MT.z);

        float RayD = RayCastDistance;
        if (PathObstacle.GetDistance(MT, GetPosition()) < RayD)
            RayD = PathObstacle.GetDistance(MT, GetPosition());
        Ray R = new Ray(GetPosition(), MT - GetPosition());
        //Debug.DrawRay(R.origin, R.direction * RayD, Color.green);
        RaycastHit[] Hits = Physics.RaycastAll(R.origin, R.direction, RayD, PathfindingRayLayer);
        if (Hits.Length > 0)
        {
            GameObject H = Hits[0].transform.gameObject;
            if (H.GetComponent<PathObstacleCollider>() && H.GetComponent<PathObstacleCollider>().Obstacle != CurrentObstacle)
                SetObstacle(H.GetComponent<PathObstacleCollider>().Obstacle, Hits[0].point, MT);
        }
        else
            RemoveObstacle(CurrentObstacle);

        if (CurrentObstacle && CurrentObstaclePoint)
        {
            if (PathObstacle.GetDistance(CurrentPointPosition, GetPosition()) <= 0.1f)
                SetObstaclePoint(CurrentObstacle.GetNextPoint(GetPosition(), MT, CurrentObstaclePoint));
            if (CurrentObstaclePoint)
                MT = new Vector3(CurrentPointPosition.x, GetPosition().y, CurrentPointPosition.z);
        }
    }

    public void PathFindingUpdate_Alter()
    {
        MT = MoveTarget;
        if (PresetTarget)
            MT = PresetTarget.transform.position;
        MT = new Vector3(MT.x, GetPosition().y, MT.z);

        Path.ClearCorners();
        Agent.CalculatePath(MT, Path);
        if (Path.corners.Length > 1)
        {
            print(Path.corners[1]);
            MT = Path.corners[1];
        }
    }

    public void PathFindingMovement()
    {
        if (PathObstacle.GetDistance(GetPosition(), MT) <= 0.1f)
        {
            ReachMoveTarget();
        }
        else
        {
            SetDirection(MT - GetPosition());
            SetSpeed(MT - GetPosition());
        }
    }

    public void SetObstacle(PathObstacle PO, Vector3 ContactPoint, Vector3 MoveTargetPosition)
    {
        CurrentObstacle = PO;
        SetObstaclePoint(PO.GetFirstPoint(GetPosition(), ContactPoint, MoveTargetPosition, PathfindingRayLayer));
    }

    public void RemoveObstacle(PathObstacle PO)
    {
        if (CurrentObstacle != PO)
            return;
        CurrentObstacle = null;
        SetObstaclePoint(null);
    }

    public void SetObstaclePoint(PathObstaclePoint Point)
    {
        CurrentObstaclePoint = Point;
        if (Point)
            CurrentPointPosition = Point.GetPosition();
    }

    public void SetDirection(Vector3 Target)
    {
        RotationFinished = false;
        RotationPivot.transform.forward = Target;
    }

    public void SetSpeed(Vector3 Value)
    {
        Rig.velocity = Value.normalized * GetSpeed();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public float GetSpeed()
    {
        return Speed;
    }
}