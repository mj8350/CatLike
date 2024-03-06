using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator anim;


    private Vector3 moveDir = Vector3.zero;
    private Vector2 MousePosition;

    private float angle;
    private float moveSpeed;
    private bool move;
    private bool miss;
    private bool bow;

    void Start()
    {
        if (!TryGetComponent<Animator>(out anim))
            Debug.Log("Player.cs - Start() - anim 참조 오류");

        moveSpeed = 5f;
        move = true;
        miss = true;
        bow = false;
    }

    void Update()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8.5f, 8.5f), Mathf.Clamp(transform.position.y, -4.9f, 3.8f));
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");


        if (Input.GetMouseButton(0))
        {
            //MousePosition = Input.mousePosition;
            //MousePosition = camera.ScreenToWorldPoint(MousePosition);
            MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            angle = Mathf.Atan2(MousePosition.y - transform.position.y, MousePosition.x - transform.position.x) * Mathf.Rad2Deg;
            bow = true;
            anim.SetBool("BowAttack", true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            bow = false;
            anim.SetBool("BowAttack", false);
        }

        if (move && bow)
        {
            transform.position += moveDir.normalized * Time.deltaTime * (moveSpeed - 3f);
            /*if (angle >= -22.5f && angle < 22.5f)
                anim.SetFloat("MouseDir", 0f);
            else if (angle >= 22.5f && angle < 67.5f)
                anim.SetFloat("MouseDir", 1f);
            else if (angle >= 67.5f && angle < 112.5f)
                anim.SetFloat("MouseDir", 2f);
            else if (angle >= 112.5f && angle < 157.5f)
                anim.SetFloat("MouseDir", 3f);
            else if (angle >= 157.5f || angle < -157.5f)
                anim.SetFloat("MouseDir", 4f);
            else if (angle >= -157.5f && angle < -112.5f)
                anim.SetFloat("MouseDir", 5f);
            else if (angle >= -112.5f && angle < -67.5f)
                anim.SetFloat("MouseDir", 6f);
            else if (angle >= -67.5f && angle < -22.5f)
                anim.SetFloat("MouseDir", 7f);*/
            anim.SetFloat("MouseDir", MouseResult());
            Space(moveDir);

        }
        else if (move)
        {
            if (moveDir.x != 0 || moveDir.y != 0)
            {
                anim.SetBool("Move", true);
                transform.position += moveDir.normalized * Time.deltaTime * moveSpeed;

                /*if (moveDir.x > 0 && moveDir.y == 0)
                {
                    anim.SetFloat("MotionDir", 0f);
                }
                else if (moveDir.x > 0 && moveDir.y > 0)
                {
                    anim.SetFloat("MotionDir", 1f);
                }
                else if (moveDir.x == 0 && moveDir.y > 0)
                {
                    anim.SetFloat("MotionDir", 2f);
                }
                else if (moveDir.x < 0 && moveDir.y > 0)
                {
                    anim.SetFloat("MotionDir", 3f);
                }
                else if (moveDir.x < 0 && moveDir.y == 0)
                {
                    anim.SetFloat("MotionDir", 4f);
                }
                else if (moveDir.x < 0 && moveDir.y < 0)
                {
                    anim.SetFloat("MotionDir", 5f);
                }
                else if (moveDir.x == 0 && moveDir.y < 0)
                {
                    anim.SetFloat("MotionDir", 6f);
                }
                else if (moveDir.x > 0 && moveDir.y < 0)
                {
                    anim.SetFloat("MotionDir", 7f);
                }*/
                anim.SetFloat("MotionDir", MoveResult());
                Space(moveDir);
            }
            else
                anim.SetBool("Move", false);
        }


    }

    private int MoveResult()
    {
        if (moveDir.x > 0 && moveDir.y == 0)
            return 0;
        else if (moveDir.x > 0 && moveDir.y > 0)
            return 1;
        else if (moveDir.x == 0 && moveDir.y > 0)
            return 2;
        else if (moveDir.x < 0 && moveDir.y > 0)
            return 3;
        else if (moveDir.x < 0 && moveDir.y == 0)
            return 4;
        else if (moveDir.x < 0 && moveDir.y < 0)
            return 5;
        else if (moveDir.x == 0 && moveDir.y < 0)
            return 6;
        else //if (moveDir.x > 0 && moveDir.y < 0)
            return 7;
    }

    private int MouseResult()
    {
        if (angle >= -22.5f && angle < 22.5f)
            return 0;
        else if (angle >= 22.5f && angle < 67.5f)
            return 1;
        else if (angle >= 67.5f && angle < 112.5f)
            return 2;
        else if (angle >= 112.5f && angle < 157.5f)
            return 3;
        else if (angle >= 157.5f || angle < -157.5f)
            return 4;
        else if (angle >= -157.5f && angle < -112.5f)
            return 5;
        else if (angle >= -112.5f && angle < -67.5f)
            return 6;
        else //if (angle >= -67.5f && angle < -22.5f)
            return 7;
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
    WaitForSeconds wfs1 = new WaitForSeconds(0.05f);
    private IEnumerator MoveOn()
    {
        yield return wfs;
        move = true;
        yield return wfs1;
        miss = true;
    }

    private IEnumerator Miss(Vector3 vec)
    {
        while (!move)
        {
            transform.position += vec.normalized * Time.deltaTime * (moveSpeed + 3f);
            yield return null;
        }
    }
}
