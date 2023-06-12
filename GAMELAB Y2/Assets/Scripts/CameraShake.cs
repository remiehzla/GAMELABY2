using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
   
    [SerializeField] private float duration;
    [SerializeField] private float magnitude;

    public void Update()
    {
        Shake();
    }
    public void Shake()
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;


        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            this.gameObject.transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;
            break;
        }
        this.gameObject.transform.localPosition = originalPos;
    }
}
