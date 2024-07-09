using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public sealed class Chicken : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float detectionDistance = 10f;
    [SerializeField] private float safeDistance = 15f;

    public enum E_State
    {
        Idle,
        Fleeing,
        Wandering
    }

    private E_State _state;

    public E_State State
    {
        set => _state = DPrint("new state: ", value);
        get => _state;
    }

    private NavMeshAgent _agent;
    private float _timer; 
    
    // Start is called before the first frame update
    private void Start()
    {
        State = E_State.Idle;
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update()
    {
        var playerPosition = playerTransform.position;
        var myPosition = this.transform.position;
        _timer += Time.deltaTime;

        var distance = Vector3.Distance(playerPosition, myPosition);
        var playerInRange = distance <= detectionDistance;

        switch (State)
        {
            case E_State.Idle:
                if (playerInRange)
                {
                    State = E_State.Fleeing;
                    break;
                }

                var wanderChance = Random.Range(-1f, 15f);
                if (DPrint("wander chance: ", wanderChance) < 0f)
                {
                    State = E_State.Wandering;
                }
                break;
            case E_State.Fleeing:
                var reachedSafeDistance = distance >= safeDistance;
                if (reachedSafeDistance)
                {
                    State = E_State.Idle;
                }
                else
                {
                    _agent.destination = this.FleeDestination(playerPosition);
                }
                break;
            case E_State.Wandering:
                if (playerInRange)
                {
                    State = E_State.Fleeing;
                    break;
                }
                
                if (_timer > 10f)
                {
                    _timer = 0;
                    _agent.destination = this.GetNewDestination();
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
    }

    private Vector3 FleeDestination(Vector3 playerPosition)
    {
        var fleeDirection = Vector3.Normalize(-playerPosition + this.transform.position);
        var distance = DPrint("distance: ", Time.deltaTime * 10_000f);
        var displacement = fleeDirection * distance;
        var position = this.transform.position + new Vector3(displacement.x, 0, displacement.y);

        return position;

    }

    private Vector3 GetNewDestination()
    {
        var randomDirection = Random.insideUnitCircle;
        var distance = DPrint("distance: ", Time.deltaTime * 10_000f);
        var displacement = randomDirection * distance;
        var position = this.transform.position + new Vector3(displacement.x, 0, displacement.y);
        
        print("curr pos: " + this.transform.position);
        return DPrint("new position: ", position);
    }

    private void Flee(Vector3 playerPosition, Vector3 myPosition)
    {
        var directionAwayFromPlayer = Vector3.Normalize(myPosition - playerPosition);
        var moveAmount = Time.deltaTime;
        this.transform.position += directionAwayFromPlayer * moveAmount;
    }

    private static T DPrint<T>(string msg, T t)
    {
        print(msg + t);

        return t;
    }
}
