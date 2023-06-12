using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public LayerMask npcLayer; // Specify the layer for NPCs in the Unity Editor

    private GameManager gameManager;
    private List<NPCController> npcControllers = new List<NPCController>();

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        // Find and add NPCs with the specified layer to the array
        FindAndAddNPCs();

        // Activate the NPCs on turn 0
        if (gameManager.turn == 0)
        {
            ActivateNPCs();
        }
    }

    private void Update()
    {
        // If the turn becomes 0, activate the NPCs
        if (gameManager.turn == 0)
        {
            ActivateNPCs();
        }
        else // If the turn is not 0, deactivate the NPCs
        {
            DeactivateNPCs();
        }
    }

    private void FindAndAddNPCs()
    {
        NPCController[] allNPCs = FindObjectsOfType<NPCController>();

        foreach (NPCController npcController in allNPCs)
        {
            // Check if the NPC's layer matches the specified layer
            if ((npcLayer.value & (1 << npcController.gameObject.layer)) > 0)
            {
                npcControllers.Add(npcController);
            }
        }
    }

    private void ActivateNPCs()
    {
        foreach (NPCController npcController in npcControllers)
        {
            npcController.enabled = true; // Enable the NPCController script
            npcController.gameObject.SetActive(true); // Activate the NPC GameObject
        }
    }

    private void DeactivateNPCs()
    {
        foreach (NPCController npcController in npcControllers)
        {
            npcController.enabled = false; // Disable the NPCController script
            npcController.gameObject.SetActive(false); // Deactivate the NPC GameObject
        }
    }
}
