using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public sealed class FireCon : MonoBehaviour
{
    [SerializeField] private Rigidbody projectilePrefab;
    [SerializeField] private Transform barrelEnd;

    private void Update()
    {
        if (!Input.GetButtonDown("Fire1")) return;
        var projectileInstance = Instantiate(projectilePrefab, barrelEnd.position, barrelEnd.rotation);
        projectileInstance.AddForce(barrelEnd.forward * 5000);
        Destroy(projectileInstance.gameObject, 8);
    }
}
