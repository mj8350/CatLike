using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator anim;


    private Vector3 moveDir = Vector3.zero;
    private float moveSpeed;
    private bool move;
    private bool miss;

    void Start()
    {
        if(!TryGetComponent<Animator>(out anim))
            Debug.Log("Player.cs - Start() - anim 참조 오류");

        moveSpeed = 5f;
        move = true;
        miss = true;
    }

    void Update()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8.5f, 8.5f), Mathf.Clamp(transform.position.y, -4.9f, 3.8f));
        moveDir.x = Input.GetAxis("Horizontal");
        moveDir.y = Input.GetAxis("Vertical");

        if (move)
        {
            /*if (moveDir.x == 0 || moveDir.y == 0)
                transform.position += moveDir * Time.deltaTime * moveSpeed;
            else
                transform.position += moveDir * Time.deltaTime * (moveSpeed - 1.5f);*/

            if (moveDir.x != 0 || moveDir.y != 0)
            {
                anim.SetBool("Move", true);
                transform.position += moveDir * Time.deltaTime * moveSpeed;

                if (moveDir.x > 0 && moveDir.y == 0)
                {
                    anim.SetFloat("MotionDir", 0f);
                    Space(new Vector3(1,0,0));
                }
                else if (moveDir.x > 0 && moveDir.y > 0)
                {
                    anim.SetFloat("MotionDir", 1f);
                    Space(new Vector3(1, 1, 0));
                }
                else if (moveDir.x == 0 && moveDir.y > 0)
                {
                    anim.SetFloat("MotionDir", 2f);
                    Space(new Vector3(0, 1, 0));
                }
                else if (moveDir.x < 0 && moveDir.y > 0)
                {
                    anim.SetFloat("MotionDir", 3f);
                    Space(new Vector3(-1, 1, 0));
                }
                else if (moveDir.x < 0 && moveDir.y == 0)
                {
                    anim.SetFloat("MotionDir", 4f);
                    Space(new Vector3(-1, 0, 0));
                }
                else if (moveDir.x < 0 && moveDir.y < 0)
                {
                    anim.SetFloat("MotionDir", 5f);
                    Space(new Vector3(-1, -1, 0));
                }
                else if (moveDir.x == 0 && moveDir.y < 0)
                {
                    anim.SetFloat("MotionDir", 6f);
                    Space(new Vector3(0, -1, 0));
                }
                else if (moveDir.x > 0 && moveDir.y < 0)
                {
                    anim.SetFloat("MotionDir", 7f);
                    Space(new Vector3(1, -1, 0));
                }
            }
            else
                anim.SetBool("Move", false);
        }


    }

    private void Space(Vector3 vector)
    {
        if (miss && Input.GetKeyDown(KeyCode.Space))
        {
            miss = false;
            move = false;
            anim.SetTrigger("Miss");
            StartCoroutine("MoveOn");
            StartCoroutine(Miss(vector));
        }
    }

    WaitForSeconds wfs = new WaitForSeconds(0.5f);
    private IEnumerator MoveOn()
    {
        yield return wfs;
        move = true;
        miss = true;
    }

    private IEnumerator Miss(Vector3 vec)
    {
        yield return null;
        while (!miss)
        {
            transform.position += vec.normalized * Time.deltaTime * (moveSpeed + 3f);
            yield return null;
        }
    }
}
