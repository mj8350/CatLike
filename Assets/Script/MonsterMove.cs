using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    private Transform player;
    private SpriteRenderer sr;
    private Animator anim;

    private Vector3 moveDir;

    private bool rush;

    public float moveSpeed;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        if (!TryGetComponent<SpriteRenderer>(out sr))
            Debug.Log("MonsterMove.cs - Awake() - sr 참조 실패");
        if(!TryGetComponent<Animator>(out anim))
            Debug.Log("MonsterMove.cs - Awake() - anim 참조 실패");
        moveSpeed = 0;
        rush = false;
    }

    public void MoveRushOn(float speed, Vector3 newdir)
    {
        moveDir = newdir;
        moveSpeed = speed;
        rush = true;
    }
    public void MoveRushOff()
    {
        moveSpeed = 0;
        rush = false;
    }

    void Update()
    {
        
        if (moveSpeed != 0f)
        {
            if (!rush)
            {
                moveDir = (player.position - transform.position).normalized;
                transform.position += moveDir * moveSpeed * Time.deltaTime;
                anim.SetBool("Move", true);
            }
            else
            {
                transform.position += moveDir * moveSpeed * Time.deltaTime;
            }
        }
        else
        {
            anim.SetBool("Move", false);
        }

        if (moveDir.x < 0f)
            sr.flipX = true;
        else
            sr.flipX = false;

        if (Input.GetKeyDown(KeyCode.G))
            StartCoroutine("ToMove");

    }

    

}
