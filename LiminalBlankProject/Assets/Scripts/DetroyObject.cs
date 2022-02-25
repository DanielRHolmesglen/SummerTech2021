using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetroyObject : MonoBehaviour
{
    public float destroyTime = 3f;

    void Start()
    {
        Invoke("ObjectKill", destroyTime);
    }

    void ObjectKill()
    {
        Destroy(gameObject);
    }

}
