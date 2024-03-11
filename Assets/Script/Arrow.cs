using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : PoolLabel
{
    private float moveSpeed;

    void Start()
    {
        moveSpeed = 40f;
    }

    
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }
}
