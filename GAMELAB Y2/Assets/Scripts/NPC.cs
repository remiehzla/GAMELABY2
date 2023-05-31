using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class NPC : MonoBehaviour
{
    // Will spawn only during turn 0
    // Will walk around the map, over tiles only
    // Will display pop-ups/emotes
    // Tile layer (6)

    [SerializeField] private float movementSpeed;

    private void FixedUpdate()
    {
        int layerMask = 1 << 8;
        
        RaycastHit hit;
    }*/
public class NPC : MonoBehaviour
{
    public LayerMask layerMask; // The layer on which the NPC should walk
    public float movementSpeed = 1f;
    public float changeDirectionInterval = 2f;

    private float timer;
    private Vector2 currentDirection;

    private void Start()
    {
        timer = changeDirectionInterval;
        ChooseNewDirection();
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            ChooseNewDirection();
            timer = changeDirectionInterval;
        }

        Move();
    }

    private void ChooseNewDirection()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        currentDirection = new Vector2(x, y).normalized;
    }

    private void Move()
    {
        Vector2 newPosition = (Vector2)transform.position + (currentDirection * movementSpeed * Time.deltaTime);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, currentDirection, movementSpeed * Time.deltaTime, layerMask);
        if (hit.collider == null)
        {
            transform.position = newPosition;
        }
        else
        {
            ChooseNewDirection();
        }
    }
}



