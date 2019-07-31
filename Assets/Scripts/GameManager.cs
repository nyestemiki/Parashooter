using System;
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
    [SerializeField] private GameObject lostScreen;
    [SerializeField] private float currentHeight;
    private Rigidbody rigidbody;
    private float hideBorder;
    private bool isStarted = false;

    void Awake() {
        lostScreen.SetActive(false);
    }

    void Start() {
        rigidbody = player.GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        
        rigidbody.mass = GetRandomMass();
        rigidbody.drag = GetRandomDrag();
        rigidbody.angularDrag = GetRandomAngularDrag();

        hideBorder = GetRandomHideDistanceDisplay();

        currentHeight = Mathf.Abs(transform.position.y - ground.transform.position.y); 
    }

    void Update() {
        if (!isStarted) {
            return;
        }

        currentHeight = Mathf.Abs(transform.position.y - ground.transform.position.y);

        if (currentHeight > hideBorder) {
            displayDistanceLeftWindow.text = currentHeight.ToString("0");
        } else {
            displayDistanceLeftWindow.text = "";
        }  

        if (Input.GetMouseButtonDown(0)) {
            ActivateParashoot();
        }

        if (currentHeight < 2) {
            Lost();
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

    private float GetRandomMass() {
        return (new System.Random()).Next(50, 150);
    }

    private float GetRandomDrag() {
        return (new System.Random()).Next(0, 1);
    }

    private float GetRandomAngularDrag() {
        return (new System.Random()).Next(0, 1);
    }

    private float GetRandomHideDistanceDisplay() {
        return (new System.Random()).Next(100, 500);
    }

    private void Lost() {
        lostScreen.SetActive(true);
    }
}
