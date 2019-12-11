using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour {
    [HideInInspector]
    public static AIControl Main;
    public Animator Anim;
    public Rigidbody Rig;
    public float Speed;
    public NavMeshAgent Agent;
    public NavMeshPath Path;
    public AIBehaviourControl ABC;
    [Space]
    public GameObject Pivot;
    public GameObject RotationPivot;
    public float RotationSpeed;
    public bool RotationFinished;
    [HideInInspector]
    public bool SubRotationFinished;
    public bool RotationDisable;
    public float RotationTime;
    public float CurrentRotationTime;
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
    public float CurrentDelay;
    public bool Delaying;
    [Space]
    public AIWork StartWork;
    public AIWork GooseWork;
    public AIWork CurrentWork;
    public AIWorkState CurrentWorkState;
    public float HeardCoolDown;
    public float MaxHeardCoolDown = 5f;
    [Space]
    public GameObject HandlePoint;
    public Item CurrentItem;
    [Space]
    public AIAnimSwitch Switch;

    public void Awake()
    {
        Main = this;
        AIBehaviourControl.Main = ABC;
        Path = new NavMeshPath();
        MoveTarget = GetPosition();
        StartCoroutine("PickUpSwitchProcess");
        StartCoroutine("ResetSwitchProcess");
    }

    // Start is called before the first frame update
    void Start()
    {
        if (StartWork)
            SetWork(StartWork);
    }

    // Update is called once per frame
    void Update()
    {
        if (RotationPivot && !RotationFinished && !RotationDisable)
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

        if (CurrentRotationTime > 0)
        {
            float T = PathObstacle.AbsAngle(RotationPivot.transform.eulerAngles.y);
            float O = PathObstacle.AbsAngle(Pivot.transform.eulerAngles.y);
            float a = O;
            a += PathObstacle.RotateDirection(O, T) * PathObstacle.RotationDistance(O, T) * ((RotationTime - CurrentRotationTime) / RotationTime);
            Vector3 b = Pivot.transform.eulerAngles;
            Pivot.transform.eulerAngles = new Vector3(b.x, a, b.z);
            SubRotationFinished = false;

            RotationTime = CurrentRotationTime;
            CurrentRotationTime -= Time.deltaTime;
        }
        else if (RotationTime > 0)
        {
            RotationTime = 0;
            RotationFinished = true;
            Pivot.transform.eulerAngles = RotationPivot.transform.eulerAngles;
            SubRotationFinished = true;
            NextStep();
        }

        if (CurrentWork)
        {
            MoveTarget = CurrentWork.GetTargetPosition();
            if (CurrentWork.OutRange())
                EndWork();
        }

        HeardCoolDown -= Time.deltaTime;

        PathFindingUpdate_Alter();

        if (CurrentDelay > 0)
        {
            CurrentDelay -= Time.deltaTime;
            SetSpeed(new Vector3());
            return;
        }
        else if (Delaying)
        {
            Delaying = false;
            NextStep();
        }

        PathFindingMovement();

        if (CurrentItem && CurrentItem.HeavyAnim)
            SetSwitchAnim("Hold", true);
        else
            SetSwitchAnim("Hold", false);
    }

    public void ReachMoveTarget()
    {
        if (CurrentWork)
        {
            SetSpeed(new Vector3());
            NextStep();
        }
    }

    public void SetMoveTarget(Vector3 Value)
    {
        MoveTarget = new Vector3(Value.x, GetPosition().y, Value.z);
    }

    public void SetDelay(float Value)
    {
        CurrentDelay = Value;
        if (Value > 0)
            Delaying = true;
        else
            Delaying = false;
    }

    public void Heard(string Key)
    {
        if (HeardCoolDown > 0)
            return;
        if (CurrentWork && CurrentWork.Protected)
            return;
        if (CurrentWork && CurrentWork.TargetItem && CurrentWork.TargetItem.gameObject == gooseControl.goose.GetComponentInChildren<GooseGrab>().hold)
            return;
        if (CurrentWork != GooseWork)
        {
            SetWork(GooseWork);
            HeardCoolDown = MaxHeardCoolDown;
        }
    }

    public void SetWork(AIWork Work)
    {
        if (!Work)
            return;
        if (CurrentWork)
            EndWork();
        Work.OnStart(CurrentWork);
        CurrentWork = Work;
        MoveTarget = CurrentWork.GetTargetPosition();
        SetWorkState(AIWorkState.Start);
    }

    public void EndWork()
    {
        if (!CurrentWork)
            return;
        AIWork AW = CurrentWork;
        RotationDisable = false;
        SetDelay(0);
        CurrentWork = null;
        AW.OnEnd();
    }

    public void NextStep()
    {
        if (CurrentWorkState == AIWorkState.Start)
            SetWorkState(AIWorkState.Rotate);
        else if (CurrentWorkState == AIWorkState.Rotate)
            SetWorkState(AIWorkState.PostRotate);
        else if (CurrentWorkState == AIWorkState.PostRotate)
            SetWorkState(AIWorkState.Move);
        else if (CurrentWorkState == AIWorkState.Move)
            SetWorkState(AIWorkState.End);
        else if (CurrentWorkState == AIWorkState.End)
            EndWork();
    }

    public void SetWorkState(AIWorkState Value)
    {
        CurrentWorkState = Value;
        if (Value == AIWorkState.Start && CurrentWork.StartDelay > 0)
        {
            RotationDisable = true;
            SetDelay(CurrentWork.StartDelay);
            SetInstantAnim(CurrentWork.StartAnim);
        }
        else if (Value == AIWorkState.Rotate && CurrentWork.RotationAnim)
        {
            RotationDisable = true;
            SetRotationTime(1f);
        }
        else if (Value == AIWorkState.PostRotate && CurrentWork.PRDelay > 0)
        {
            RotationDisable = true;
            SetDelay(CurrentWork.PRDelay);
            SetInstantAnim(CurrentWork.PRAnim);
        }
        else if (Value == AIWorkState.Move)
        {
            RotationDisable = false;
        }
        else if (Value == AIWorkState.End && CurrentWork.EndDelay > 0)
        {
            CurrentWork.OnPreEnd();
            RotationDisable = CurrentWork.EndRotationLock;
            SetDelay(CurrentWork.EndDelay);
            SetInstantAnim(CurrentWork.EndAnim);
        }
        else
            NextStep();
    }

    public void PickUpItem(Item I)
    {
        CurrentItem = I;
        I.OnPickUp();
        gooseControl.goose.GetComponentInChildren<GooseGrab>().Grabbed(I.gameObject);
    }

    public void DropItem(bool Reset)
    {
        if (!CurrentItem)
            return;

        if (Reset)
            CurrentItem.ItemReset();
        else
            CurrentItem.ItemDrop();
        CurrentItem = null;
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
            MT = Path.corners[1];

        if (PathObstacle.GetDistance(MT, GetPosition()) <= 0.05f)
            return;
        SetDirection(MT - GetPosition());
    }

    public void PathFindingMovement()
    {
        if (PathObstacle.GetDistance(GetPosition(), MT) <= 0.1f)
        {
            if (PathObstacle.GetDistance(GetPosition(), new Vector3(MoveTarget.x, GetPosition().y, MoveTarget.z)) <= 0.1f)
                ReachMoveTarget();
        }
        else
        {
            SetSpeed(MT - GetPosition());
        }

        if (CurrentWork != DoorControl.Main.OpenWork)
        {
            Ray R = new Ray(GetPosition(), MT - GetPosition());
            if (Physics.Raycast(R, (MT - GetPosition()).magnitude, DoorControl.Main.DoorRayCastLayer))
                SetWork(DoorControl.Main.OpenWork);
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
        if (Value.x == 0 && Value.z == 0)
            SetSwitchAnim("Walk", false);
        else
            SetSwitchAnim("Walk", true);
        Rig.velocity = Value.normalized * GetSpeed();
    }

    public void SetRotationTime(float Value)
    {
        CurrentDelay = Value;
        RotationTime = Value;
        CurrentRotationTime = Value;
        SetInstantAnim("Turn");
    }

    public void SetSwitchAnim(string Key, bool Value)
    {
        if (Key == "")
            return;
        Anim.SetBool(Key, Value);
    }

    public void SetInstantAnim(string Key)
    {
        if (Key == "")
            return;
        StartCoroutine(SetInstantAnimIE(Key));
    }

    public IEnumerator SetInstantAnimIE(string Key)
    {
        Anim.SetBool(Key, true);
        yield return new WaitForSeconds(0.25f);
        Anim.SetBool(Key, false);
    }

    public IEnumerator ResetSwitchProcess()
    {
        while (true)
        {
            if (Switch.ResetSwitch && CurrentItem)
            {
                DropItem(true);
                yield return new WaitForSeconds(0.1f);
            }
            yield return 0;
        }
    }

    public IEnumerator PickUpSwitchProcess()
    {
        while (true)
        {
            if (Switch.PickUpSwitch && CurrentWork && CurrentWork.TargetItem)
            {
                PickUpItem(CurrentWork.TargetItem);
                yield return new WaitForSeconds(0.1f);
            }
            yield return 0;
        }
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