using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEvents : MonoBehaviour
{
    private GameManager gameManager;
    private Tile existingPrompt;
    

    // One event is always neutral
    private int numberOfEvents = 6;

    void Update()
    {
        if (gameManager.round <= gameManager.maxRounds)
        {
            if (gameManager.turn != 0)
            {
                int randomNumber = Random.Range(0, numberOfEvents);

                switch (randomNumber)
                {
                    case 0:
                        EarthquakeEvent();
                        break;
                    case 1:
                        BadConstructionEvent();
                        break;
                    case 2:
                        FireEvent();
                        break;
                    case 3:
                        StormEvent();
                        break;
                    case 4:
                        CounterClockTurnsEvent();
                        break;
                    case 5:
                        NoEvent();
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
