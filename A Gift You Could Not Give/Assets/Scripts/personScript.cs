using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class personScript : MonoBehaviour
{
    private Transform realGoal;
    public Transform waypoints;
    private Transform[] boi;
    NavMeshAgent agent;

    public float sightdist;

    public LineRenderer line;

    void Start()
    {
        boi = waypoints.GetComponentsInChildren<Transform>();
        int choice = Random.Range(1, boi.Length);
        realGoal = boi[choice];
        print(choice);
        agent = GetComponent<NavMeshAgent>();

        agent.destination = realGoal.position;
    }

    private void Update()
    {
        RaycastHit hit1;

        if (Physics.Raycast(transform.position, transform.forward, out hit1, sightdist))
        {
            
            line.SetPosition(0, gameObject.transform.position);
            line.SetPosition(1, hit1.collider.gameObject.transform.position);
            
            if (hit1.collider.gameObject.tag == "Player")
            {
                hit1.collider.gameObject.GetComponent<Player>().Die();
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        Transform tempGoal;
        print(collider);
        if (collider.gameObject.tag == "Waypoint")
        {
            int choice = Random.Range(1, boi.Length);
            print(choice);
            tempGoal = boi[choice];

            while (tempGoal == realGoal)
            {
                choice = Random.Range(1, boi.Length);
                print(choice);
                realGoal = boi[choice];
            }

            realGoal = boi[choice];

            agent.destination = realGoal.position;
        }
    }
}