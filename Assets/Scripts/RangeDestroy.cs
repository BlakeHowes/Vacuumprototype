using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDestroy : MonoBehaviour
{
    public void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
