using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEvents : MonoBehaviour
{
    private GameManager gameManager;
   // private Tile existingPrompt;

    // The four player' maps
    public GameObject map1;
    public GameObject map2;
    public GameObject map3;
    public GameObject map4;

    public int numberOfTiles = 22;
    public int randomTile;

    [SerializeField] private bool eventTrigger;
    [SerializeField] private int randomNumber;

    // One event must be neutral
    private int numberOfEvents = 6;

    private void Start()
    {
        //randomTile = null;
        gameManager = FindObjectOfType<GameManager>();
        eventTrigger = false;
        ChooseEvent();
    }

    void Update()
    {
        if (gameManager.round <= gameManager.maxRounds)
        {
            if (gameManager.turn != 0)
            {  
                switch (randomNumber)
                {
                    case 0:
                        Invoke("EarthquakeEvent", 1);
                        eventTrigger = true;
                        break;
                    case 1:
                        NoEvent();
                        break;
                    /*case 2:
                        FireEvent();
                        break;
                    case 3:
                        StormEvent();
                        break;
                    case 4:
                        CounterClockTurnsEvent();
                        break;
                    case 5:
                        BadConstructionEvent();
                        break;*/

                }

               // EarthquakeEvent();
            }
        }
    }

    private void ChooseEvent()
    {
        randomNumber = Random.Range(0, 2);
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
                    if (eventTrigger)
                    { 
                        eventTrigger = false;
                        randomTile = Random.Range(0, numberOfTiles);
                        Invoke("ChooseEvent", 1);
                    }
                    map1.transform.GetChild(randomTile).gameObject.SetActive(false);
                    
                    break;
                }
            case 2:
                {
                    // P2
                    if (eventTrigger)
                    {
                        eventTrigger = false;
                        randomTile = Random.Range(0, numberOfTiles);
                        Invoke("ChooseEvent", 1);
                    }
                    map2.transform.GetChild(randomTile).gameObject.SetActive(false);
                    break;
                }
            case 3:
                {
                    // P3
                    if (eventTrigger)
                    {
                        eventTrigger = false;
                        randomTile = Random.Range(0, numberOfTiles);
                        Invoke("ChooseEvent", 1);
                    }
                    map3.transform.GetChild(randomTile).gameObject.SetActive(false);
                    break;
                }
            case 4:
                {
                    // P4
                    if (eventTrigger)
                    {
                        eventTrigger = false;
                        randomTile = Random.Range(0, numberOfTiles);
                        Invoke("ChooseEvent", 1);
                    }
                    map4.transform.GetChild(randomTile).gameObject.SetActive(false);
                    break;
                }
        }
    }

    private void BadConstructionEvent()
    { 
        // Choose a random prompt
        // Play smoke particle effect
        // Destroy it
    }

    private void FireEvent()
    { 
        // Choose a random prompt
        // Play fire particle effect
        // Destroy it
    }

    private void StormEvent()
    { 
        // Choose a random prompt
        // Play wind patricle effect
        // Destroy it
    }

    private void CounterClockTurnsEvent()
    { 
        // Counter clock turns
        // Game goes on
    }

    private void NoEvent()
    {
        // Do nothing
    }
}
