using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingTexture : MonoBehaviour
{
    public Vector2 Scroll;
    public Vector2 Offset = new Vector2(0f, 0f);
    void Update()
    {
        Offset += Scroll * Time.deltaTime;
        var rend = GetComponent<ParticleSystemRenderer>();
        if (Input.GetMouseButton(0))
        {
            rend.trailMaterial.mainTextureOffset = -Offset;
        }
        if (Input.GetMouseButton(1))
        {
            rend.trailMaterial.mainTextureOffset = Offset;
        }
    }
}
