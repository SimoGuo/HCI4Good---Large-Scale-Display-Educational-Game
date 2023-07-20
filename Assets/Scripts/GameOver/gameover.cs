using System.Collections;
using UnityEngine;
using TMPro;

public class gameover : MonoBehaviour
{
    public TextMeshProUGUI numberText;
    private int countScore = 17; // This is demo. After game finish this value will be update. 
    private int jumpCount = 0;

    void Start()
    {
        StartCoroutine(JumpObjectRoutine());
    }

    void JumpObject()
    {
        if (jumpCount < 6)
        {
            StartCoroutine(JumpRoutine());
            jumpCount++;
        }
    }

    IEnumerator JumpRoutine()
    {
        float originalY = transform.position.y;
        float jumpHeight = 50f;
        float jumpDuration = 0.25f;

        for (int i = 0; i < 5; i++)
        {
            Vector3 jumpPos = new Vector3(transform.position.x, originalY + jumpHeight, transform.position.z);
            transform.position = jumpPos;
            yield return new WaitForSeconds(jumpDuration);

            transform.position = new Vector3(transform.position.x, originalY, transform.position.z);
            yield return new WaitForSeconds(jumpDuration);
        }

        numberText.text = countScore.ToString();
    }

    IEnumerator JumpObjectRoutine()
    {
        yield return new WaitForEndOfFrame();
        JumpObject();
    }
}

