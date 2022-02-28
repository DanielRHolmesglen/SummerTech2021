using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetroyObject : MonoBehaviour
{
    [SerializeField] private float destroyTime = 3f;

    void Start()
    {
        Invoke("ObjectKill", destroyTime);
    }

    void ObjectKill()
    {
        Destroy(gameObject);
    }

}
