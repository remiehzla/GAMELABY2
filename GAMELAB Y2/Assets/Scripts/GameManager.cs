using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
using Unity.Services.Analytics;
using Unity.Services.Core;

public class GameManager : MonoBehaviour
{
    public static int playerCount;

    public int money;
    public int manpower;
    public int round;
    public int turn;

    public int maxRounds;

    [SerializeField] private Transform cameraTransform;

    public List<Transform> cameraFocus = new List<Transform>();

    [SerializeField] private Animator fadeScreen;

    [SerializeField] private GameObject pointDisplay;

    [SerializeField] private Text moneyCounter;
    [SerializeField] private Text manpowerCounter;
    [SerializeField] private Text roundCounter;
    [SerializeField] private Text turnCounter;

    [SerializeField] private Text playerCounter;

    [SerializeField] private GameObject skipButton;
    [SerializeField] private Text endButtonText;

    private PromptManager promptManager;
    private RandomEvents randomEvents;
    private ResourceManager resourceManager;

    private bool isFading;

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

    void Start()
    {
        GetAnalytics();
        promptManager = FindObjectOfType<PromptManager>();
        randomEvents = FindObjectOfType<RandomEvents>();
        resourceManager = FindObjectOfType<ResourceManager>();

    }

    void Update()
    {
        // Update the UI

        if (playerCounter != null)
        {
            playerCounter.text = "Amount of players: " + playerCount;
        }

        moneyCounter.text = "Money: " + money.ToString();
        manpowerCounter.text = "Manpower: " + manpower.ToString();
        roundCounter.text = "Round: " + round.ToString();
        turnCounter.text = "Player: " + turn.ToString();

        // Enable scoreboard when nobody has their turn

        if (turn == 0)
        {
            skipButton.SetActive(false);
            pointDisplay.SetActive(true);
            pointDisplay.GetComponent<Animator>().SetBool("Enabled", true);
        }
        else
        {
            skipButton.SetActive(true);
            pointDisplay.SetActive(false);
            pointDisplay.GetComponent<Animator>().SetBool("Enabled", false);
        }

        // Update UI to end game after rounds are over

        if (round > maxRounds)
        {
            endButtonText.text = "Back to menu";
        }
    }

    public void IncreaseTurn()
    {
        // Move to the next player and move to the next round once everyone had their turn
        
        if (!isFading)
        {
            if (round <= maxRounds)
            {
                isFading = true;
                turn++;
                if (turn > playerCount)
                {
                    turn = 0;
                    Invoke("IncreaseRound", 1);
                }
                Invoke("FadeScreen", 1);
                Invoke("TransferTurn", 2);
            }
            else
            {
                EndGame();
            }         
        }
    }

    public void DecreaseTurn()
    {
        if (!isFading)
        {
            if (round <= maxRounds)
            {
                isFading = true;
                turn--;
                if (turn <= 0)
                {
                    turn = 0;
                    randomEvents.counterClockedTurns = false;
                    Invoke("IncreaseRound", 1);
                }
                Invoke("FadeScreen", 1);
                Invoke("TransferTurn", 2);
            }
            else
            {
                EndGame();
            }
        }
    }


    void FadeScreen()
    {
        fadeScreen.SetTrigger("Fade");
    }

    void TransferTurn()
    {
        // Move the camera && reroll UI tiles when turn switches

        isFading = false;

        cameraTransform.position = cameraFocus[turn].position;
        if (turn != 0)
        {
            promptManager.RandomizePrompts();
        }
    }

    void IncreaseRound()
    {
        // I hope that if you read this you can do the math here yourself

        round += 1;
    }

    void EndGame()
    {
        // Restart the scene and send analytics

        Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "gamesFinished", + 1 },
            };
        AnalyticsService.Instance.CustomData("gameFinished", parameters);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
