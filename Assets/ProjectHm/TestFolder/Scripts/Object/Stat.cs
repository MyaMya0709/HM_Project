using System;
using UnityEngine;

public class Stat
{
    private float _curStat;
    public float CurStat
    {
        get { return _curStat; }
        set
        {
            if (_curStat != value)
            {
                _curStat = value;
                OnStatChanged?.Invoke();
                OnStatPercentageChanged?.Invoke(GetPercentage());
            }
        }
    }
    public float minStat;
    public float maxStat;

    public Action OnStatChanged;
    public Action<float> OnStatPercentageChanged;


    public Stat(float startStat, float maxStat, float minStat = 0f)
    {
        this.CurStat = startStat;
        this.maxStat = maxStat;
        this.minStat = minStat;
    }

    public void Add(float amount)
    {
        CurStat = Mathf.Min(CurStat + amount, maxStat);
    }

    public void Extend(float amount)
    {
        maxStat += amount;
        CurStat = Mathf.Min(CurStat + amount, maxStat);
    }

    public void Subtract(float amount)
    {
        CurStat = Mathf.Max(CurStat - amount, minStat);
    }

    public void shrink(float amount)
    {
        maxStat -= amount;
        CurStat = Mathf.Min(CurStat, maxStat);
    }

    public float GetPercentage()
    {
        return CurStat / maxStat;
    }

    public void Reset()
    {
        CurStat = maxStat;
    }
}
