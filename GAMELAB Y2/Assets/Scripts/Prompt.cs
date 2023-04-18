using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prompt : MonoBehaviour
{
    public int ownedByPlayer;

    public int neededMoney;
    public int neededManpower;
    public int neededRounds;

    public bool built;
    private int placedInRound;

    [SerializeField] private GameObject constructionIcon;

    public int addedSocialPoints;
    public int addedNaturePoints;
    public int addedEconomyPoints;

    private GameManager gameManager;



    void Start()
    {
        // Check if there are enough resources to build the prompt, if not destroy it

        gameManager = FindObjectOfType<GameManager>();

        if (gameManager.money > neededMoney && gameManager.manpower > neededManpower)
        {
            gameManager.money = gameManager.money -= neededMoney;
            gameManager.manpower = gameManager.manpower -= neededManpower;
            placedInRound = gameManager.round;
        }
        else
        {
            DestroyPrompt();
        }
    }

    void Update()
    {
        // Once enough rounds have passed, build the prompt

        if (gameManager.round >= placedInRound + neededRounds && !built)
        {
            BuildPrompt();
        }
    }

    void BuildPrompt()
    {
        // Change prompt status to being built

        built = true;
        constructionIcon.SetActive(false);
    }

    public void DestroyPrompt()
    {
        Destroy(gameObject);
    }
}
