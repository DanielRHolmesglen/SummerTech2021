using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWall : MonoBehaviour
{
    void Start()
    {
        Invoke("ObjectKill", 15f);
    }

    void ObjectKill()
    {
        Destroy(gameObject);
    }
}
