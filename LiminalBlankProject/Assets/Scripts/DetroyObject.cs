using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetroyObject : MonoBehaviour
{

    void Start()
    {
        Invoke("ObjectKill", 3f);
    }

    void ObjectKill()
    {
        Destroy(gameObject);
    }

}
