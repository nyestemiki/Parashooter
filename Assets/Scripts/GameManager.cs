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
    [SerializeField] private GameObject stoppedScreen;
    [SerializeField] private Text stoppedDisplay;
    [SerializeField] private float currentHeight;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip idle;
    [SerializeField] private AudioClip falling;
    private float startPosition = 39f;
    private Rigidbody rigidbody;
    private float hideBorder;
    private bool isStarted = false;

    void Awake() {
        rigidbody = player.GetComponent<Rigidbody>();
        setInitialHeight();

        startScreen.SetActive(true);

        lostScreen.SetActive(false);
        stoppedScreen.SetActive(false);
    }

    void Start() {
        rigidbody.isKinematic = true;
        randomInitialData();
        setCurrentHeight();
    }

    private void randomInitialData() {
        rigidbody.mass = GetRandomMass();
        rigidbody.drag = GetRandomDrag();
        rigidbody.angularDrag = GetRandomAngularDrag();

        hideBorder = GetRandomHideDistanceDisplay();
    }

    private void setCurrentHeight() {
        currentHeight = Mathf.Abs(transform.position.y - ground.transform.position.y); 
    }

    private void setInitialHeight() {
        player.transform.position = new Vector3(transform.position.x, startPosition, transform.position.z);
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

        if (currentHeight < 50) {
            rigidbody.isKinematic = true;
            Lost();
        }
    }

    public void Play() {
        rigidbody.isKinematic = false;
        isStarted = true;
        audioSource.clip = falling;
        audioSource.Play();
        startScreen.SetActive(false);
    }

    public void Replay() {
        audioSource.clip = idle;
        audioSource.Play();
        isStarted = false;
        startScreen.SetActive(true);
        lostScreen.SetActive(false);
        stoppedScreen.SetActive(false);
        displayDistanceLeftWindow.text = "";
        randomInitialData();
        setInitialHeight();
    }

    public void ActivateParashoot() {
        audioSource.clip = idle;
        audioSource.Play();
        rigidbody.isKinematic = true;
        stoppedScreen.SetActive(true);
        isStarted = false;
        
        if (currentHeight > 300) {
            stoppedDisplay.text = "Too soon";
        } else if (currentHeight > 200) {
            stoppedDisplay.text = "Okay...ish";
        } else if (currentHeight > 100) {
            stoppedDisplay.text = "Good job";
        } else {
            stoppedDisplay.text = "Impressive";
        }
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
        audioSource.clip = idle;
        audioSource.Play();
        isStarted = false;
    }
}
