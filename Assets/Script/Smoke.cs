using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : PoolLabel
{
    public void SmokeOff()
    {
        ReturnPool();
    }
}
