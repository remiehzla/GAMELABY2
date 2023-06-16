using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour //able to be accessed by orhee scripts
{
    IEnumerator Shake (float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < duration) // the time that we started shaking doesnt exceed the suration
        {
            //offsetting the came
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);
            //applying the offset to the camera position

            yield return null; // waiting for the next frame to be drawn

            elapsed += Time.deltaTime; // will kee running until the duration is exceeded
            yield return null;
        }
        transform.localPosition = originalPos;


        
    }
}
