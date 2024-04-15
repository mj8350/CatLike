using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : PoolLabel
{
    private float TT;
    public void ThornUp(float time)
    {
        TT = time;
        StartCoroutine("Up");
    }

    private IEnumerator Up()
    {
        yield return YieldInstructionCache.WaitForSeconds(TT);
        ReturnPool();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamage>(out IDamage damage))
        {
            damage.TakeDamage(5f);
        }
    }
}
