using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prompt : MonoBehaviour
{
    public string promptName;

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

        //promptName = gameObject.name;

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

        if (built)
        {
            constructionIcon.SetActive(false);
        }
        else
        {
            constructionIcon.SetActive(true);
        }
    }

    void BuildPrompt()
    {
        // Change prompt status to being built

        built = true;
    }

    public void DestroyPrompt()
    {
        Destroy(gameObject);
    }
}
