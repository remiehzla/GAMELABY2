using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int playerCount;

    public static int money = 500;
    public static int manpower = 100;
    public static int round = 0;
    public int turn;

    public List<int> socialPointsPerPlayer;
    public List<int> environmentPointsPerPlayer;
    public List<int> economyPointsPerPlayer;

    [SerializeField] private Text moneyCounter;
    [SerializeField] private Text manpowerCounter;
    [SerializeField] private Text roundCounter;
    [SerializeField] private Text turnCounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Update the UI

        moneyCounter.text = "Money: " + money.ToString();
        manpowerCounter.text = "Manpower: " + manpower.ToString();
        roundCounter.text = "Round: " + round.ToString();
        turnCounter.text = "Turn: " + turn.ToString();
    }
}
