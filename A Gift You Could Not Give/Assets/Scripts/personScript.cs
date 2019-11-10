using UnityEngine;
using UnityEngine.AI;

public class personScript : MonoBehaviour
{
    private Transform realGoal;
    public Transform waypoints;
    private Transform[] boi;
    NavMeshAgent agent;

    public float sightdist;

    private int currentGoal;
    public int waypointTot;

    public LineRenderer line;
    public LineRenderer line2;
    public LineRenderer line3;

    private Vector3 line1direction = new Vector3(0, 0, 1);
    private Vector3 line2direction = new Vector3(-0.25f, 0, 1);
    private Vector3 line3direction = new Vector3(0.25f, 0, 1);

    void Start()
    {
        boi = waypoints.GetComponentsInChildren<Transform>();
   
        currentGoal = 1;
        realGoal = boi[currentGoal];
        print(boi[currentGoal]);
        agent = GetComponent<NavMeshAgent>();

        agent.destination = realGoal.position;

        line2direction = Quaternion.AngleAxis(-15, transform.up) * new Vector3(0, 0, 1);
        line3direction = Quaternion.AngleAxis(15, transform.up) * new Vector3(0, 0, 1); 
    }

    private void Update()
    {
        RaycastHit hit1;
        RaycastHit hit2;
        RaycastHit hit3;

        line.SetPosition(0, new Vector3(0, 0, 0));
        line.SetPosition(1, line1direction * sightdist);

        line2.SetPosition(0, new Vector3(0, 0, 0));
        line2.SetPosition(1, line2direction * sightdist);

        line3.SetPosition(0, new Vector3(0, 0, 0));
        line3.SetPosition(1, line3direction * sightdist);

        if (Physics.Raycast(transform.position, transform.forward, out hit1, sightdist))
        {
            //Debug.DrawLine(transform.position, hit1.point, Color.green);
            line.SetPosition(1, line1direction * hit1.distance);

            if (hit1.collider.gameObject.tag == "Player")
            {
                hit1.collider.gameObject.GetComponent<Player>().Die();
            }
        }

        if (Physics.Raycast(transform.position, Quaternion.AngleAxis(-15, transform.up) * transform.forward, out hit2, sightdist))
        {
            //Debug.DrawLine(transform.position, hit2.point, Color.green);
            line2.SetPosition(1, line2direction * hit2.distance);

            if (hit2.collider.gameObject.tag == "Player")
            {
                hit2.collider.gameObject.GetComponent<Player>().Die();
            }
        }

        if (Physics.Raycast(transform.position, Quaternion.AngleAxis(15, transform.up) * transform.forward, out hit3, sightdist))
        {
            //Debug.DrawLine(transform.position, hit3.point, Color.green);
            line3.SetPosition(1, line3direction * hit3.distance);

            if (hit3.collider.gameObject.tag == "Player")
            {
                hit3.collider.gameObject.GetComponent<Player>().Die();
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Waypoint")
        {
            print(currentGoal);
            int newWaypoint;

            currentGoal++;
            print(currentGoal);
            newWaypoint = currentGoal % waypointTot;
            while(newWaypoint == 0)
            {
                currentGoal++;
                newWaypoint = currentGoal % waypointTot;
            }

            realGoal = boi[newWaypoint];

            agent.destination = realGoal.position;
        }
    }
}