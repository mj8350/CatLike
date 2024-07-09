using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnEX : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;

    private int value = 0;
    private GameObject obj;
    private Vector3 Pos = new Vector3(-0.5f, 6f, 0);

    public void Function_Button()
    {
        obj = PoolManager.Inst.pools[dropdown.value +10].Pop();
        obj.transform.position = Pos;
        if (obj.TryGetComponent<AI>(out AI ai))
            ai.Creat();
    }
}
