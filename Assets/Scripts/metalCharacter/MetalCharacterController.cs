/**
* @author: Simo Guo
* This class is to implement the metal player is able to push
* large cubes.
*/
/*using UnityEngine;

public class MetalCharacterController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float pushForce = 5f;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 targetPosition = hit.point;

                Vector3 moveDirection = targetPosition - transform.position;
                moveDirection.y = 0f;
                moveDirection.Normalize();

                if (moveDirection != Vector3.zero)
                {
                    transform.LookAt(transform.position + moveDirection);
                }

                transform.position += moveDirection * moveSpeed * Time.deltaTime;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cube"))
        {
            
            Vector3 pushDirection = new Vector3(collision.transform.position.x - transform.position.x, 0f, collision.transform.position.z - transform.position.z).normalized;

            
            collision.transform.Translate(pushDirection * pushForce * Time.deltaTime);
        }
    }
}*/
