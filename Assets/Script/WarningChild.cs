using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningChild : MonoBehaviour
{
    private Warning warning;

    private void Awake()
    {
        warning = transform.GetComponentInParent<Warning>();
    }
    public void SetFire()
    {
        warning.Spwan();
    }
}
