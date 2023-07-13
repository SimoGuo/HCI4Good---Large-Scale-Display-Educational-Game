using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeCombat : MonoBehaviour
{
    // Start is called before the first frame update
   //public Animator animator;
   public List<Collider> detectedColliders = new List<Collider>();
   Collider col;
   private void Awake(){
    col = GetComponent<Collider>();
   }

    private void OnTriggerEnter(Collider collision)
    {
        detectedColliders.Add(collision);
        
    }

    private void OnTriggerExit(Collider collision)
    {
        detectedColliders.Remove(collision);
    }

   
}
