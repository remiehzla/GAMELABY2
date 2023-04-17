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
    public int naturePoints;
    public int economyPoints;

    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private Material unbuiltMat;
    [SerializeField] private Material builtMat;

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
            mesh.material = unbuiltMat;
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
        built = true;
        mesh.material = builtMat;
    }

    public void DestroyPrompt()
    {
        Destroy(gameObject);
    }
}
