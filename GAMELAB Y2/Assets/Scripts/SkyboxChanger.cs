using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Material daySkyboxMaterial; // Assign the turn 0 skybox material in the Inspector
    public Material nightSkyboxMaterial; // Assign the other skybox material in the Inspector

    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

    }

    private void Update()
    {
        ChangeSkybox();
    }

    void ChangeSkybox()
    {
        // Check the current turn and assign the corresponding skybox material
        if (gameManager.turn == 0)
        {
            RenderSettings.skybox = daySkyboxMaterial;
        }
        else
        {
            RenderSettings.skybox = nightSkyboxMaterial;
        }
    }
}