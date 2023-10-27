using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 2f;

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

                transform.position += moveDirection * moveSpeed * Time.deltaTime;
            }
        }
    }
}
