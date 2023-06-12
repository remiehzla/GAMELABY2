using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    float horizontalInput;
    float verticalInput;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;

    [SerializeField] private float duration;
    [SerializeField] private float magnitude;
    [SerializeField] private float elapsed;



    private GameManager gameManager;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Shake();

        if (gameManager.turn == 0)
        {
            animator.SetBool("ZoomedIn", false);
        }
        else
        {
            animator.SetBool("ZoomedIn", true);
        }

        Debug.Log(elapsed);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //GetInput();
        //MoveCamera();
    }
    
    void GetInput()
    {
        //horizontalInput = Input.GetAxisRaw("Horizontal");
        //verticalInput = Input.GetAxisRaw("Vertical");
        //if (Input.GetKeyDown(KeyCode.Q))
        {
            //gameObject.transform.Rotate(0, 90, 0, Space.World);
        }
        //if (Input.GetKeyDown(KeyCode.E))
        {
           //gameObject.transform.Rotate(0, -90, 0, Space.World);
        }
        //if (Input.GetKey(KeyCode.PageDown) && Camera.main.orthographicSize < maxZoom)
        {
            //Camera.main.orthographicSize = Camera.main.orthographicSize * zoomSpeed;
        }
        //if (Input.GetKey(KeyCode.PageUp) && Camera.main.orthographicSize > minZoom)
        {
            //Camera.main.orthographicSize = Camera.main.orthographicSize / zoomSpeed;
        }
    }

    void MoveCamera()
    {
        gameObject.transform.Translate(horizontalInput / 100f * moveSpeed, verticalInput / 100f * moveSpeed, 0f, Space.Self);
    }

    public void Shake()
    {
        Vector3 originalPos = this.gameObject.transform.localPosition;

         //elapsed = 0.0f;

        while (elapsed < duration)
        {
            //float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-10f, 10f) * magnitude;
            //this.gameObject.transform.localPosition = new Vector3(x, y, originalPos.z);
            gameObject.transform.Translate(0, y, 0, Space.Self);

            elapsed += Time.deltaTime;
            break;
        }
        //this.gameObject.transform.localPosition = originalPos;
        gameObject.transform.Translate(0, 0, 0);

    }
}
