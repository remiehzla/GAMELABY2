using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOT USED SCRIPT
public class ResourceManager : MonoBehaviour
{
    [SerializeField] [Range(0f, 2000)] public float money;
    [SerializeField] [Range(0f, 1000)] public float manpower;
    [SerializeField] [Range(0f, 10)] public float round;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        money = Mathf.Clamp(money, 0f, 2000);
        manpower = Mathf.Clamp(manpower, 0f, 1000);
        round = Mathf.Clamp(round, 0f, 10);

        money = gameManager.money;
        manpower = gameManager.manpower;
        round = gameManager.round;
    }
}
