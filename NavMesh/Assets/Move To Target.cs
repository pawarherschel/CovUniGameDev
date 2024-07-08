using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public sealed class MoveToTarget : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent _agent;

    // Start is called before the first frame update
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.destination = target.transform.position;
    }

    private void Update()
    {
        var hasStoppedMoving = Vector3.Magnitude(_agent.velocity) < 0.1;
        if (!hasStoppedMoving) return;
        _agent.destination = target.transform.position;
    }
}