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

    private void OnMouseEnter()
    {
        meshRenderer.material = selectedMaterial;
    }

    private void OnMouseExit()
    {
        meshRenderer.material = defaultMaterial;
    }
    private void OnMouseDown()
    {
        if (!hasPrompt)
        {
            hasPrompt = true;
            if (promptManager.selectedPrompt != 0)
            {
                promptManager.BuildPrompt(transform);
            }
            else
            {
                hasPrompt = false;
            }
        }
        else
        {
            hasPrompt = false;
            Destroy(prompt);
        }
    }
}
