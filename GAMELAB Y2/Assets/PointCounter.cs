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

    [SerializeField] private Text socialPointText;
    [SerializeField] private Text naturePointText;
    [SerializeField] private Text economyPointText;

    void Start()
    {
        
    }

    void Update()
    {
        // Update the UI

        socialPointText.text = "Social: " + socialPoints.ToString();
        naturePointText.text = "Nature: " + naturePoints.ToString();
        economyPointText.text = "Economy: " + economyPoints.ToString();
    }
}
