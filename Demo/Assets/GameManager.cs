
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public bool isStarted {get; private set;}
    
    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        } else {
            Destroy(gameObject); // Destroy any duplicate GameManager instances
        }
    }

    public void StartGame() {
        isStarted = true;
        Debug.Log("Starting Game");
    }
    
    void Start()
    {
        isStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Instance.isStarted);
    }
}
