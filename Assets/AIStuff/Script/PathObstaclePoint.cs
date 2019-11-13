using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class PathObstaclePoint : MonoBehaviour {
        public PathObstacle PO;
        public int Index;
        public float PreviousDistance;
        public float NextDistance;
        [Space]
        public float MaxRadius = 0.9f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public Vector3 GetAbsPosition()
        {
            return transform.position;
        }

        public Vector3 GetPosition()
        {
            return transform.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * Random.Range(0, MaxRadius);
        }
    }
