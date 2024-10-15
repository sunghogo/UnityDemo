using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; set; }
    public bool isStarted;
    
    // Start is called before the first frame update
    void Start()
    {
        isStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
