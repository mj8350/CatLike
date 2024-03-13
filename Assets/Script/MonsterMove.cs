using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    private Transform player;
    private SpriteRenderer sr;
    private Animator anim;

    private Vector3 moveDir;

    public float moveSpeed;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        if (!TryGetComponent<SpriteRenderer>(out sr))
            Debug.Log("MonsterMove.cs - Awake() - sr 참조 실패");
        if(!TryGetComponent<Animator>(out anim))
            Debug.Log("MonsterMove.cs - Awake() - anim 참조 실패");
        moveSpeed = 0;
    }

    void Update()
    {
        moveDir = (player.position - transform.position).normalized;
        if (moveDir.x < 0f)
            sr.flipX = true;
        else
            sr.flipX = false;

        if (moveSpeed != 0f)
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
            anim.SetBool("Move", true);
        }
        else
        {
            anim.SetBool("Move", false);
        }

        if (Input.GetKeyDown(KeyCode.G))
            StartCoroutine("ToMove");

    }

    

}
