using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEvents : MonoBehaviour
{
    private GameManager gameManager;

    // One event is always neutral
    private int numberOfEvents = 7;

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
                    case 6:
                        break;

                }
            }
        }
    }

    private void EarthquakeEvent()
    {   

    }
    
    private void BadConstructionEvent()
    { 

    }

    private void FireEvent()
    { 

    }

    private void StormEvent()
    { 

    }

    private void CounterClockTurnsEvent()
    { 

    }

    private void NoEvent()
    {

    }
}
