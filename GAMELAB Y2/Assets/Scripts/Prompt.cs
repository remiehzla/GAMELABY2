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
    [SerializeField] private GameObject constructionCrane;

    public int addedSocialPoints;
    public int addedNaturePoints;
    public int addedEconomyPoints;

    private GameManager gameManager;
    private ResourceManager resourceManager;



    void Start()
    {
        // Check if there are enough resources to build the prompt, if not destroy it

        //promptName = gameObject.name;

        gameManager = FindObjectOfType<GameManager>();
        resourceManager = FindObjectOfType<ResourceManager>();

        if (gameManager.money >= neededMoney && gameManager.manpower >= neededManpower)
        {
            gameManager.money = gameManager.money -= neededMoney;
            gameManager.manpower = gameManager.manpower -= neededManpower;
            placedInRound = gameManager.round;

            //The bar value goes down
            resourceManager.money = resourceManager.money -= neededMoney;
            resourceManager.manpower = resourceManager.manpower -= neededManpower;
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
            //constructionCrane.SetActive(false);
        }
        else
        {
            constructionIcon.SetActive(true);
            //constructionCrane.SetActive(true);
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
