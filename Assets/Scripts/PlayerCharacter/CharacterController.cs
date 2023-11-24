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

                if (!IsCollidingWithWall(moveDirection))
                {
                    MoveCharacter(moveDirection);
                    RotateCharacter(moveDirection);
                }
            }
        }
    }

    void MoveCharacter(Vector3 moveDirection)
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    void RotateCharacter(Vector3 moveDirection)
    {
        if (moveDirection != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10f);
        }
    }

    bool IsCollidingWithWall(Vector3 moveDirection)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, moveDirection, out hit, 0.5f))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                return true;
            }
        }
        return false;
    }
    
}
