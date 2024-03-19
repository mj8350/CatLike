using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertLine : PoolLabel
{
    private Vector3 dir;
    private Transform player;
    private float angle;
    private GameObject obj;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        dir = player.position - transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    public void Spwan()
    {
        obj = PoolManager.Inst.pools[(int)PoolState.arrowEnemy].Pop();
        obj.transform.position = transform.position;
        obj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        ReturnPool();
    }

}
