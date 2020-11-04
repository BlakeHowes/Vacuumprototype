using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    private Vector3 currentpos;
    public GameObject Player;
    public float smoothSpeed;
    public Vector3 smoothedPosition;
    public Vector3 desiredPosition;
    public float offsetmax;
    public float shakeforce;
    public float magnitudeofshake;
    void Awake()
    {
        currentpos = transform.position;
    }


    void FixedUpdate()
    {
        Vector3 desiredPosition = (currentpos + Player.transform.position);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = (smoothedPosition);
    }

    public void camerashake()
    {
        StartCoroutine(Shake(0.1f, magnitudeofshake));
    }

    public IEnumerator Shake(float duration, float magnitude)
    {

        float elapsed = 0f;

        while (elapsed < duration)
        {
            Vector3 offset = new Vector3(Player.transform.position.x * offsetmax, Player.transform.position.y * offsetmax, Player.transform.position.z * offsetmax);

            float x = Random.Range(-shakeforce, shakeforce) * magnitude;
            float y = Random.Range(-shakeforce, shakeforce) * magnitude;
            float z = Random.Range(-shakeforce, shakeforce) * magnitude;

            Vector3 pos = transform.position;

            magnitude -= 0.02f;

            Vector3 shake = new Vector3(x + pos[0], y + pos[1], z + pos[2]);
            transform.position = shake + offset;
            elapsed += Time.deltaTime;
            yield return 0;
        }

    }
}
