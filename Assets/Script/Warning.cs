using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : PoolLabel
{
    private int type;
    private float TT;
    private GameObject obj;

    public bool iceBorn = false;


    public void SetType(PoolState poolState,float time)
    {
        type = (int)poolState;
        TT = time;
    }
    
    public void Spwan()
    {
        obj = PoolManager.Inst.pools[type].Pop();
        obj.transform.position = transform.position;
        if(obj.TryGetComponent<Thorn>(out Thorn thorn))
        {
            thorn.ThornUp(TT);
        }
        if(obj.TryGetComponent<Ice>(out Ice ice)&&iceBorn)
		{
            ice.IcePop();
		}
        iceBorn = false;
        ReturnPool();
    }
    
}
