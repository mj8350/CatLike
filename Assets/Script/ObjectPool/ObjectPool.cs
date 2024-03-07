using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject targetObj; // 생성해 낼 오브젝트.
    [SerializeField]
    private int allocateCount; // 생성해 낼 갯수.

    private Stack<PoolLabel> poolStack = new Stack<PoolLabel>(); // 미리 생성된 오브젝트를 담고 있을 스택.

    private int objMaxCount; // 오브젝트 풀이 만들어낸 오브젝트의 총 갯수.
    private int objActiveCount; // 활성화 되어 사용 중인 오브젝트의 총 갯수.

    private void Awake()
    {
        objMaxCount = 0;
        objActiveCount = 0;
        Allocate();
    }

    private GameObject obj;
    public void Allocate()
    {
        for (int i = 0; i < allocateCount; i++)
        {
            obj = Instantiate(targetObj, this.transform);
            if (obj.TryGetComponent<PoolLabel>(out PoolLabel label))
            {
                label.Create(this); // 라벨에 생성해낸 오너가 누구인지 기록.
                poolStack.Push(label);
                objMaxCount++;
            }
            else
                Debug.Log("ObjectPool.cs - Allocate() - PoolLabel이 아닌 객체를 생성");
        }
    }


    // pool에서 관리하고 있는 객체 한개 리턴. 
    public GameObject Pop()
    {
        if (objActiveCount >= objMaxCount)
        {
            Allocate();
        }

        obj = poolStack.Pop().gameObject;
        obj.SetActive(true);
        if (obj.TryGetComponent<PoolLabel>(out PoolLabel label))
            label.GettingPool();
        objActiveCount++;
        return obj;
    }

    // 사용이 끝난 객체를 반환 받는 함수.
    public void Push(PoolLabel returnObj)
    {
        if (returnObj.gameObject.activeSelf) // 활성화된 상태로 반환 됐다면,
        {
            returnObj.gameObject.SetActive(false); // 비활성화 처리.
            poolStack.Push(returnObj);
            objActiveCount--;
        }
    }
}
