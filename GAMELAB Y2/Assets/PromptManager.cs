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
        promptCounter.text = "Prompt selected: " + selectedPrompt;
    }

    public void ChangePromptNumber()
    {
        if (selectedPrompt < prompts.Count - 1)
        {
            selectedPrompt += 1;
        }
        else
        {
            selectedPrompt = 0;
        }
    }

    public void BuildPrompt(Transform location)
    {
        GameObject prompt = Instantiate(prompts[selectedPrompt], location.position, Quaternion.identity);
        prompt.transform.parent = location;
        location.GetComponent<Tile>().prompt = prompt;
    }
}
