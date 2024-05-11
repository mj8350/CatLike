using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    private int level;
    private float exp;
    private float maxhp;
    private float hp;
    public float MaxStamina = 200;
    private float stamina;
    private int gold;
    #region getset
    public int Level
    {
        get => level;
        set
        {
            level = value;
            GameManager.Instance.LevelUI();
        }
    }
    public float EXP
    {
        get => exp;
        set
        {
            exp = value;
            GameManager.Instance.EXPUI();
        }
    }
    public float MaxHP
    {
        get => maxhp;
        set
        {
            maxhp = value;
            GameManager.Instance.MAXHPUI();
        }
    }
    public float HP
    {
        get => hp;
        set
        {
            hp = value;
            GameManager.Instance.HPUI();
        }
    }
    public float Stamina
    {
        get => stamina;
        set
        {
            stamina = value;
            GameManager.Instance.StaminaUI();
        }
    }
    public int Gold
    {
        get => gold;
        set
        {
            gold = value;
            GameManager.Instance.GoldUI();
        }
    }
    #endregion
}
public class GameManager : Singleton<GameManager>
{
    private PlayerData pData;
    public PlayerData PlayerData
    {
        get => pData;
    }

    private void Awake()
    {
        base.Awake();

        pData = new PlayerData();
        StartData();
        
    }

    public void StartData()
    {
        pData.Level = 1;
        pData.EXP = 0;
        pData.MaxHP = 150f;
        pData.HP = pData.MaxHP;
        pData.MaxStamina = 200f;
        pData.Stamina = pData.MaxStamina;
        pData.Gold = 0;
    }

    #region UI
    private UIManager uiManager;
    public UIManager UI_Manager
    {
        get
        {
            if (uiManager == null)
            {
                uiManager = FindFirstObjectByType<UIManager>();
            }
            return uiManager;
        }
    }
    public void STANCE(int i)
    {
        UI_Manager.Stance(i);
    }

    public void LevelUI()
    {
        UI_Manager.Levelup(pData.Level);
    }

    public void StaminaUI()
    {
        UI_Manager.STBar(pData.Stamina, pData.MaxStamina);
    }

    public void MAXHPUI()
    {
        UI_Manager.MAXHPBar(pData.MaxHP);
    }

    public void HPUI()
    {
        UI_Manager.HPBar(pData.HP, pData.MaxHP);
    }

    private float Levelup = 10;
    public void EXPUI()
    {
        if (pData.EXP >= Levelup)
        {
            pData.Level++;
            pData.EXP -= Levelup;
            Levelup *= 1.2f;
        }
        UI_Manager.ExpBar(pData.EXP, Levelup);
    }

    public void GoldUI()
    {
        UI_Manager.GoldT(pData.Gold);
    }
    #endregion

}
