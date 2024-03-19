using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowEnemy : PoolLabel
{
    [SerializeField]
    private float moveSpeed;

    private GameObject obj;


    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamage>(out IDamage damage))
        {
            damage.TakeDamage(5);
            obj = PoolManager.Inst.pools[(int)PoolState.boom].Pop();
            obj.transform.position = transform.position;
            if(obj.TryGetComponent<Boom>(out Boom boom))
            {
                boom.BoomOff();
            }
            ReturnPool();
        }
    }
}
