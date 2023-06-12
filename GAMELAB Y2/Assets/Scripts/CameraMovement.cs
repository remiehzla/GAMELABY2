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
    [SerializeField] private float delayBeforeShake;
    [SerializeField] private float maxDistance;

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
        if (gameManager.turn == 0)
        {
            animator.SetBool("ZoomedIn", false);
        }
        else
        {
            animator.SetBool("ZoomedIn", true);
        }        
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
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsed = 0f;

        // Wait for a delay before capturing the original position
        yield return new WaitForSeconds(delayBeforeShake);

        Vector3 originalPos = transform.localPosition;

        while (elapsed < duration)
        {
            // Calculate the shake amount using a Perlin noise function
            float x = Mathf.PerlinNoise(Time.time * magnitude, 0f) * 2f - 1f;
            float y = Mathf.PerlinNoise(0f, Time.time * magnitude) * 2f - 1f;

            // Apply the shake offset to the camera's position
            Vector3 shakeOffset = new Vector3(x, y, 0f) * magnitude;
            transform.localPosition = originalPos + Vector3.ClampMagnitude(shakeOffset, maxDistance);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Reset the camera's position
        transform.localPosition = originalPos;
    }

}
