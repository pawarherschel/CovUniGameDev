using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = System.Diagnostics.Debug;
using Random = UnityEngine.Random;

public class PipeSpawner : MonoBehaviour
{
    public static PipeSpawner Instance { get; private set; }
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
    
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    [SerializeField] private GameObject[] pipes;

    private void Start()
    {
        Debug.Assert(pipes != null, nameof(pipes) + " != null");
    }

    public void SpawnPipe()
    {
        Instantiate(pipes[Random.Range(0, pipes.Length)], this.transform);
    }
}
