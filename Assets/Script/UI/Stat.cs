using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Stat
{
    [SerializeField]
    private Bar bar;

    [SerializeField]
    private float maxVal;

    [SerializeField]
    private float currValue;

    public float CurrValue
    {
        get 
        { 
            return currValue; 
        }
        set 
        {
            this.currValue = Mathf.Clamp(value, 0, MaxVal); 
            bar.Value = currValue;
            
        }
    }
    public float MaxVal
    {
        get 
        { 
            return maxVal; 
        }
        set 
        { 
            this.maxVal = value;
            bar.MaxValue = maxVal;
        }
    }
    
    

    public void Initialize()
    {
        this.MaxVal = maxVal;
        this.CurrValue = currValue;
    }
}
