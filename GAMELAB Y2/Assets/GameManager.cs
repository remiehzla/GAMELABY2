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

    [SerializeField] private Text moneyCounter;
    [SerializeField] private Text manpowerCounter;
    [SerializeField] private Text roundCounter;
    [SerializeField] private Text turnCounter;

    void Start()
    {
        
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

        if (turn < playerCount)
        {
            turn += 1;
        }
        else
        {
            turn = 0;
            IncreaseRound();
        }
    }

    void IncreaseRound()
    {
        // I hope that if you can read this you can do the math here yourself

        round += 1;
    }
}
