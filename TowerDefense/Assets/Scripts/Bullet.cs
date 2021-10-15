using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview
{
    public abstract class Bullet : MonoBehaviour
    {
        public GameObject targetObject;

        public virtual void Initialize(GameObject target)
        {
            targetObject = target;
        }
    }
}
