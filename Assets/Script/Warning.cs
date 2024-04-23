using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : PoolLabel
{
    private int type;
    private float TT;
    private GameObject obj;

    public bool iceBorn = false;
    public bool zigu = false;


    public void SetType(PoolState poolState,float time)
    {
        type = (int)poolState;
        TT = time;
    }
    
    public void Spwan()
    {
        if (!zigu)
        {
            obj = PoolManager.Inst.pools[type].Pop();
            obj.transform.position = transform.position;
            if (obj.TryGetComponent<Thorn>(out Thorn thorn))
            {
                thorn.ThornUp(TT);
            }
            if (obj.TryGetComponent<Ice>(out Ice ice) && iceBorn)
            {
                ice.IcePop();
            }
            iceBorn = false;
        }
        else
        {
            Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, 3.5f);
            foreach (Collider2D col in hit)
            {
                if (col.CompareTag("Player"))
                {
                    if (col.TryGetComponent<Player>(out Player player) && player.miss)
                    {
                        if (col.TryGetComponent<IDamage>(out IDamage damage))
                        {
                            damage.TakeDamage(5f);
                        }
                    }
                }
            }
            transform.localScale = new Vector3(1.5f, 1f, 1f);
            zigu = false;
        }
        ReturnPool();
    }
    
    public void ziguScale()
    {
        transform.localScale = new Vector3(7f, 7f, 7f);
    }
        
    
    
}
