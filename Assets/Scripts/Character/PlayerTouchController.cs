using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchController : MonoBehaviour {
    [SerializeField] private float speed = 5;
    private Rigidbody rb;
    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        if (Input.touchCount > 0) {
            Ray r = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            
            if (Physics.Raycast(r, out RaycastHit hit)) {
                if (hit.collider.CompareTag("Ground")) {
                    transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
                    rb.AddForce((new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position).normalized * speed, ForceMode.Acceleration);
                }
            }
        }

        if (Input.GetMouseButton(0)) {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(r, out RaycastHit hit)) {
                if (hit.collider.CompareTag("Ground")) {
                    transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
                    rb.AddForce((new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position).normalized * speed, ForceMode.Acceleration);
                }
            }
        }
    }
}
