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

    void Update()
    {
        // Update the UI

        //promptChoiceText[0].text = promptChoice[0].ToString();
        //promptChoiceText[1].text = promptChoice[1].ToString();
        //promptChoiceText[2].text = promptChoice[2].ToString();
    }

    public void RandomizePrompts()
    {
        promptUI.SetActive(true);

        // Choose tile card screen

        for (int i = Random.Range(1, prompts.Count); i <= prompts.Count; i++)
        {
            string promptNameT1 = prompts[i].GetComponent<Prompt>().promptName;
            string neededMoneyT1 = prompts[i].GetComponent<Prompt>().neededMoney.ToString();
            string neededManpowerT1 = prompts[i].GetComponent<Prompt>().neededManpower.ToString();
            string neededRoundsT1 = prompts[i].GetComponent<Prompt>().neededRounds.ToString();
            promptChoice[0] = i;
            promptChoiceTextName[0].text = promptNameT1;
            promptChoiceTextMoney[0].text = "Money needed: " + neededMoneyT1;
            promptChoiceTextManpower[0].text = "Manpower needed: " + neededManpowerT1;
            promptChoiceTextRounds[0].text = "Rounds needed: " + neededRoundsT1;
            break;
        }
        for (int i = Random.Range(1, prompts.Count); i <= prompts.Count; i++)
        {
            string promptNameT2 = prompts[i].GetComponent<Prompt>().promptName;
            string neededMoneyT2 = prompts[i].GetComponent<Prompt>().neededMoney.ToString();
            string neededManpowerT2 = prompts[i].GetComponent<Prompt>().neededManpower.ToString();
            string neededRoundsT2 = prompts[i].GetComponent<Prompt>().neededRounds.ToString();
            promptChoice[1] = i;
            promptChoiceTextName[1].text = promptNameT2;
            promptChoiceTextMoney[1].text = "Money needed: " + neededMoneyT2;
            promptChoiceTextManpower[1].text = "Manpower needed: " + neededManpowerT2;
            promptChoiceTextRounds[1].text = "Rounds needed: " + neededRoundsT2;
            break;
        }
        for (int i = Random.Range(1, prompts.Count); i <= prompts.Count; i++)
        {
            string promptNameT3 = prompts[i].GetComponent<Prompt>().promptName;
            string neededMoneyT3 = prompts[i].GetComponent<Prompt>().neededMoney.ToString();
            string neededManpowerT3 = prompts[i].GetComponent<Prompt>().neededManpower.ToString();
            string neededRoundsT3 = prompts[i].GetComponent<Prompt>().neededRounds.ToString();
            promptChoice[2] = i;
            promptChoiceTextName[2].text = promptNameT3;
            promptChoiceTextMoney[2].text = "Money needed: " + neededMoneyT3;
            promptChoiceTextManpower[2].text = "Manpower needed: " + neededManpowerT3;
            promptChoiceTextRounds[2].text = "Rounds needed: " + neededRoundsT3;
            break;
        }

        if (promptChoice[0] == promptChoice[1] || promptChoice[1] == promptChoice[2] || promptChoice[2] == promptChoice[0] 
            || promptChoice[0] == promptChoice[1] && promptChoice[0] == promptChoice[2])
        {
            RandomizePrompts();
        }
    }

    // Chosen prompt placement

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
