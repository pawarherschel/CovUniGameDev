using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public sealed class MoveTarget : MonoBehaviour
{
    [NotNull]
    private static T MyDebug<T>(string msg, T t)
    {
        Debug.Log(msg + t);

        return t;
    }

    [SerializeField] private GameObject agent;
    [SerializeField] private Collider plane;

    private NavMeshAgent _navMeshAgent;

    private void Start()
    {
        _navMeshAgent = agent.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update()
    {
        var agentReachedMe = Vector3.Distance(agent.transform.position, transform.position) < 1;
        var agentHasStopped = Vector3.Magnitude(_navMeshAgent.velocity) < 0.001;
        if (!agentReachedMe && !agentHasStopped) return;

        var newPosition = new Vector3(MyDebug("x: ", Random.Range(-1f, 1f)), 0, MyDebug("z: ", Random.Range(-1f, 1f))) *
                          plane.bounds.extents.x;
        print(newPosition);
        transform.position = newPosition;
    }
}