using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolState
{
    bow = 0,
    projectile,
    miniChoco,
    warning,
    thorn,
    ice,
    alertLine,
    arrowEnemy,
    boom,
    bubble,

    slime,
    snake,
    choco,
    grape,
    bear_CB,
    bear_SP,
    pudding,
    shark,
    slime_Mid,
    slime_Boss,
}
public class PoolManager : MonoBehaviour
{
    private static PoolManager instance;
    public static PoolManager Inst => instance;

    private void Awake()
    {
        if (instance) // instance �� null �� �ƴϸ�,
        {
            Destroy(gameObject);
            return;
        }
        else
            instance = this; // ���ʷ� ��������� instance���, 
    }

    public List<ObjectPool> pools; // �ش� �Ŵ����� �����ϴ� ������Ʈ Ǯ�� ����Ʈ.
}
