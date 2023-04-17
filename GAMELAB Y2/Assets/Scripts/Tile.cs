using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int ownedByPlayer;
    public PointCounter pointCounter;
    private bool hasGivenPoints;
    public bool hasPrompt;
    public GameObject prompt;

    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material selectedMaterial;
    
    private MeshRenderer meshRenderer;
    private PromptManager promptManager;
    private GameManager gameManager;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        promptManager = FindObjectOfType<PromptManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        // If no prompt is detected, allow for a new prompt to be built

        if (prompt == null)
        {
            hasPrompt = false;
        }

        // Add the points gained from the prompt once every round on turn 0

        if (hasPrompt && prompt.GetComponent<Prompt>().built)
        {
            if (gameManager.turn == 0 && !hasGivenPoints)
            {
                hasGivenPoints = true;
                pointCounter.socialPoints = pointCounter.socialPoints += prompt.GetComponent<Prompt>().addedSocialPoints;
                pointCounter.naturePoints = pointCounter.naturePoints += prompt.GetComponent<Prompt>().addedNaturePoints;
                pointCounter.economyPoints = pointCounter.economyPoints += prompt.GetComponent<Prompt>().addedEconomyPoints;
            }
            else if (gameManager.turn > 0)
            {
                hasGivenPoints = false;
            }
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
        
        if (ownedByPlayer == gameManager.turn)
        {
            if (!hasPrompt)
            {
                hasPrompt = true;
                promptManager.PlacePrompt(transform);
                prompt.GetComponent<Prompt>().ownedByPlayer = ownedByPlayer;
            }
            else
            {
                hasPrompt = false;
                promptManager.DemolishPrompt(prompt);
            }
        }
    }
}