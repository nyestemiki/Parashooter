using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class GameManager : Singleton<GameManager> {
    [SerializeField] private float maxCameraHeight;
    [SerializeField] private GameObject ground;
    [SerializeField] private GameObject player;
    [SerializeField] private Text displayDistanceLeftWindow;
    private float currentHeight;
    private float mass; 
    private float drag; 
    private float angularDrag;
    private Rigidbody rigidbody;

    void Start() {
        mass = 10f;
        drag = 0f;
        angularDrag = 0.05f;

        rigidbody = player.GetComponent<Rigidbody>();
        
        rigidbody.mass = mass;
        rigidbody.drag = drag;
        rigidbody.angularDrag = angularDrag;
    }

    void Update() {
        // rigidbody.mass = mass;
        // rigidbody.drag = drag;
        // rigidbody.angularDrag = angularDrag;
        currentHeight = Mathf.Abs(transform.position.y - ground.transform.position.y); 
        displayDistanceLeftWindow.text = currentHeight.ToString("0");

        if (Input.GetMouseButtonDown(0)) {
            rigidbody.isKinematic = true;
        }
    }
}
