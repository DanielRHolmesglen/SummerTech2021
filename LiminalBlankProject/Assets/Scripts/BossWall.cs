using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWall : MonoBehaviour
{
    void Start()
    {
        Invoke("ObjectKill", 11f);
    }

    void ObjectKill()
    {
        Destroy(gameObject);
    }
}
