using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Vector3 initialPosition;
    public bool returning;

    private GameManager gameManager;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        gameManager = FindObjectOfType<GameManager>();

        // Store the initial position as the target destination
        initialPosition = transform.position;
        SetRandomDestination();
    }

    private void Update()
    {
        Debug.Log(returning);
        if (gameManager.turn != 0)
            returning = true;
        else
            returning = false;

        if (returning)
        {
            navMeshAgent.SetDestination(initialPosition);
        }
        else
        {
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
                SetRandomDestination();

        }
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
        Vector3 randomPoint = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[randomIndex * 3 + 0]], navMeshData.vertices[navMeshData.indices[randomIndex * 3 + 1]], Random.value);
        randomPoint = Vector3.Lerp(randomPoint, navMeshData.vertices[navMeshData.indices[randomIndex * 3 + 2]], Random.value);
        return randomPoint;
    }
}