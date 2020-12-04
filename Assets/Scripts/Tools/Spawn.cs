using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public float spawmtimer;
    public float SpawnRate;
    public float RandomRange;
    public float ScaleMax;
    public float ScaleMin;
    public LayerMask triggerzone;
    public List<GameObject> Items = new List<GameObject>();
    public List<GameObject> EnemySpawnPoints = new List<GameObject>();
    public float SpawnRange;
    void Update()
    {
        spawmtimer += Time.deltaTime;
        if (spawmtimer > Random.Range(SpawnRate - RandomRange, SpawnRate + RandomRange))
        {
            Debug.Log(Vector3.Distance(transform.position, EnemySpawnPoints[0].transform.position));
            foreach (GameObject spawn in EnemySpawnPoints)
            {
                if (Vector3.Distance(transform.position, spawn.transform.position) <= SpawnRange)
                {
                    var spawnedobject = Instantiate(Items[Random.Range(0, Items.Count)], transform.position, Quaternion.identity);
                    spawnedobject.transform.localScale = spawnedobject.transform.localScale * Random.Range(ScaleMax, ScaleMin);
                    spawmtimer = 0f;
                }
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position,transform.localScale.x);
    }
}


