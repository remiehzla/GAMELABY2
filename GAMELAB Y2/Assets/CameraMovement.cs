using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    float horizontalInput;
    float verticalInput;

    [SerializeField] private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        MoveCamera();
    }
    
    void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Q))
        {
            gameObject.transform.Rotate(0, 90, 0, Space.World);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            gameObject.transform.Rotate(0, -90, 0, Space.World);
        }
    }

    void MoveCamera()
    {
        gameObject.transform.Translate(horizontalInput / 100f * moveSpeed, verticalInput / 100f * moveSpeed, 0f, Space.Self);
    }

}