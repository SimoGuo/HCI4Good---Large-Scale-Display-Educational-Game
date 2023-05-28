using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 position) {
        rb.isKinematic = true;
        transform.position = position;
        rb.isKinematic = false;
    }
}
