using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public GameObject enemyPrefab;
    public List<GameObject> spawnPositions;
    public GameObject player;
    public float speed = 1f;


    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPositions[Random.Range(0, 4)].transform.position, enemyPrefab.transform.rotation);
            enemy.transform.LookAt(player.transform);
            StartCoroutine(SendHoming(enemy));
        }
    }

    public IEnumerator SendHoming(GameObject enemy)
    {
        while (Vector3.Distance(player.transform.position, enemy.transform.position) > 0.3)
        {
            enemy.transform.position += (player.transform.position - enemy.transform.position).normalized * speed * Time.deltaTime;
            enemy.transform.LookAt(player.transform);
            yield return null;
        }
        Destroy(enemy);
    }

}
