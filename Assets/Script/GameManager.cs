using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    
    private static GameManager instance;

    [SerializeField]
    private GameObject blueBerry;

    public GameObject BlueBerry
    {
        
        get 
        { 
            return blueBerry; 
        }
        
    }

    public static GameManager Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
        
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
