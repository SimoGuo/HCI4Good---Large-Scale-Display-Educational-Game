using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeCombat : MonoBehaviour
{
    // Start is called before the first frame update
   //public Animator animator;
   public List<Collider> detectedColliders = new List<Collider>(); // lit of detected enemies
   Collider col;
   private void Awake(){
    col = GetComponent<Collider>(); // use Collider Object
   }

    private void OnTriggerEnter(Collider collision) // When the enemy enters the zone it triggers
    {
        detectedColliders.Add(collision); // Adds the number of enemies detected
        
    }

    private void OnTriggerExit(Collider collision)
    {
        detectedColliders.Remove(collision); // Removes the enemy from the list if the range becomes far from the player
    }

    
      
}
