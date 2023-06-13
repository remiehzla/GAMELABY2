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

    private bool isWinning;
    private bool isLosing;

    [SerializeField] private Image panel;
    [SerializeField] private Text socialPointText;
    [SerializeField] private Text naturePointText;
    [SerializeField] private Text economyPointText;
    [SerializeField] private Text totalPointText;

    [SerializeField] private List<PointCounter> opponents = new List<PointCounter>();

    [SerializeField] private Color32 UIColor;
    [SerializeField] private Color32 winUIColor;


    [SerializeField] private GameObject heartParticles;
    [SerializeField] private GameObject rainParticles;
    [SerializeField] private GameObject confettiParticles;


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

        // Compare if player has more points or less than all of its opponents

        if (totalPoints >= opponents[0].totalPoints && totalPoints >=
            opponents[1].totalPoints && totalPoints >= opponents[2].totalPoints)
        {
            isWinning = true;
        }
        else
        {
            isWinning = false;
        }

        if (totalPoints < opponents[0].totalPoints && totalPoints <
            opponents[1].totalPoints && totalPoints < opponents[2].totalPoints)
        {
            isLosing = true;
        }
        else
        {
            isLosing = false;
        }

        // Enable UI and particles based on if a player is winning or losing

        if (isWinning)
        {
            if (gameManager.round > gameManager.maxRounds)
            {
                panel.color = winUIColor;
                confettiParticles.SetActive(true);
            }
            else
            {
                panel.color = UIColor;
            }
        }

        // Toggle particles

        if (isWinning && gameManager.turn == 0)
        { 
            heartParticles.SetActive(true);
        }
        else
        {
            heartParticles.SetActive(false);
        }

        if (isLosing && gameManager.turn == 0)
        {
            rainParticles.SetActive(true);
        }
        else
        {
            rainParticles.SetActive(false);
        }
    

    }
}
