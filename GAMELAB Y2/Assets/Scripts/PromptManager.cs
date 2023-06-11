using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using Unity.Services.Analytics;
using Unity.Services.Core;

public class PromptManager : MonoBehaviour
{
    public List<GameObject> prompts = new List<GameObject>();

    public int selectedPrompt;
    public bool demolishMode;

    [SerializeField] private int demolishMoney;
    [SerializeField] private int demolishManpower;

    public List<int> promptChoice = new List<int>();

    [SerializeField] private GameObject promptUI;

    [SerializeField] private List<Text> promptChoiceTextName = new List<Text>();
    [SerializeField] private List<Text> promptChoiceTextMoney = new List<Text>();
    [SerializeField] private List<Text> promptChoiceTextManpower = new List<Text>();
    [SerializeField] private List<Text> promptChoiceTextRounds = new List<Text>();

    private GameManager gameManager;
    private RandomEvents randomEvents;


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
        randomEvents = FindObjectOfType<RandomEvents>();
    }

    public void RandomizePrompts()
    {
        promptUI.SetActive(true);

        // Select random prompt from list and update UI for all 3 options

        for (int x = 0; x <= promptChoice.Count; x++)
        {
            for (int i = Random.Range(1, prompts.Count); i <= prompts.Count; i++)
            {
                string promptName = prompts[i].GetComponent<Prompt>().promptName;
                string neededMoney = prompts[i].GetComponent<Prompt>().neededMoney.ToString();
                string neededManpower = prompts[i].GetComponent<Prompt>().neededManpower.ToString();
                string neededRounds = prompts[i].GetComponent<Prompt>().neededRounds.ToString();
                promptChoice[x] = i;
                promptChoiceTextName[x].text = promptName;
                promptChoiceTextMoney[x].text = neededMoney;
                promptChoiceTextManpower[x].text = neededManpower;
                promptChoiceTextRounds[x].text = neededRounds;
                break;
            }
        }

        // If any options are the same, reroll

        if (promptChoice[0] == promptChoice[1] || promptChoice[1] == promptChoice[2] || promptChoice[2] == promptChoice[0] 
        || promptChoice[0] == promptChoice[1] && promptChoice[0] == promptChoice[2])
        {
            RandomizePrompts();
        }
    }

    public void ChoosePrompt1()
    {
        selectedPrompt = promptChoice[0];
        promptUI.SetActive(false);
    }
    public void ChoosePrompt2()
    {
        selectedPrompt = promptChoice[1];
        promptUI.SetActive(false);
    }
    public void ChoosePrompt3()
    {
        selectedPrompt = promptChoice[2];
        promptUI.SetActive(false);
    }
    public void ChooseDemolish()
    {
        demolishMode = true;
        promptUI.SetActive(false);
    }
    public void ChooseSkip()
    {
        promptUI.SetActive(false);
        if (randomEvents.counterClockedTurns)
            gameManager.DecreaseTurn();
        else
            gameManager.IncreaseTurn();
    }

    public void ChangePromptNumber()
    {
        // Toggle between the prompts in the list

        if (selectedPrompt < prompts.Count - 1)
        {
            selectedPrompt += 1;
        }
        else
        {
            selectedPrompt = 0;
        }
    }

    public void PlacePrompt(Transform location)
    {
        // Place down the prompt selected from the list on the tile that called the function

        if (!promptUI.activeInHierarchy)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "placedPrompts", + 1 },
            };
            AnalyticsService.Instance.CustomData("countPlacedPrompts", parameters);

            GameObject prompt = Instantiate(prompts[selectedPrompt], location.position, Quaternion.identity);
            prompt.transform.parent = location;

            location.GetComponent<Tile>().prompt = prompt;
            location.GetComponent<Tile>().hasPrompt = true;

            selectedPrompt = 0;
            demolishMode = false;

            if (randomEvents.counterClockedTurns)
                gameManager.DecreaseTurn();
            else
                gameManager.IncreaseTurn();
        }
    }

    public void DemolishPrompt(GameObject prompt)
    {
        // Demolish the prompt selected by the tile that called the function

        if (!promptUI.activeInHierarchy)
        {
            if (!demolishMode || gameManager.money >= demolishMoney && gameManager.manpower >= demolishManpower)
            {
                if (demolishMode)
                {
                    gameManager.money = gameManager.money -= demolishMoney;
                    gameManager.manpower = gameManager.manpower -= demolishManpower;
                }
                Dictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    { "demolishedPrompts", + 1 },
                };
                AnalyticsService.Instance.CustomData("countDemolishedPrompts", parameters);
                prompt.GetComponent<Prompt>().DestroyPrompt();
                selectedPrompt = 0;
                demolishMode = false;
                if (randomEvents.counterClockedTurns)
                    gameManager.DecreaseTurn();
                else
                    gameManager.IncreaseTurn();
            }
        }
    }
}
