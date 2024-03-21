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
        if (instance) // instance 가 null 이 아니면,
        {
            Destroy(gameObject);
            return;
        }
        else
            instance = this; // 최초로 만들어지는 instance라면, 
    }

    public List<ObjectPool> pools; // 해당 매니저가 관리하는 오브젝트 풀의 리스트.
}
