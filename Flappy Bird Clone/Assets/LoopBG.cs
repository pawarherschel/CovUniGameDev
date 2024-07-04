using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopBg : MonoBehaviour
{
    [SerializeField] private float resetPositionX;
    [SerializeField] private float exitPositionX;
    [SerializeField] private float speed;
    
    // Update is called once per frame
    private void Update()
    {
        transform.position = transform.position.x <= exitPositionX ? new Vector3(resetPositionX, transform.position.y) : new Vector3(transform.position.x - (speed / Time.deltaTime), transform.position.y);
    }
}
