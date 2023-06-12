using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int ownedByPlayer;

    private bool hasGivenPoints;
    public bool hasPrompt;

    public GameObject prompt;
    private Prompt promptClass;
    public PointCounter pointCounter;

    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material selectedMaterial;
    
    private MeshRenderer meshRenderer;
    private PromptManager promptManager;
    private GameManager gameManager;

    private void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
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
        else
        {
            promptClass = prompt.GetComponent<Prompt>();
        }

        // Add the points gained from the prompt once every round on turn 0

        if (hasPrompt && promptClass.built)
        {
            if (gameManager.turn == 0 && !hasGivenPoints)
            {
                hasGivenPoints = true;
                pointCounter.socialPoints = pointCounter.socialPoints += promptClass.addedSocialPoints;
                pointCounter.naturePoints = pointCounter.naturePoints += promptClass.addedNaturePoints;
                pointCounter.economyPoints = pointCounter.economyPoints += promptClass.addedEconomyPoints;
                pointCounter.totalPoints = pointCounter.totalPoints += promptClass.addedSocialPoints += 
                    prompt.GetComponent<Prompt>().addedNaturePoints += promptClass.addedEconomyPoints;
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
                promptManager.PlacePrompt(transform);
                prompt.GetComponent<Prompt>().ownedByPlayer = ownedByPlayer;
            }
            else if (promptManager.demolishMode)
            {
                promptManager.DemolishPrompt(prompt);
            }
        }
    }
}
