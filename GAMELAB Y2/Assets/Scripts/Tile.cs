using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool hasPrompt;
    public GameObject prompt;

    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material selectedMaterial;
    
    private MeshRenderer meshRenderer;
    private PromptManager promptManager;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        promptManager = FindObjectOfType<PromptManager>();
    }

    private void Update()
    {
        if (prompt == null)
        {
            hasPrompt = false;
        }
    }

    private void OnMouseEnter()
    {
        // Update tile color when mouse hovers over

        meshRenderer.material = selectedMaterial;
    }

    private void OnMouseExit()
    {
        // Update tile color when mouse doesn't hover over

        meshRenderer.material = defaultMaterial;
    }
    private void OnMouseDown()
    {
        // If tile is empty, place the selected prompt. If not empty, demolish the prompt.

        if (!hasPrompt)
        {
            if (promptManager.selectedPrompt != 0)
            {
                hasPrompt = true;
                promptManager.PlacePrompt(transform);
            }
            else
            {
                hasPrompt = false;
            }
        }
        else
        {
            hasPrompt = false;
            promptManager.DemolishPrompt(prompt);
        }
    }
}
