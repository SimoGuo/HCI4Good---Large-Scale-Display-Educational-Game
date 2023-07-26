using PlayerCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterGameAnimation : MonoBehaviour
{
    public static EnterGameAnimation Instance;

    
    public GameObject[] interlocutors;
    
    [SerializeField] private Transform interlocutor1Transform;
    
    [SerializeField] private Transform interlocutor2Transform;
    private GameObject player1Clone;
    private GameObject player2Clone;
    private bool isPlayingAnim;

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayingAnim)
        {
            if (Vector3.Distance(player1Clone.transform.position, player2Clone.transform.position) >= 15)
            {
                player1Clone.transform.LookAt(new Vector3(player2Clone.transform.position.x, player1Clone.transform.position.y, player2Clone.transform.position.z));
                player1Clone.transform.Translate(player1Clone.transform.forward * Time.deltaTime * 6f, Space.World);
                player2Clone.transform.LookAt(new Vector3(player1Clone.transform.position.x, player2Clone.transform.position.y, player1Clone.transform.position.z));
                player2Clone.transform.Translate(player2Clone.transform.forward * Time.deltaTime * 6f, Space.World);
                if (player1Clone.GetComponent<Player>())
                {
                    player1Clone.GetComponent<Player>().enabled = false;
                }
                player1Clone.GetComponent<Animator>().SetFloat("Speed",1);
            }
            else
            {
                isPlayingAnim = false;
                player1Clone.GetComponent<Animator>().SetFloat("Speed", 0);
                if (player2Clone.GetComponent<Animator>())
                {
                    player2Clone.GetComponent<Animator>().SetFloat("Speed", 0);
                }
                DialogController.Instance.ShowDialog();
            }
        }
    }

    
    public void InitPlayerAndEnemy(int _interlocutor1,int interlocutor2)
    {
        Destroy(player1Clone);
        Destroy(player2Clone);
        player1Clone = Instantiate(interlocutors[_interlocutor1]);
        player1Clone.transform.localScale = new Vector3(4, 4, 4);
        player1Clone.transform.position = interlocutor1Transform.position;
        GameObject dlgCanvasClone = Instantiate(DialogController.Instance.dlgCanvas,player1Clone.transform);
        player2Clone = Instantiate(interlocutors[interlocutor2]);
        player2Clone.transform.localScale = new Vector3(4, 4, 4);
        player2Clone.transform.position = interlocutor2Transform.position;
        if (interlocutor2 >=1 && interlocutor2<=4)
        {
            player2Clone.GetComponent<Animator>().SetFloat("Speed", 1);
        }
        isPlayingAnim = true;
    }
}
