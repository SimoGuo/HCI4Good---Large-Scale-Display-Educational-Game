using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    //A list containing all player characters for the purpose of tracking who is selected
    public static List<PlayerController> playerList = new List<PlayerController>();
    [SerializeField]
    private Material material;

    private bool selected;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        playerList.Add(this);
        agent = GetComponent<NavMeshAgent>();
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
                agent.SetDestination(hit.point);
            }
        }
    }

    //This method highlight the player when it is selected through a mouse click and deselect all other player
    private void OnMouseDown()
    {
        selected = true;
        material.color = Color.green;

        foreach (PlayerController player in playerList)
        {
            if (player != this)
            {
                player.selected = false;
                material.color = Color.white;
            }
        }
    }

    //This method highlight the player when it is selected through a user touch and deselect all other player
    private void OnTouchDown()
    {

    }
}
