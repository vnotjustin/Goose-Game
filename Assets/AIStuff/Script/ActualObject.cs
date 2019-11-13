using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class ActualObject : MonoBehaviour {
        public bool Messed;
        public Vector3 OriPosition;
        [Space]
        public float PickUpDelay;
        public float PutDownDelay;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public Vector3 GetMovePosition()
        {
            return OriPosition;
        }
    }
