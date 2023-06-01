using UnityEngine;

public class RandomWalk : MonoBehaviour
{
    public LayerMask walkableLayer; // The layer on which the NPC should walk
    public float movementSpeed = 1f;
    public float changeDirectionInterval = 2f;

    private float timer;
    private Vector3 currentDirection;

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
        float z = Random.Range(-1f, 1f);
        currentDirection = new Vector3(x, 0f, z).normalized;
    }

    private void Move()
    {
        Vector3 newPosition = transform.position + currentDirection * movementSpeed * Time.deltaTime;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, currentDirection, out hit, movementSpeed * Time.deltaTime))
        {
            if (hit.collider.gameObject.layer == walkableLayer)
            {
                // NPC can walk on the walkableLayer
                transform.position = newPosition;
            }
            else
            {
                ChooseNewDirection();
            }
        }
        else
        {
            ChooseNewDirection();
        }
    }
}
