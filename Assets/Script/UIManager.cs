using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject Knight;
    [SerializeField] private GameObject Bow;
    [SerializeField] private Slider StaminaBar;
    [SerializeField] private Slider EXPBar;
    [SerializeField] private GameObject[] HPHeart;
    [SerializeField] private TextMeshProUGUI Gold;
    [SerializeField] private Slider BossHPBar;
    [SerializeField] private TextMeshProUGUI Level;


    public void Stance(int i)
    {
        if (i == 1)
            Knight.transform.SetAsLastSibling();
        if (i == 2)
            Bow.transform.SetAsLastSibling();
    }

    public void STBar(float st, float stMax)
    {
        float percent = st / stMax;
        StaminaBar.value = percent;
    }

    public void HPBar(float hp, float max)
    {
        float percent = (max - hp) / 50;
        for (int i = 0; i < max / 50; i++)
        {
            if (percent - i < 1 && percent - i >= 0)
                HPHeart[i].transform.GetChild(1).localScale = new Vector3(1 - (percent % 1), 1 - (percent % 1), 1 - (percent % 1));
            else if (percent - i < 0)
                HPHeart[i].transform.GetChild(1).localScale = Vector3.one;
            else
                HPHeart[i].transform.GetChild(1).localScale = Vector3.zero;
        }
    }

    public void MAXHPBar(float Max)
    {
        for (int i = 0; i < Max / 50; i++)
        {
            if (!HPHeart[i].activeSelf)
                HPHeart[i].SetActive(true);
        }
    }

    public void ExpBar(float exp, float max)
    {
        float percent = exp / max;
        EXPBar.value = percent;
    }

    public void Levelup(int level)
    {
        Level.text = $"Lv.{level}";
    }

    public void GoldT(int gold)
    {
        Gold.text = $"{gold}G";
    }
}
