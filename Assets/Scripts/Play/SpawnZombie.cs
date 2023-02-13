using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnZombie : MonoBehaviour
{
    public GameObject[] zombiePrefab;
    public int zombieSum;
    public float spawnRadius;
    public bool spawnOnStart;

    private void Start()
    {
        if (spawnOnStart)
        {
            SpawnAll();
        }
    }

    public void SpawnAll()
    {
        for (int i = 0; i < zombieSum; i++)
        {
            Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadius;

            int randomIndex = RandomIndex(zombiePrefab);
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPos, out hit, 5.0f, NavMesh.AllAreas))
            {
                Instantiate(zombiePrefab[randomIndex],randomPos,Quaternion.identity);
            }
            else
            {
                i--;
            }
        }
    }

    public int RandomIndex(GameObject[] g)
    {
        return Random.Range(0, g.Length);
    }

 }
