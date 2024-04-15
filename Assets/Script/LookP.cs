using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookP : MonoBehaviour
{
    private Transform player;
    private SpriteRenderer sr;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        if (!TryGetComponent<SpriteRenderer>(out sr))
            Debug.Log("LookP.cs - Awake() - sr 참조 실패");
    }

    private void Update()
    {
        if (transform.position.x > player.position.x)
            sr.flipX = true;
        else
            sr.flipX = false;
    }
}
