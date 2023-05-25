using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    public void Move(Vector3 position) {
        transform.position = position;
    }
}
