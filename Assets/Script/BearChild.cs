using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearChild : MonoBehaviour
{
    private SpriteRenderer sr;

    private void Awake()
    {
        if (!TryGetComponent<SpriteRenderer>(out sr))
            Debug.Log("BearChild.cs - Awake() - sr 참조 오류");
    }

    public void SRswap(bool sp)
    {
        if (sp)
            sr.flipX = true;
        else
            sr.flipX = false;
    }

    public void WPon()
    {
        if (sr.flipX)
            transform.rotation = Quaternion.Euler(0, 0, 45f);
        else
            transform.rotation = Quaternion.Euler(0, 0, -45f);
    }
    public void WPoff()
    {
        transform.rotation = Quaternion.identity;
    }
}
