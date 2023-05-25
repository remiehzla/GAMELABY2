using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using Unity.Services.Analytics;
using Unity.Services.Core;

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

    async void GetAnalytics()
    {
        try
        {
            await UnityServices.InitializeAsync();
            List<string> consentIdentifiers = await AnalyticsService.Instance.CheckForRequiredConsents();
        }
        catch (ConsentCheckException e)
        {
            // Something went wrong when checking the GeoIP, check the e.Reason and handle appropriately.
        }
    }

    private void Start()
    {
        GetAnalytics();
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
                Dictionary<string, object> parameters = new Dictionary<string, object>()
                        {
                            { "randomEvents", + 1 },
                        };

                // The events
                switch (randomEvent)
                {
                    case 0:
                        NoEvent();
                        break;

                    case 1:
                        Invoke("EarthquakeEvent", 1);
                        AnalyticsService.Instance.CustomData("countRandomEvents", parameters);
                        break;
                    case 2:
                        Invoke("NaturalEvent", 1);
                        AnalyticsService.Instance.CustomData("countRandomEvents", parameters);
                        break;
                }
            }
        }
    }

    private void EarthquakeEvent()
    {
        // Choose a random tile, may it have a prompt built or not
        // Play a shaking + move down animation
        // Replace texture/disable tile

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
                    randomTile = Random.Range(0, numberOfTiles);
                    map2.transform.GetChild(randomTile).gameObject.SetActive(false);

                    break;
                }
            case 3:
                {
                    // P3
                    randomTile = Random.Range(0, numberOfTiles);
                    map3.transform.GetChild(randomTile).gameObject.SetActive(false);

                    break;
                }
            case 4:
                {
                    // P4
                    randomTile = Random.Range(0, numberOfTiles);
                    map4.transform.GetChild(randomTile).gameObject.SetActive(false);

                    break;
                }
        }
    }

    private void NaturalEvent()
    {
        // Play fire OR water particle system
        // Destroy building

        switch (gameManager.turn)
        {
            case 1:
                // P1
                break;

            case 2:
                // P2
                break;

            case 3:
                // P3
                break;

            case 4:
                // P3
                break;
        }
    }

    private void NoEvent()
    {
        // Do nothing
    }
}
