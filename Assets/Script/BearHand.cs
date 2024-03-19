using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearHand : MonoBehaviour
{
    private BearChild bc;
    private SpriteRenderer sr;

    private void Awake()
    {
        bc = GetComponentInChildren<BearChild>();
        if (!TryGetComponent<SpriteRenderer>(out sr))
            Debug.Log("BearHand.cs - Awake() - sr 참조 오류");
    }

    private void Update()
    {
        bc.SRswap(sr.flipX);
    }

    private void On()
    {
        bc.WPon();
    }

    private void Off()
    {
        bc.WPoff();
    }
}
