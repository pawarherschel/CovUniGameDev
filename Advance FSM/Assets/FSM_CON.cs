using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;

public sealed class FsmCon : MonoBehaviour
{
    private enum GameStates
    {
        Idle,
        ChasePlayer,
        Retreat
    };

    private GameStates _state = GameStates.Idle;

    private GameStates State
    {
        get => _state;
        set => _state = DPrint("new state: ", value);
    }

    private static T DPrint<T>(string msg, T t)
    {
        print(msg + t);

        return t;
    }

    private enum GameEvents
    {
        OnEnter,
        OnUpdate
    };
    
    private GameEvents _event = GameEvents.OnEnter;

    private GameEvents Event
    {
        get => _event;
        set => _event = DPrint("new event: ", value);
    }

    private GameObject[] _gameObjects;
    private GameObject _player;
    private const float MoveSpeed = 3f;
    private bool _hitByProjectileEvent;

    private void Start()
    {
        _gameObjects = GameObject.FindGameObjectsWithTag("Player");
        Assert.AreNotEqual(_gameObjects.Length, 0);

        _player = _gameObjects[0];
        Assert.IsNotNull(_player);
    }

    private void Update()
    {
        this.FsmUpdate();
    }

    private void FsmUpdate()
    {
        switch (State)
        {
            case GameStates.Idle:
                switch (Event)
                {
                    case GameEvents.OnEnter:
                        this.IdleEnter();
                        break;
                    case GameEvents.OnUpdate:
                        this.IdleUpdate();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                break;
            case GameStates.ChasePlayer:
                switch (Event)
                {
                    case GameEvents.OnEnter:
                        this.ChasePlayerEnter();
                        break;
                    case GameEvents.OnUpdate:
                        this.ChasePlayerUpdate();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                break;
            case GameStates.Retreat:
                switch (Event)
                {
                    case GameEvents.OnEnter:
                        this.RetreatEnter();
                        break;
                    case GameEvents.OnUpdate:
                        this.RetreatUpdate();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        this.FsmProcessInputs();
    }

    private void OnCollisionEnter([NotNull] Collision other)
    {
        print("on collision enter: " + other.gameObject.name);

        if (!other.gameObject.CompareTag("Projectile")) return;
        Destroy(other.gameObject);
        _hitByProjectileEvent = true;
    }

    private void FsmProcessInputs()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeFsmState(GameStates.ChasePlayer);
        }
    }

    private void ChangeFsmState(GameStates newState)
    {
        switch (State)
        {
            case GameStates.Idle:
                IdleExit();
                break;
            case GameStates.ChasePlayer:
                ChasePlayerExit();
                break;
            case GameStates.Retreat:
                RetreatExit();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        State = newState;
        Event = GameEvents.OnEnter;
    }

    private void RetreatExit()
    {
        print("RetreatExit()");
    }

    private void ChasePlayerExit()
    {
        print("ChasePlayerExit()");
    }

    private void IdleExit()
    {
        print("IdleExit()");
    }

    private void RetreatUpdate()
    {
        var myPosition = this.transform.position;
        var playerPosition = _player.transform.position;
        var direction = playerPosition - myPosition;

        direction.y = 0;
        direction.Normalize();

        var playerIsTooFar = Vector3.Distance(myPosition, playerPosition) > 20f;

        if (playerIsTooFar)
        {
            ChangeFsmState(GameStates.Idle);
        }
    }

    private void RetreatEnter()
    {
        Event = GameEvents.OnUpdate;
    }

    private void ChasePlayerUpdate()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, _player.transform.position,
            (MoveSpeed * Time.deltaTime));

        if (!_hitByProjectileEvent) return;
        _hitByProjectileEvent = false;
        ChangeFsmState(GameStates.Retreat);
    }

    private void ChasePlayerEnter()
    {
        Event = GameEvents.OnUpdate;
    }

    private void IdleUpdate()
    {
        Transform myTransform;
        (myTransform = this.transform).Rotate(0, 1, 0, Space.World);

        var playerIsVisible = Vector3.Distance(myTransform.position, _player.transform.position) < 10f;

        if (playerIsVisible)
        {
            ChangeFsmState(GameStates.ChasePlayer);
        }

        if (!_hitByProjectileEvent) return;
        _hitByProjectileEvent = false;
        ChangeFsmState(GameStates.Retreat);
    }

    private void IdleEnter()
    {
        Event = GameEvents.OnUpdate;
    }
}
