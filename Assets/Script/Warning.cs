using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : PoolLabel
{
    private int type;

    private GameObject obj;



    public void SetType(PoolState poolState)
    {
        type = (int)poolState;
    }
    
    public void Spwan()
    {
        obj = PoolManager.Inst.pools[type].Pop();
        obj.transform.position = transform.position;
        if(obj.TryGetComponent<Thorn>(out Thorn thorn))
        {
            thorn.ThornUp();
        }
        ReturnPool();
    }
    
}
