using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z
{
    public class TempMovement : MonoBehaviour {
        public GameObject PresetTarget;
        public Vector3 MoveTarget;
        public PathObstacle CurrentObstacle;
        public PathObstaclePoint CurrentObstaclePoint;
        public Vector3 CurrentPointPosition;
        public float RayCastDistance = 10f;
        public LayerMask PathfindingRayLayer;
        [Space]
        public Rigidbody Rig;
        public GameObject RotationPivot;
        public float Speed;

        public void Awake()
        {

        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Vector3 MT = MoveTarget;
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

            SetDirection(MT);

            Rig.velocity = RotationPivot.transform.forward * Speed;
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
            RotationPivot.transform.forward = Target - RotationPivot.transform.position;
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

    public enum DefenceType
    {
        Empty,
        Light,
        Heavy
    }
}