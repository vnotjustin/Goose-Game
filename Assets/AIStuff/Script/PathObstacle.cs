using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z
{
    public class PathObstacle : MonoBehaviour {
        public List<PathObstaclePoint> Points;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public PathObstaclePoint GetFirstPoint(Vector3 OriPosition, Vector3 ContactPosition, Vector3 MoveTargetPosition, LayerMask Layer)
        {
            List<PathObstaclePoint> Temp = new List<PathObstaclePoint>();
            foreach (PathObstaclePoint P in Points)
            {
                Vector3 RealOri = OriPosition;
                if (!Physics.Raycast(RealOri, P.GetAbsPosition() - RealOri, GetDistance(RealOri, P.GetAbsPosition()), Layer))
                    Temp.Add(P);
            }

            float a = Mathf.Infinity;
            PathObstaclePoint Point = null;
            PathObstaclePoint SecondPoint = null;
            foreach (PathObstaclePoint G in Temp)
            {
                float b = GetDistance(G.GetAbsPosition(), ContactPosition);
                if (b <= a)
                {
                    a = b;
                    Point = G;
                }
            }
            float c = Mathf.Infinity;
            foreach (PathObstaclePoint G in Temp)
            {
                float b = GetDistance(G.GetAbsPosition(), ContactPosition);
                if (b <= c && G != Point)
                {
                    c = b;
                    SecondPoint = G;
                }
            }
            if (SecondPoint)
            {
                float q = GetPointDistance(Point.Index, GetFinalPoint(MoveTargetPosition).Index, out int d1);
                float w = GetPointDistance(SecondPoint.Index, GetFinalPoint(MoveTargetPosition).Index, out int d2);
                if (w < q)
                    Point = SecondPoint;
            }
            return Point;
        }

        public PathObstaclePoint GetNextPoint(Vector3 OriPosition, Vector3 FinalPosition, PathObstaclePoint CurrentPoint)
        {
            if (!CurrentPoint)
                return null;

            int a = CurrentPoint.Index;
            int b = GetFinalPoint(FinalPosition).Index;

            GetPointDistance(a, b, out int d);
            if (d == -1)
                return Points[GetPreviousIndex(a)];
            else
                return Points[GetNextIndex(a)];
        }

        public PathObstaclePoint GetFinalPoint(Vector3 Position)
        {
            PathObstaclePoint SetPoint = null;
            float d = Mathf.Infinity;
            for (int i = Points.Count - 1; i >= 0; i--)
            {
                float t = GetDistance(Points[i].GetAbsPosition(), Position);
                if (t <= d)
                {
                    d = t;
                    SetPoint = Points[i];
                }
            }
            return SetPoint;
        }

        public float GetPointDistance(int A, int B, out int D)
        {
            PathObstaclePoint PA = Points[A];
            PathObstaclePoint PB = Points[B];

            float l = 0;
            float r = 0;
            int Temp = A;
            while (Temp != B)
            {
                int T = Temp;
                Temp = GetPreviousIndex(Temp);
                l += GetDistance(Points[T].GetAbsPosition(), Points[Temp].GetAbsPosition());
            }
            Temp = A;
            while (Temp != B)
            {
                int T = Temp;
                Temp = GetNextIndex(Temp);
                r += GetDistance(Points[T].GetAbsPosition(), Points[Temp].GetAbsPosition());
            }

            if (l < r)
            {
                D = -1;
                return l;
            }
            else
            {
                D = 1;
                return r;
            }
        }

        public int GetNextIndex(int Value)
        {
            if (Points.Count <= 0)
                return -1;

            int a = Value + 1;
            while (a >= Points.Count)
                a -= Points.Count;
            return a;
        }

        public int GetPreviousIndex(int Value)
        {
            if (Points.Count <= 0)
                return -1;

            int a = Value - 1;
            while (a < 0)
                a += Points.Count;
            return a;
        }

        public void EditorApply()
        {
            Points = new List<PathObstaclePoint>();
            foreach (PathObstaclePoint POP in GetComponentsInChildren<PathObstaclePoint>())
                Points.Add(POP);
            for (int i = 0; i < Points.Count; i++)
            {
                Points[i].PO = this;
                Points[i].Index = i;
                Points[i].PreviousDistance = GetDistance(Points[i].GetAbsPosition(), Points[GetPreviousIndex(i)].GetAbsPosition());
                Points[i].NextDistance = GetDistance(Points[i].GetAbsPosition(), Points[GetNextIndex(i)].GetAbsPosition());
            }
            foreach (PathObstacleCollider POC in GetComponentsInChildren<PathObstacleCollider>())
                POC.Obstacle = this;
        }

        public static float GetDistance(Vector3 A, Vector3 B)
        {
            return Mathf.Abs((A - B).magnitude);
        }

        public static float AbsAngle(float OriAngle)
        {
            if (OriAngle < 0)
                return OriAngle + 360;
            else if (OriAngle >= 360)
                return OriAngle - 360;
            else
                return OriAngle;
        }

        public static int RotateDirection(float RotateAngle, float TargetAngle)
        {
            if (TargetAngle > RotateAngle)
            {
                float a = TargetAngle - RotateAngle;
                float b = RotateAngle + 360 - TargetAngle;
                if (a >= b)
                    return -1;
                else
                    return 1;
            }
            else if (TargetAngle < RotateAngle)
            {
                float a = RotateAngle - TargetAngle;
                float b = TargetAngle + 360 - RotateAngle;
                if (a > b)
                    return 1;
                else
                    return -1;
            }
            return 0;
        }
    }
}