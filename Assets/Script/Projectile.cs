using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : PoolLabel
{
    private bool isSetDir = false;
    //private bool isSetDir2 = false;


    private Vector3 moveDir;
    private float moveSpeed;
    private Vector3 DefaultScale = new Vector3(0.6f, 0.6f, 0.6f);

    private Transform player;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    public void MoveTo1(Vector3 newDir, float newSpeed, Vector3 newScale)
    {
        moveDir = newDir.normalized;
        moveSpeed = newSpeed;
        transform.localScale = newScale;
        isSetDir = true;
    }

    public void MoveTo2(float newSpeed, Vector3 newScale)
    {
        moveSpeed = newSpeed;
        transform.localScale = newScale;
        isSetDir = true;
        StartCoroutine("Move2");
    }
    private IEnumerator Move2()//0.5�� �������� 0.5�ʸ���� 2�� �÷��̾� ���� �� ��������
    {
        moveDir = (player.position - transform.position).normalized;
        
        yield return YieldInstructionCache.WaitForSeconds(0.5f);
        isSetDir = false;
        
        yield return YieldInstructionCache.WaitForSeconds(0.5f);
        isSetDir = true;
        StartCoroutine("FMove");
        yield return YieldInstructionCache.WaitForSeconds(2f);
        StopCoroutine("FMove");


    }

    private IEnumerator FMove()//�÷��̾� ��� ����
    {
        while (isSetDir)
        {
            moveDir = (player.position - transform.position).normalized;
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isSetDir)
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }
        /*if (isSetDir2)
        {
            StartCoroutine("Move2");
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }*/
    }

    private void OnDisable()
    {
        isSetDir = false;

        //isSetDir2 = false;
        StopCoroutine("Move2");

        moveSpeed = 0f;
        moveDir = Vector3.zero;
        transform.localScale = DefaultScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IDamage>(out IDamage damage))
        {
            damage.TakeDamage(5f);
            OnDisable();
            ReturnPool();
        }
    }

}
