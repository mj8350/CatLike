using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolLabel : MonoBehaviour
{
    protected ObjectPool pool; // 자신을 관리해주는 풀이 어떤 객체인지. 

    // 오브젝트가 풀에 최초로 등록이 될때.
    public virtual void Create(ObjectPool owner)
    {
        pool = owner;
        gameObject.SetActive(false);
    }

    // 오브젝트가 풀에 반환이 될때 호출.
    public virtual void ReturnPool()
    {
        pool.Push(this);
    }

    // 오브젝트가 풀에서 꺼내져 나갈때 호출.
    public virtual void GettingPool()
    {

    }
}
