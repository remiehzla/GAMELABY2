using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptManager : MonoBehaviour
{
    public List<GameObject> prompts = new List<GameObject>();

    public int selectedPrompt;
    public bool demolishMode;

    public int promptChoice1;
    public int promptChoice2;
    public int promptChoice3;

    [SerializeField] private GameObject promptUI;

    [SerializeField] private Text promptChoice1Text;
    [SerializeField] private Text promptChoice2Text;
    [SerializeField] private Text promptChoice3Text;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        // Update the UI

        promptChoice1Text.text = promptChoice1.ToString();
        promptChoice2Text.text = promptChoice2.ToString();
        promptChoice3Text.text = promptChoice3.ToString();
    }

    public void RandomizePrompts()
    {
        promptUI.SetActive(true);
        promptChoice1 = Random.Range(1, prompts.Count);
        promptChoice2 = Random.Range(1, prompts.Count);
        promptChoice3 = Random.Range(1, prompts.Count);
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
            prompt.GetComponent<Prompt>().DestroyPrompt();
            selectedPrompt = 0;
            demolishMode = false;
            gameManager.IncreaseTurn();
        }
    }
}
