using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointCounter : MonoBehaviour
{
    public int player;

    public int socialPoints;
    public int naturePoints;
    public int economyPoints;
    public int totalPoints;

    [SerializeField] private Image panel;
    [SerializeField] private Text socialPointText;
    [SerializeField] private Text naturePointText;
    [SerializeField] private Text economyPointText;
    [SerializeField] private Text totalPointText;

    [SerializeField] private PointCounter opponent1;
    [SerializeField] private PointCounter opponent2;
    [SerializeField] private PointCounter opponent3;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        // Update the UI

        socialPointText.text = "Social: " + socialPoints.ToString();
        naturePointText.text = "Nature: " + naturePoints.ToString();
        economyPointText.text = "Economy: " + economyPoints.ToString();
        switch (player)
        {
            case 1:
                {
                    totalPointText.text = PlayerSettings.playerName1 + ": " + totalPoints.ToString();
                    break;
                }
            case 2:
                {
                    totalPointText.text = PlayerSettings.playerName2 + ": " + totalPoints.ToString();
                    break;
                }
            case 3:
                {
                    totalPointText.text = PlayerSettings.playerName3 + ": " + totalPoints.ToString();
                    break;
                }
            case 4:
                {
                    totalPointText.text = PlayerSettings.playerName4 + ": " + totalPoints.ToString();
                    break;
                }
        }

        // Compare points once rounds are over and change UI color for the winner

        if (gameManager.round > gameManager.maxRounds && totalPoints >= opponent1.totalPoints 
            && totalPoints >= opponent2.totalPoints && totalPoints >= opponent3.totalPoints)
        {
            panel.color = new Color32(255, 255, 0, 255);
        }
        else
        {
            panel.color = new Color32(255, 255, 255, 255);
        }

        // Disable counter if player isn't playing

        if (player > GameManager.playerCount)
        {
            panel.enabled = false;
            socialPointText.enabled = false;
            naturePointText.enabled = false;
            economyPointText.enabled = false;
            totalPointText.enabled = false;
        }
    }
}
