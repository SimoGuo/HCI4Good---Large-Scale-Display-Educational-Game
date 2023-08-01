using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Normal Attack")]
public class AttackTemplate : ScriptableObject
{
    public AnimatorOverrideController animatorOveride;
    public float damage;
}
