using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class TestController : MonoBehaviour
{
    //A list containing all player characters for the purpose of tracking who is selected
    public static List<TestController> playerList = new List<TestController>();
    [SerializeField]

    //Boolean variable to determine if a chracter is currently being selected by the player
    private bool selected;

    NavMeshAgent agent;

    public Vector3 walkPoint;
    private Vector3 distanceToWalkPoint;
    public float distance;
    public float stoppingDistance;
    public bool isRunning;

    //Animation 
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        playerList.Add(this);
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.touchCount >=1) && selected)
        {
            RaycastHit hit;

            //On default, use Raycast with Mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //If a touch is sensed, use Touch position 
            if (Input.touchCount == 1)
            {
                ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).rawPosition);
            }
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                walkPoint = hit.point;
                agent.SetDestination(walkPoint);
                isRunning = true;
            }
        }

        //Update the distance from the player to the player selected location
        if (isRunning == true)
        {
            distanceToWalkPoint = transform.position - walkPoint;
            distance = distanceToWalkPoint.magnitude;
        }

        if (stoppingDistance >= distance)
        {
            animator.SetBool("isRunning", false);
            isRunning = false;
        }
        else
        {
            animator.SetBool("isRunning", true);
        }

    }

    //This method highlight the player when it is selected through a mouse click and deselect all other player
    private void OnMouseDown()
    {
        selected = true;

        foreach (TestController player in playerList)
        {
            if (player != this)
            {
                player.selected = false;
            }
        }
    }

    //This method highlight the player when it is selected through a user touch and deselect all other player
    private void OnTouchDown()
    {

    }
}
