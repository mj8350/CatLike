using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChar : PoolLabel, IDamage
{
    public void TakeDamage(float damage)
    {
        Debug.Log($"몬스터가 {damage}의 데미지를 입음");
    }
}
