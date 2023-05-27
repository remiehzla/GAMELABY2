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

    public GameObject smokeEffect;
    public GameObject currentSmokeEffect;
    public GameObject currentTile;

    [SerializeField] private int randomTile;

    // Choosing events
    [SerializeField] private int currentTurn;
    [SerializeField] private int randomEvent;
    [SerializeField] private int totalEvents;

    // Natural events
    [SerializeField] public bool promptFound = false;

    // Counterclocking turns
    public bool counterClockedTurns = false;

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

    private void Update()
    {
        if (currentTurn != gameManager.turn)
            ChooseEvent();
        Debug.Log(currentTile);
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

                    case 3:
                        Invoke("CounterClockTurns", 1);
                        break;

                    case 4:
                        Invoke("IncreaseEconomy", 1);
                        break;

                    case 5:
                        Invoke("DecreaseEconomy", 1);
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
                    randomTile = Random.Range(0, map1.transform.childCount);
                    map1.transform.GetChild(randomTile).gameObject.SetActive(false);
                    map1.transform.GetChild(randomTile).gameObject.GetComponent<Tile>().hasPrompt = false;
                    break;
                }
            case 2:
                {
                    // P2
                    randomTile = Random.Range(0, map2.transform.childCount);
                    map2.transform.GetChild(randomTile).gameObject.GetComponent<Tile>().hasPrompt = false;
                    break;
                }
            case 3:
                {
                    // P3
                    randomTile = Random.Range(0, map3.transform.childCount);
                    map3.transform.GetChild(randomTile).gameObject.GetComponent<Tile>().hasPrompt = false;
                    break;
                }
            case 4:
                {
                    // P4
                    randomTile = Random.Range(0, map4.transform.childCount);
                    map4.transform.GetChild(randomTile).gameObject.GetComponent<Tile>().hasPrompt = false;
                    break;
                }
        }
    }

    private void NaturalEvent()
    {
        switch (gameManager.turn)
        {
            case 1:
                // P1
                promptFound = false; // Reset promptFound 

                for (int i = 0; i < map1.transform.childCount && !promptFound; i++)
                {
                    currentTile = map1.transform.GetChild(i).gameObject;
                    if (currentTile.GetComponent<Tile>().hasPrompt)
                    {
                        promptFound = true;
                        currentSmokeEffect = Instantiate(smokeEffect, currentTile.transform.position, currentTile.transform.rotation);
                        StartCoroutine(DisableBuilding(currentSmokeEffect));
                    }
                }
                break;

            case 2:
                // P2
                promptFound = false; // Reset promptFound 

                for (int i = 0; i < map2.transform.childCount && !promptFound; i++)
                {
                    currentTile = map2.transform.GetChild(i).gameObject;
                    if (currentTile.GetComponent<Tile>().hasPrompt)
                    {
                        promptFound = true;
                        currentSmokeEffect = Instantiate(smokeEffect, currentTile.transform.position, currentTile.transform.rotation);
                        StartCoroutine(DisableBuilding(currentSmokeEffect));
                    }
                }                
                break;

            case 3:
                // P3
                promptFound = false; // Reset promptFound 

                for (int i = 0; i < map3.transform.childCount && !promptFound; i++)
                {
                    currentTile = map3.transform.GetChild(i).gameObject;
                    if (currentTile.GetComponent<Tile>().hasPrompt)
                    {
                        promptFound = true;
                        currentSmokeEffect = Instantiate(smokeEffect, currentTile.transform.position, currentTile.transform.rotation);
                        StartCoroutine(DisableBuilding(currentSmokeEffect));
                    }
                }
                break;

            case 4:
                // P4
                promptFound = false; // Reset promptFound 

                for (int i = 0; i < map4.transform.childCount && !promptFound; i++)
                {
                    currentTile = map4.transform.GetChild(i).gameObject;
                    if (currentTile.GetComponent<Tile>().hasPrompt)
                    {
                        promptFound = true;
                        currentSmokeEffect = Instantiate(smokeEffect, currentTile.transform.position, currentTile.transform.rotation);
                        StartCoroutine(DisableBuilding(currentSmokeEffect));
                    }
                }
                break;
        }
    }

    private void CounterClockTurns()
    {
        counterClockedTurns = true;
    }

    private void IncreaseEconomy()
    {
        gameManager.money += gameManager.money + Random.Range(10, 1000);
    } 
    private void DecreaseEconomy()
    {
        gameManager.money -= gameManager.money - Random.Range(10, 1000);
    }

    public IEnumerator DisableBuilding(GameObject smokeEffectInstance)
    {
        yield return new WaitForSeconds(10);
        Destroy(currentTile.GetComponent<Tile>().prompt);
        currentTile = null;
        smokeEffectInstance.SetActive(false);
    }

    private void NoEvent()
    {
        // Do nothing
    }
}
