using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Vector3 initialPosition;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Store the initial position as the target destination
        initialPosition = transform.position;
        SetRandomDestination();
    }

    private void Update()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            SetRandomDestination();
        }
    }

    private void SetRandomDestination()
    {
        Vector3 randomPoint = GetRandomPointOnNavMesh();
        navMeshAgent.SetDestination(randomPoint);
    }

    private Vector3 GetRandomPointOnNavMesh()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();
        int randomIndex = Random.Range(0, navMeshData.indices.Length / 3);
        Vector3 randomPoint = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[randomIndex * 3 + 0]], navMeshData.vertices[navMeshData.indices[randomIndex * 3 + 1]], Random.value);
        randomPoint = Vector3.Lerp(randomPoint, navMeshData.vertices[navMeshData.indices[randomIndex * 3 + 2]], Random.value);
        return randomPoint;
    }
}
