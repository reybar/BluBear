using UnityEngine;
using System.Collections;

public class AutoScroll : MonoBehaviour 
{
    
    public Vector2 scrollSpeed = Vector2.one;

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () 
    {


        Vector2 finalTextureOffset = GetComponent<Renderer>().material.mainTextureOffset;

        finalTextureOffset += scrollSpeed * Time.deltaTime;

        if (finalTextureOffset.x >= 1.0f)
        {
            finalTextureOffset.x -= (float)System.Math.Truncate(finalTextureOffset.x);
            
        }

        while (finalTextureOffset.x < 0.0f)
        {
            finalTextureOffset.x += 1.0f;
        }

        GetComponent<Renderer>().material.mainTextureOffset = finalTextureOffset;
    }

}
