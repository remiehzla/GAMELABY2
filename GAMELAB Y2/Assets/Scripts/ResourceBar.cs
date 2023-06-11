using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ResourceBar : MonoBehaviour
{
    private Image fillBar;

    [SerializeField] private bool moneyBool;
    [SerializeField] private bool manpowerBool;
    [SerializeField] private bool roundsBool;

    [SerializeField] private float currentAmount;
    [SerializeField] private float maxAmount;

    ResourceManager resourceManager;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        fillBar = GetComponent<Image>();
        resourceManager = FindObjectOfType<ResourceManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ChooseResource();
        ResourceBarUpdate();
    }

    private void ChooseResource()
    {
        if (moneyBool)
        {
            currentAmount = gameManager.money;
        }
        else if (manpowerBool)
        {
            currentAmount = gameManager.manpower;

        }
        else if (roundsBool)
        {
            currentAmount = gameManager.round;
        }
        else
            Debug.LogError("No resource selected");
    }

    private void ResourceBarUpdate()
    {
        float resourceRatio = currentAmount / maxAmount;
        fillBar.fillAmount = resourceRatio;
    }
}
