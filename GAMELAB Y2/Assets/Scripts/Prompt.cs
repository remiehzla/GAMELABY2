using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prompt : MonoBehaviour
{
    public int neededMoney;
    public int neededManpower;
    public int neededRounds;

    private bool built;
    private int placedInRound;

    public int socialPoints;
    public int environmentPoints;
    public int economyPoints;

    [SerializeField] private GameObject mesh;



    void Start()
    {
        // Check if there are enough resources to build the prompt, if not destroy it

        if (GameManager.money > neededMoney && GameManager.manpower > neededManpower)
        {
            GameManager.money = GameManager.money -= neededMoney;
            GameManager.manpower = GameManager.manpower -= neededManpower;
            placedInRound = GameManager.round;
            //mesh.SetActive(false);
        }
        else
        {
            DestroyPrompt();
        }
    }

    void Update()
    {
        // Once enough rounds have passed, build the prompt

        if (GameManager.round >= placedInRound + neededRounds && !built)
        {
            BuildPrompt();
        }
    }

    void BuildPrompt()
    {
        built = true;
        //mesh.SetActive(true);
    }

    public void DestroyPrompt()
    {
        Destroy(gameObject);
    }
}
