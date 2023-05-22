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

    public int promptChoice1;
    public int promptChoice2;
    public int promptChoice3;

    [SerializeField] private GameObject promptUI;

    [SerializeField] private Text promptChoice1TextName;
    [SerializeField] private Text promptChoice1TextMoney;
    [SerializeField] private Text promptChoice1TextManpower;
    [SerializeField] private Text promptChoice1TextRounds;
    [SerializeField] private Text promptChoice2TextName;
    [SerializeField] private Text promptChoice2TextMoney;
    [SerializeField] private Text promptChoice2TextManpower;
    [SerializeField] private Text promptChoice2TextRounds;
    [SerializeField] private Text promptChoice3TextName;
    [SerializeField] private Text promptChoice3TextMoney;
    [SerializeField] private Text promptChoice3TextManpower;
    [SerializeField] private Text promptChoice3TextRounds;

    private GameManager gameManager;


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
        // Update the UI

        //promptChoice1Text.text = promptChoice1.ToString();
        //promptChoice2Text.text = promptChoice2.ToString();
        //promptChoice3Text.text = promptChoice3.ToString();
    }

    public void RandomizePrompts()
    {
        promptUI.SetActive(true);

        for(int i = Random.RandomRange(1, prompts.Count); i <= prompts.Count; i++)
        {
            string promptNameT1 = prompts[i].GetComponent<Prompt>().promptName;
            string neededMoneyT1 = prompts[i].GetComponent<Prompt>().neededMoney.ToString();
            string neededManpowerT1 = prompts[i].GetComponent<Prompt>().neededManpower.ToString();
            string neededRoundsT1 = prompts[i].GetComponent<Prompt>().neededRounds.ToString();
            promptChoice1 = i;
            promptChoice1TextName.text = promptNameT1;
            promptChoice1TextMoney.text = "Money needed: " + neededMoneyT1;
            promptChoice1TextManpower.text = "Manpower needed: " + neededManpowerT1;
            promptChoice1TextRounds.text = "Rounds needed: " + neededRoundsT1;
            break;
        }
        for (int i = Random.RandomRange(1, prompts.Count); i <= prompts.Count; i++)
        {
            string promptNameT2 = prompts[i].GetComponent<Prompt>().promptName;
            string neededMoneyT2 = prompts[i].GetComponent<Prompt>().neededMoney.ToString();
            string neededManpowerT2 = prompts[i].GetComponent<Prompt>().neededManpower.ToString();
            string neededRoundsT2 = prompts[i].GetComponent<Prompt>().neededRounds.ToString();
            promptChoice2 = i;
            promptChoice2TextName.text = promptNameT2;
            promptChoice2TextMoney.text = "Money needed: " + neededMoneyT2;
            promptChoice2TextManpower.text = "Manpower needed: " + neededManpowerT2;
            promptChoice2TextRounds.text = "Rounds needed: " + neededRoundsT2;
            break;
        }
        for (int i = Random.RandomRange(1, prompts.Count); i <= prompts.Count; i++)
        {
            string promptNameT3 = prompts[i].GetComponent<Prompt>().promptName;
            string neededMoneyT3 = prompts[i].GetComponent<Prompt>().neededMoney.ToString();
            string neededManpowerT3 = prompts[i].GetComponent<Prompt>().neededManpower.ToString();
            string neededRoundsT3 = prompts[i].GetComponent<Prompt>().neededRounds.ToString();
            promptChoice3 = i;
            promptChoice3TextName.text = promptNameT3;
            promptChoice3TextMoney.text = "Money needed: " + neededMoneyT3;
            promptChoice3TextManpower.text = "Manpower needed: " + neededManpowerT3;
            promptChoice3TextRounds.text = "Rounds needed: " + neededRoundsT3;
            break;
        }
        //promptChoice1 = Random.Range(1, prompts.Count);
        //promptChoice2 = Random.Range(1, prompts.Count);
        //promptChoice3 = Random.Range(1, prompts.Count);
    }

    public void ChoosePrompt1()
    {
        selectedPrompt = promptChoice1;
        promptUI.SetActive(false);
    }
    public void ChoosePrompt2()
    {
        selectedPrompt = promptChoice2;
        promptUI.SetActive(false);
    }
    public void ChoosePrompt3()
    {
        selectedPrompt = promptChoice3;
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

        if (!promptUI.active)
        {
            GameObject prompt = Instantiate(prompts[selectedPrompt], location.position, Quaternion.identity);
            prompt.transform.parent = location;
            location.GetComponent<Tile>().prompt = prompt;
            location.GetComponent<Tile>().hasPrompt = true;
            selectedPrompt = 0;
            demolishMode = false;
            gameManager.IncreaseTurn();
        }
    }

    public void DemolishPrompt(GameObject prompt)
    {
        // Demolish the prompt selected by the tile that called the function

        if (!promptUI.active)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "demolishedPrompts", + 1 },
            };
            AnalyticsService.Instance.CustomData("countDemolishedPrompts", parameters);
            prompt.GetComponent<Prompt>().DestroyPrompt();
            selectedPrompt = 0;
            demolishMode = false;
            gameManager.IncreaseTurn();
        }
    }
}
