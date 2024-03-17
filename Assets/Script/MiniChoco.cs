using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniChoco : PoolLabel
{
    private MonsterAttack attack;

    private void Awake()
    {
        if (!TryGetComponent<MonsterAttack>(out attack))
            Debug.Log("MiniChoco.cs - Awake() - attack 참조 실패");

    }
    public void miniOn()
    {
        StartCoroutine("Chocomini");
    }

    private IEnumerator Chocomini()
    {
        yield return YieldInstructionCache.WaitForSeconds(1);

        for(int i = 0; i<3; i++)
        {
            attack.AttackActive(AttackType.FollowFire01);
            yield return YieldInstructionCache.WaitForSeconds(2);
        }

        attack.AttackActive(AttackType.CircleFire01);
        yield return YieldInstructionCache.WaitForSeconds(2);
        ReturnPool();
    }

    


}
