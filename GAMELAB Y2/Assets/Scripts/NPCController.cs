using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public float movementSpeed;
    public float returnTime; // Time in seconds before NPC returns to the target destination
    public Transform targetDestination; // The target destination to return to

    private NavMeshAgent navMeshAgent;
    private Vector3 initialPosition;
    [SerializeField] private float timer;
    [SerializeField] private bool returning;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = movementSpeed;

        // Store the initial position as the target destination
        initialPosition = transform.position;
        SetRandomDestination();
    }

    private void SetRandomDestination()
    {
        Vector3 randomPoint = GetRandomPointOnNavMesh();
        navMeshAgent.SetDestination(randomPoint);
    }

    // Move towards a random direction on the map
    private Vector3 GetRandomPointOnNavMesh()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();
        int randomIndex = Random.Range(0, navMeshData.indices.Length / 3);
        Vector3 randomPoint = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[randomIndex * 3 + 0]], 
            navMeshData.vertices[navMeshData.indices[randomIndex * 3 + 1]], Random.value);
        randomPoint = Vector3.Lerp(randomPoint, navMeshData.vertices[navMeshData.indices[randomIndex * 3 + 2]], Random.value);
        return randomPoint;
    }

    private void Update()
    {
        if (returning)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                returning = false;
/*              navMeshAgent.SetDestination(targetDestination.position);
*/             //navMeshAgent.SetDestination(initialPosition);

            }
        }
        else
        {
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
            {
                timer = returnTime;
                returning = true;
/*              navMeshAgent.SetDestination(initialPosition);
*/              navMeshAgent.SetDestination(targetDestination.position);

            }
        }
    }
}
