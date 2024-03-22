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
}
