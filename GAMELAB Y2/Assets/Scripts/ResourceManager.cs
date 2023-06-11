using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] [Range(0f, 2000)] public float money;
    [SerializeField] [Range(0f, 1000)] public float manpower;
    [SerializeField] [Range(0f, 10)] public float rounds;

    // Update is called once per frame
    void Update()
    {
        money = Mathf.Clamp(money, 0f, 2000);
        manpower = Mathf.Clamp(manpower, 0f, 1000);
        rounds = Mathf.Clamp(rounds, 0f, 10);
    }
}
