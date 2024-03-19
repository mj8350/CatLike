using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : PoolLabel
{
    
    public void BoomOff()
    {
        StartCoroutine("off");
    }

    private IEnumerator off()
    {
        yield return YieldInstructionCache.WaitForSeconds(0.5f);
        ReturnPool();
    }
}
