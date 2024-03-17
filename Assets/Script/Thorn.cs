using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : PoolLabel
{
    public void ThornUp()
    {
        StartCoroutine("Up");
    }

    private IEnumerator Up()
    {
        yield return YieldInstructionCache.WaitForSeconds(3f);
        ReturnPool();
    }
}
