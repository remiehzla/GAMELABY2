using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptManager : MonoBehaviour
{
    public List<GameObject> prompts = new List<GameObject>();

    public int selectedPrompt;

    [SerializeField] private Text promptCounter;

    void Update()
    {
        // Update the UI

        promptCounter.text = "Prompt selected: " + selectedPrompt;
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

        GameObject prompt = Instantiate(prompts[selectedPrompt], location.position, Quaternion.identity);
        prompt.transform.parent = location;
        location.GetComponent<Tile>().prompt = prompt;
    }

    public void DemolishPrompt(GameObject prompt)
    {
        // Demolish the prompt selected by the tile that called the function

        prompt.GetComponent<Prompt>().DestroyPrompt();
    }
}
