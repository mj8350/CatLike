using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : PoolLabel
{
    [SerializeField]
    private float moveSpeed;

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }

    public void del()
    {
        StartCoroutine("delete");
    }

    private IEnumerator delete()
    {
        yield return YieldInstructionCache.WaitForSeconds(0.2f);
        ReturnPool();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IDamage>(out IDamage damage))
        {
            damage.TakeDamage(5);
            ReturnPool();
        }

        if (collision.CompareTag("Wall") || collision.CompareTag("Object"))
            ReturnPool();
    }
}
