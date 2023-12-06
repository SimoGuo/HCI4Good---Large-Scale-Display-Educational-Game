using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalGoalScript : MonoBehaviour
{

    bool hasGroundTouched = false;
    bool hasWindTouched = false;
    bool hasTriggered = false;

    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] GameObject floatingStar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Update is called once per frame
    void Update()
    {
        if (hasGroundTouched && hasWindTouched && !hasTriggered)
        {
            //Triggers the scene transition when both players touch the FloatingStar GameObject
            ContinueToScene("lobby");
            hasGroundTouched = false;
            hasWindTouched = false;
            hasTriggered = true;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Ground"))
        {
            hasGroundTouched = true;
        }

        if (col.CompareTag("Wind"))
        {
            hasWindTouched = true;
        }
    }

    
    private void ContinueToScene(string sceneName)
    {
        
        SceneManager.LoadScene(sceneName);
    }
}
