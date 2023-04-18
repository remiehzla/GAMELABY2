using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int playerCount;

    public int money;
    public int manpower;
    public int round;
    public int turn;

    [SerializeField] private Transform cameraTransform;

    [SerializeField] private Transform cameraFocusDay;
    [SerializeField] private Transform cameraFocusP1;
    [SerializeField] private Transform cameraFocusP2;
    [SerializeField] private Transform cameraFocusP3;
    [SerializeField] private Transform cameraFocusP4;

    [SerializeField] private Animator fadeScreen;

    [SerializeField] private GameObject pointDisplay;

    [SerializeField] private Text moneyCounter;
    [SerializeField] private Text manpowerCounter;
    [SerializeField] private Text roundCounter;
    [SerializeField] private Text turnCounter;

    private PromptManager promptManager;

    private bool isFading;

    void Start()
    {
        promptManager = FindObjectOfType<PromptManager>();
    }

    void Update()
    {
        // Update the UI

        moneyCounter.text = "Money: " + money.ToString();
        manpowerCounter.text = "Manpower: " + manpower.ToString();
        roundCounter.text = "Round: " + round.ToString();
        turnCounter.text = "Turn: " + turn.ToString();
    }

    public void IncreaseTurn()
    {
        // Move to the next player and move to the next round once everyone had their turn
        if (!isFading)
        {
            isFading = true;
            if (turn < playerCount)
            {
                turn += 1;
            }
            else
            {
                turn = 0;
                IncreaseRound();
            }
            Invoke("FadeScreen", 1);
            Invoke("TransferTurn", 2);
            
        }
    }

    void FadeScreen()
    {
        fadeScreen.SetTrigger("Fade");
    }

    void TransferTurn()
    {
        // Move the camera && reroll UI tiles when turn switches

        isFading = false;
        switch (turn)
        {
            case 0:
                cameraTransform.position = cameraFocusDay.position;
                pointDisplay.SetActive(true);
                break;
            case 1:
                cameraTransform.position = cameraFocusP1.position;
                promptManager.RandomizePrompts();
                pointDisplay.SetActive(false);
                break;
            case 2:
                cameraTransform.position = cameraFocusP2.position;
                promptManager.RandomizePrompts();
                pointDisplay.SetActive(false);
                break;
            case 3:
                cameraTransform.position = cameraFocusP3.position;
                promptManager.RandomizePrompts();
                pointDisplay.SetActive(false);
                break;
            case 4:
                cameraTransform.position = cameraFocusP4.position;
                promptManager.RandomizePrompts();
                pointDisplay.SetActive(false);
                break;
        }
    }

    void IncreaseRound()
    {
        // I hope that if you read this you can do the math here yourself

        round += 1;
    }
}
