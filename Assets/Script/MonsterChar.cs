using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChar : PoolLabel, IDamage
{
    public void TakeDamage(float damage)
    {
        Debug.Log($"���Ͱ� {damage}�� �������� ����");
    }
}
