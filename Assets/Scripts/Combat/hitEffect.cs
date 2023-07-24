using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem hitEffectSystem = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            getHitEffect();
        }
    }

    public void getHitEffect()
    {
        hitEffectSystem.Play();
    }
}
