using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerSettings : MonoBehaviour
{
    private int playerSelecting = 1;

    [SerializeField] private bool allowSelectingInScene;

    public List<Sprite> icons = new List<Sprite>();

    public static string playerName1;
    public static string playerName2;
    public static string playerName3;
    public static string playerName4;

    public static int iconNumber1;
    public static int iconNumber2;
    public static int iconNumber3;
    public static int iconNumber4;

    [SerializeField] private InputField playerNameInput;
    [SerializeField] private Text playerNameInputPlaceholder;
    [SerializeField] private Image iconImage;
    private int iconSelected = 1;

    [SerializeField] private List<Text> playerNameText = new List<Text>();

    [SerializeField] private Text playerNameTextGameplay;
    [SerializeField] private Image iconImageGameplay;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        GameManager.playerCount = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (allowSelectingInScene && Input.GetButtonDown("Submit"))
        {
            NextPlayer();
        }

        switch (playerSelecting)
        {
            case 1:
                {
                    playerName1 = playerNameInput.text;
                    iconNumber1 = iconSelected;
                    break;
                }
            case 2:
                {
                    playerName2 = playerNameInput.text;
                    iconNumber2 = iconSelected;
                    break;
                }
            case 3:
                {
                    playerName3 = playerNameInput.text;
                    iconNumber3 = iconSelected;
                    break;
                }
            case 4:
                {
                    playerName4 = playerNameInput.text;
                    iconNumber4 = iconSelected;
                    break;
                }
        }

        iconImage.sprite = icons[iconSelected - 1];
    }

    private void LateUpdate()
    {
        if (!allowSelectingInScene)
        {
            switch (gameManager.turn)
            {
                case 0:
                    {
                        playerNameTextGameplay.text = "Unknown";
                        iconImageGameplay.sprite = null;
                        iconImageGameplay.enabled = false;
                        break;
                    }
                case 1:
                    {
                        playerNameTextGameplay.text = playerName1;
                        iconImageGameplay.sprite = icons[iconNumber1];
                        iconImageGameplay.enabled = true;
                        break;
                    }
                case 2:
                    {
                        playerNameTextGameplay.text = playerName2;
                        iconImageGameplay.sprite = icons[iconNumber2];
                        iconImageGameplay.enabled = true;
                        break;
                    }
                case 3:
                    {
                        playerNameTextGameplay.text = playerName3;
                        iconImageGameplay.sprite = icons[iconNumber3];
                        iconImageGameplay.enabled = true;
                        break;
                    }
                case 4:
                    {
                        playerNameTextGameplay.text = playerName4;
                        iconImageGameplay.sprite = icons[iconNumber4];
                        iconImageGameplay.enabled = true;
                        break;
                    }
            }
        }
        else
        {
            playerNameText[0].text = "Player 1: " + playerName1;
            playerNameText[1].text = "Player 2: " + playerName2;
            playerNameText[2].text = "Player 3: " + playerName3;
            playerNameText[3].text = "Player 4: " + playerName4;
        }
    }

    void NextPlayer()
    {
        if (playerSelecting < GameManager.playerCount)
        {
            playerSelecting += 1;
            playerNameInput.text = "";
            playerNameInputPlaceholder.text = "Player " + playerSelecting;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void IncreaseIcon()
    {
        if (iconSelected < icons.Count)
        {
            iconSelected += 1;
        }
        else
        {
            iconSelected = 1;
        }
    }
    public void DecreaseIcon()
    {
        if (iconSelected > 1)
        {
            iconSelected -= 1;
        }
        else
        {
            iconSelected = icons.Count;
        }
    }
}
