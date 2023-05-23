using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private float targetRot;
    [SerializeField] private float rotSpeed;

    //private bool _isDay = true;

    [SerializeField] private Light sunLight;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        targetRot = 0f;
    }

    //Each turn takes place during night time, whereas the scoreboard is displayed during day time
   void Update()
    {
        if (gameManager.round <= gameManager.maxRounds)
        {
            if (gameManager.turn == 0)
            {
                targetRot = 20f;
            }
            else if (gameManager.turn >= 1 && gameManager.turn <= 4)
            {
                targetRot = -60;
            }
            else if (gameManager.turn > 4)
            {
                targetRot = 0f;
            }

            Quaternion targetQuaternion = Quaternion.Euler(targetRot, 0f, 0f);
            sunLight.transform.rotation = Quaternion.Lerp(sunLight.transform.rotation, 
                targetQuaternion, rotSpeed * Time.deltaTime);
        }
    }
}
