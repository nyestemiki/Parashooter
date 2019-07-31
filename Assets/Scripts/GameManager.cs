using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class GameManager : Singleton<GameManager> {
    [SerializeField] private float maxCameraHeight;
    [SerializeField] private GameObject ground;
    [SerializeField] private GameObject player;
    [SerializeField] private Text displayDistanceLeftWindow;
    [SerializeField] private GameObject startScreen;
    private float currentHeight;
    private float mass; 
    private float drag; 
    private float angularDrag;
    private Rigidbody rigidbody;
    private bool isStarted = false;

    void Start() {
        mass = 10f;
        drag = 0f;
        angularDrag = 0.05f;

        rigidbody = player.GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        
        rigidbody.mass = mass;
        rigidbody.drag = drag;
        rigidbody.angularDrag = angularDrag;

        currentHeight = Mathf.Abs(transform.position.y - ground.transform.position.y); 
    }

    void Update() {
        if (!isStarted) {
            return;
        }

        // rigidbody.mass = mass;
        // rigidbody.drag = drag;
        // rigidbody.angularDrag = angularDrag;

        

        if (currentHeight < 200) {
            displayDistanceLeftWindow.text = "";
        } else {
            currentHeight = Mathf.Abs(transform.position.y - ground.transform.position.y); 
            displayDistanceLeftWindow.text = currentHeight.ToString("0");
        }

        if (Input.GetMouseButtonDown(0)) {
            ActivateParashoot();
        }
    }

    public void Play() {
        isStarted = true;
        startScreen.SetActive(false);
        rigidbody.isKinematic = false;
    }

    public void ActivateParashoot() {
        rigidbody.isKinematic = true;
    }
}
