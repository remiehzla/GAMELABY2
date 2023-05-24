using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEvents : MonoBehaviour
{
    private GameManager gameManager;

    // The four player' maps
    public GameObject map1;
    public GameObject map2;
    public GameObject map3;
    public GameObject map4;

    // The map's tiles
    public int numberOfTiles = 22;
    public int randomTile;

    // Choosing events
    [SerializeField] private int currentTurn;
    [SerializeField] private int randomEvent;
    [SerializeField] private int totalEvents;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (currentTurn != gameManager.turn)
            ChooseEvent();
    }

    private void ChooseEvent()
    {
        randomEvent = Random.Range(0, totalEvents);
        currentTurn = gameManager.turn;

        if (gameManager.round <= gameManager.maxRounds)
        {
            if (gameManager.turn != 0)
            {
                // The events
                switch (randomEvent)
                {
                    case 0:
                        NoEvent();
                        break;

                    case 1:
                        Invoke("EarthquakeEvent", 1);
                        break;
                }
            }
        }
    }

    private void EarthquakeEvent()
    {
        // Choose a random tile, may it have a prompt built or not
        // Play a shaking + move down animation
        // Destory tile

        switch (gameManager.turn)
        {
            case 1:
                {
                    // P1
                    randomTile = Random.Range(0, numberOfTiles);
                    map1.transform.GetChild(randomTile).gameObject.SetActive(false);

                    break;
                }
            case 2:
                {
                    // P2
                    break;
                }
            case 3:
                {
                    // P3
                    break;
                }
            case 4:
                {
                    // P4
                    break;
                }
        }
    }

    private void NoEvent()
    {
        // Do nothing
    }
}
