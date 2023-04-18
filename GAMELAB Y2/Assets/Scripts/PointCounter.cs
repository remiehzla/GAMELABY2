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
        totalPointText.text = "Player " + player.ToString() + ": " + totalPoints.ToString();

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
    }
}
