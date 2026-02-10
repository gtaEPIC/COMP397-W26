using UnityEngine;
using KBCore.Refs;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCMovement : MonoBehaviour
{
    [SerializeField, Self] private NavMeshAgent agent;
    [SerializeField] private GameObject[] waypoints;
    private Vector3 destination;
    private int index;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        if (waypoints.Length == 0) return;
        agent.destination = destination = waypoints[index].transform.position;
    }

    void OnValidate() => this.ValidateRefs();

    // Update is called once per frame
    void Update()
    {
        if (waypoints.Length == 0) return;
        if (Vector3.Distance(transform.position, destination) < 1f)
        {
            index = (index + 1) % waypoints.Length;
            destination = waypoints[index].transform.position;
            agent.destination = destination;
        }
    }
}
