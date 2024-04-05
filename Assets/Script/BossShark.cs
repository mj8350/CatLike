using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShark : MonoBehaviour
{
    private GameObject obj;
    private MonsterChar monster;
    private Transform player;
    public bool die;

    private void Awake()
    {
        if (!TryGetComponent<MonsterChar>(out monster))
            Debug.Log("BossShark.cs - Awake() - monster 참조 오류");
        player = GameObject.Find("Player").transform;
        die = true;
    }

    private void Update()
    {
        if(monster.HP <= 0)
            die = false;
    }

    private IEnumerator spwanIce3()
    {
        while (die)
        {
            for (int i = 0; i < 4; i++)
            {
                yield return YieldInstructionCache.WaitForSeconds(3f);
                obj = PoolManager.Inst.pools[(int)PoolState.warning].Pop();
                obj.transform.position = player.position;
                if (obj.TryGetComponent<Warning>(out Warning warning))
                {
                    warning.SetType(PoolState.ice, 0f);
                    warning.iceBorn = true;
                }
            }
        }
    }
}
