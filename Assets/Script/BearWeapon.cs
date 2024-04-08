using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearWeapon : MonoBehaviour
{
    [SerializeField]
    private Sprite cb;
    [SerializeField]
    private Sprite sp;


    private GameObject obj;
    private TwoAi ai;
    private SpriteRenderer sr;


    private void Awake()
    {
        obj = transform.root.gameObject;
        if (!obj.TryGetComponent<TwoAi>(out ai))
            Debug.Log("BearWeapon.cs - Awake() - ai참조 오류");
        if(!TryGetComponent<SpriteRenderer>(out sr))
            Debug.Log("BearWeapon.cs - Awake() - sr참조 오류");
    }

    private void Update()
    {
        if(ai.ran == 0)
        {
            sr.sprite = null;
        }
        if(ai.ran == 1)
        {
            sr.sprite = cb;
        }
        if(ai.ran == 2)
        {
            sr.sprite = sp;
        }
    }
}
