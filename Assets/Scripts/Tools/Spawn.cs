using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    private float spawmtimer;
    public float SpawnRate;
    public float RandomRange;
    public float ScaleMax;
    public float ScaleMin;
    public List<GameObject> Items = new List<GameObject>();
    void Update()
    {
        spawmtimer += Time.deltaTime;
        if(spawmtimer > Random.Range(SpawnRate - RandomRange, SpawnRate + RandomRange))
        {
            var spawnedobject = Instantiate(Items[Random.Range(0,Items.Count)], transform.position, Quaternion.identity);
            spawnedobject.transform.localScale = spawnedobject.transform.localScale * Random.Range(ScaleMax, ScaleMin);
            spawmtimer = 0f;
        }
    }
}
