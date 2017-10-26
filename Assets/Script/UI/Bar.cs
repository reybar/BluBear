using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bar : MonoBehaviour {

    
    private float fill;

    [SerializeField]
    private Image content;

    public float MaxValue { get; set; }

    public float Value
    {
        set
        {
            fill = Map(value, MaxValue);
        }
    }

	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        HandleBar();
	}

    private void HandleBar()
    {
        if (fill != content.fillAmount)
        {
            content.fillAmount = fill;
        }
        
    }

    private float Map(float value, float maxHp)
    {
        return (value / maxHp);
    }
}


