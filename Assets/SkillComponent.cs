using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillComponent : MonoBehaviour
{
    protected float skillDelay;
    protected float damage;

    virtual public void ActiveSkill()
    {
        if (CanUseSkill())
            return;
    }

    private bool CanUseSkill()
    {
        return true;
    }
}
